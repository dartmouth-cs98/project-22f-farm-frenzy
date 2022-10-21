using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CartoonFX;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed, maxForce, jumpForce;
    public Vector2 move = new Vector2(0,0);
    public bool grounded;


    [SerializeField]
    private GameObject jumpFX;

    private bool dashing;

    public void OnMove(InputAction.CallbackContext context)
    {
        if(move != context.ReadValue<Vector2>()) {
            move = context.ReadValue<Vector2>();

            Vector3 newPosition = new Vector3(move.x, 0.0f, move.y);
            transform.LookAt(newPosition + transform.position);
        }
        

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
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
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

        rb.AddForce(jumpForces, ForceMode.VelocityChange);
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

    }
}
