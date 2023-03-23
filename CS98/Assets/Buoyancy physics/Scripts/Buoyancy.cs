using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {

    //Private vars
    private Vector3 center;
    private Transform currentWaterTransform;
    private bool triggered;
    private Vector3 buoyancy;
    private Vector3 point;

    //Public configuration vars
    public float factor = 0.7f;
    public float damper = 0.3f;
    public float sinking = 0.6f;
    public Vector3 centerOfMass;

    //Configurate center of mass or custom center of mass
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        center = -centerOfMass;
    }

    //Fixed update is only used to simulate physics, if you modify this script, use Update instead
    void FixedUpdate()
    {
        if (triggered && currentWaterTransform)
        {
            Vector3 point = transform.position + transform.TransformDirection(center);
            float buoyancyPoint = currentWaterTransform.transform.localScale.y;

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
                Vector3 buoyancy = -Physics.gravity * (force - GetComponent<Rigidbody>().velocity.y * damper);
                GetComponent<Rigidbody>().AddForceAtPosition(buoyancy, point);
            }
            
        }
        
    }

    //On trigger enter check if the collider is water, then, activate physics simulation
    void OnTriggerStay (Collider collider) 
    {
        if (!currentWaterTransform && (collider.tag == "Water" || collider.name == "Water"))
        {
            currentWaterTransform = collider.transform;
            triggered = true;
        } 
    }

    //Only deactivate physics on trigger exit and only if the trigger is the same than the current water's collider
    void OnTriggerExit(Collider collider)
    {
        if (currentWaterTransform && collider == currentWaterTransform.GetComponent<Collider>())
        {
            triggered = false;
            currentWaterTransform = null;
        }
    }

}
