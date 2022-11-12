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

    public Animator animator;

    [SerializeField]
    private GameObject jumpFX;

    private bool dashing;

    public void OnMove(InputAction.CallbackContext context)
    {
        // if (stunTime <= 0) {
        if (move != context.ReadValue<Vector2>())
        {
            move = context.ReadValue<Vector2>();

            if (move != Vector2.zero)
            {
                Vector3 targetVelocity = new Vector3(signX * move.y, 0, signY * move.x);
                float targetAngle = Mathf.Atan2(targetVelocity.z, targetVelocity.x) * Mathf.Rad2Deg;
                // Debug.Log(targetAngle);
                this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle + 270f, 0f);

                animator.SetBool("Idle", false);
                animator.SetBool("Walking", true);
                animator.SetBool("Knock Out", false);
                animator.SetBool("Jump Attack", false);
                animator.SetBool("Punching", false);
                animator.SetBool("Jump", false);

            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Walking", false);
            }

            Vector3 newPosition = new Vector3(signX * move.x, 0.0f, signY * move.y);

            rb.gameObject.transform.LookAt(newPosition + transform.position);


            // Normal Map: x, y
            // Current Map: y, -x
            //Vector3 newPosition = new Vector3(move.y, 0.0f, -move.x);
            //transform.LookAt(newPosition + transform.position);
        }
        //else {
        //    animator.SetBool("Idle", true);
        //    animator.SetBool("Walking", false);
        //}
        // }
            
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

        Vector3 currentVelocity = this.GetComponent<Rigidbody>().velocity;
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
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            //animator.SetTrigger("Jump");
            jumpForces = Vector3.up * jumpForce;
            playJumpFX();
        }

        rb.AddForce(jumpForces, ForceMode.Impulse);
        //if (jumpForce >= 0)
        //{
        //    Jumpwaiter();
        //}

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

    }
    private void FixedUpdate()
    {
        
        Move();
        rb.AddForce(Vector3.down * gravity * rb.mass);

    }

    //IEnumerator Jumpwaiter()
    //{
    //    animator.SetBool("Idle", false);
    //    animator.SetBool("Walking", false);
    //    animator.SetBool("Jump", true);

    //    //Wait for 4 seconds
    //    yield return new WaitForSecondsRealtime(2);

    //    animator.SetBool("Idle", true);
    //    animator.SetBool("Jump", false);
    //}
}
