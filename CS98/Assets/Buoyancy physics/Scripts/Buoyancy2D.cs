using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Buoyancy2D : MonoBehaviour {

    //Private vars
    private Vector3 center;
    private Transform currentWaterTransform;
    private bool triggered;
    private Vector3 buoyancy;
    private Vector3 point;
    private Rigidbody2D rigidbody2d;

    //Public configuration vars
    public float factor = 0.7f;
    public float damper = 0.3f;
    public float sinking = 0.07f;
    public Vector3 centerOfMass;

    //Public option vars
    public bool raycastingDetection;
    public LayerMask buoyancyDetectionLayers;

    //Configurate center of mass or custom center of mass
    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2d.centerOfMass = centerOfMass;
        center = -centerOfMass;
    }


    //Fixed update is only used to simulate physics, if you modify this script, use Update instead
    void FixedUpdate()
    {
        /*For the best precision, we simulate physics if this transform is inside the current water transform
         * renderer's bounds. In other words, only if this object is inside the water's renderer.
         */
        if (currentWaterTransform && currentWaterTransform.GetComponent<Renderer>().bounds.Contains(transform.position))
        {
            triggered = true;
        }
        else
        {
            triggered = false;
        }

        //If this object is inside a water transform (previously checked), simulate physics.
        if (triggered && currentWaterTransform)
        {
            Vector3 point = transform.position + transform.TransformDirection(center);
            float buoyancyPoint = 0.0f;

            /* Raycasting detection
             * Used on the top of the mesh, always detects the current position of the mesh collider (or edge collider).
             * This is useful for mesh deformation (water waves and splash effect). Also is more precise, but more expensive.
             * REMBEMBER: you need to set the "Buoyancy Detection Layers" of this object to the same layer of your water.
             */
            if (raycastingDetection)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, buoyancyDetectionLayers);
                if (hit.collider && hit.collider.name == "Water")
                {
                    buoyancyPoint = (hit.point.y - currentWaterTransform.position.y) * 2;
                }
            }
            /* Transform scale detection
             * If raycasting detection is disabled, the buoyancy point will be the top of the current transform.
             * May not work with non square meshes.
             */
            else
            {
                buoyancyPoint = currentWaterTransform.transform.localScale.y*2.5f;
            }
            
            /* Sinking parameter can't be 0 or negative because it causes null forces on rigidbody. To avoid exceptions, if sinking
             * value is 0 or less, set it to a minimal value.
             */
            if (sinking <= 0)
            {
                Debug.Log("<color=blue>Buoyancy physics</color> Sinking parameter cannot be 0 or less. Changing it to a minimal value");
                sinking = .01f;
            }

            /* Buoyancy simulation
             * Applies force to this transform relative to water's top position or "buoyancy point".
             */
            float force = factor - ((point.y - currentWaterTransform.transform.position.y - (buoyancyPoint / 2)) / (sinking));
            if (force > factor)
            {
                Vector3 buoyancy = -Physics.gravity * (force - rigidbody2d.velocity.y * damper);
                rigidbody2d.AddForceAtPosition(buoyancy, point);
            }

        }

    }

    /* On trigger enter check if the collider is water, then, activate physics simulation. You can add some splash
     * effects inside this code.
     */
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!currentWaterTransform && (collider.tag == "Water" || collider.name == "Water"))
        {
            currentWaterTransform = collider.transform;
        }
    }

    /* Deactivate physics on trigger exit and only if the trigger is the same than the current water's collider
     * Here we set the variable "currentWaterTransform" to null so FixedUpdate doesn't waste useful CPU usage.
     * Also you can add "out of water" effects here.
     */
    void OnTriggerExit2D(Collider2D collider)
    {
        if (currentWaterTransform && collider == currentWaterTransform.GetComponent<Collider2D>())
        {
            currentWaterTransform = null;
        }
    }
}
