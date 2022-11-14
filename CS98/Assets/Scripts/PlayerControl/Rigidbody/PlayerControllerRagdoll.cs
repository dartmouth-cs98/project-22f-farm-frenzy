using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CartoonFX;
using System;

public class PlayerControllerRagdoll : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] private ConfigurableJoint hipJoint;
    public float speed, maxForce, jumpForce, gravity;
    public Vector2 move = new Vector2(0,0);
    public float signX, signY = 1;
    public bool grounded;
    Material rend;
    public Material[] mat_list;
    public static string col;
   


    [SerializeField]
    private GameObject jumpFX;

    private bool dashing;


    public void OnMove(InputAction.CallbackContext context)
    {
        if (move != context.ReadValue<Vector2>())
        {
            move = context.ReadValue<Vector2>();

            if (move != Vector2.zero)
            {
                Vector3 targetVelocity = new Vector3(signX * move.y, 0, signY * move.x);
                float targetAngle = Mathf.Atan2(targetVelocity.z, targetVelocity.x) * Mathf.Rad2Deg;
                this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle + 270f, 0f);
            }

            //Vector3 newPosition = new Vector3(signX * move.x, 0.0f, signY * move.y);

            //rb.gameObject.transform.LookAt(newPosition + transform.position);

            // Normal Map: x, y
            // Current Map: y, -x
            //Vector3 newPosition = new Vector3(move.y, 0.0f, -move.x);
            //rb.gameObject.transform.LookAt(newPosition + rb.transform.localPosition);
        }
            
        //if (context.ReadValue<Vector2>() != Vector2.zero)
        //{
        //        Vector3 currentVelocity = this.GetComponent<Rigidbody>().velocity;
        //        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        //        float targetAngle = Mathf.Atan2(targetVelocity.z, targetVelocity.x) * Mathf.Rad2Deg;
        //        Debug.Log(targetAngle);
        //        this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle + 270f, 0f);
            
        //}
        

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        dashing = context.action.triggered;
    }

    public void Move()
    {

        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(signX * move.y, 0, signY * move.x);

        // hip joint rotation
        //float targetAngle = Mathf.Atan2(targetVelocity.z, targetVelocity.x) * Mathf.Rad2Deg;
        //Debug.Log(targetAngle);
        //this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle + 270f, 0f);

        //Vector3 currentVelocity = rb.velocity;
        // Normal Map: x, y
        // Current Map: y, -x
        //Vector3 targetVelocity = new Vector3(move.y, 0, -move.x);
        targetVelocity *= speed;

        //Align direction (Do not use if rotating character manually, otherwise movement gets messed up)
        //targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
        //Clamp forces
        Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);


    }

    public void Jump()
    {
        Vector3 jumpForces = Vector3.zero;

        if (grounded)
        {
            jumpForces = Vector3.up * jumpForce;
            playJumpFX();
        }

        rb.AddForce(jumpForces, ForceMode.Impulse);
    }

    void playJumpFX()
    {
        jumpFX.transform.localScale = new Vector3(.15f, .15f, .15f);
        jumpFX.transform.localPosition = new Vector3(0, -.5f, 0);
        jumpFX.GetComponent<ParticleSystem>().Stop();
        jumpFX.GetComponent<ParticleSystem>().Play();
    }


    public void SetGrounded(bool state)
    {
        grounded = state;
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>().material;
        Debug.Log(rend);
        rend = mat_list[0];
        Debug.Log(rend);

    }
    private void FixedUpdate()
    {
        
        Move();
        rb.AddForce(Vector3.down * gravity * rb.mass);
        if (col == "Red") {
            rend = mat_list[1];
            Debug.Log(rend);
        } else if (col == "Blue") {
            rend = mat_list[2]; 
        } 


    }
}
