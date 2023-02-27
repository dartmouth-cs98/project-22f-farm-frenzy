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

    private int stunTime;

    public Animator animator;

    [SerializeField]
    private GameObject jumpFX;

    private bool dashing;
    private float speedMultiplier = 1;
    public bool isDead;

    // for scores
    public int knockouts = 0, scored_fruits = 0, seed_planted = 0, fruit_trade = 0;
    public bool onPlanting = false;


    public void OnMove(InputAction.CallbackContext context)
    {
        if (stunTime <= 0)
        {
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

                //Vector3 newPosition = new Vector3(signX * move.x, 0.0f, signY * move.y);

                //rb.gameObject.transform.LookAt(newPosition + transform.position);


                // Normal Map: x, y
                // Current Map: y, -x
                //Vector3 newPosition = new Vector3(move.y, 0.0f, -move.x);
                //transform.LookAt(newPosition + transform.position);
            }
        }
        //else {
        //    Debug.Log("ooooooh i'm stunned");
        //}
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
        targetVelocity *= CalculateSpeed();

        //Align direction (Do not use if rotating character manually, otherwise movement gets messed up)
        //targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
        //Clamp forces
        Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);


    }

    public float CalculateSpeed()
    {
        return speed * speedMultiplier;
    }

    public void SetSpeedMultiplier(float mult)
    {
        speedMultiplier = mult;
    }


    public void Jump()
    {
        Vector3 jumpForces = Vector3.zero;

        if (grounded && !isDead)
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
        jumpFX.GetComponent<ParticleSystem>().Stop();
        jumpFX.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<AudioManager>().PlayAudio("JumpSound");

    }


    public void SetGrounded(bool state)
    {
        grounded = state;
    }
  private void FixedUpdate()
    {
        
        Move();
        rb.AddForce(Vector3.down * gravity * rb.mass);
        if(isDead)
        {
            GetComponentInChildren<PickUpObject>().dropItem();
        }

    }

    public void getStun(int sec)
    {
        stunTime = sec;
        //animator.SetBool("Idle", false);
        //animator.SetTrigger("Knock Out");

        //JointDrive drive = new JointDrive();
        //drive.positionSpring = 0;
        //Debug.Log("turning to 0 spring");
        //hipJoint.angularXDrive = drive;
        //hipJoint.angularYZDrive = drive;
        //PlayerControllerRagdoll controller = GetComponent<PlayerControllerRagdoll>();
        //controller.enabled = false;
        isDead = true;
        StartCoroutine(Jointwaiter());

        InvokeRepeating("stunCountDown", 0f, 1f);
    }

    private void stunCountDown()
    {
        stunTime = stunTime - 1;
        if (stunTime < 0)
        {
            PlayerControllerRagdoll controller = GetComponent<PlayerControllerRagdoll>();
            controller.enabled = true;

            animator.SetBool("Idle", true);
            //animator.SetBool("Knock Out", false);
            animator.ResetTrigger("Knock Out");

            JointDrive drive = new JointDrive();
            //drive.maximumForce = original_force;
            drive.maximumForce = Mathf.Infinity; ;
            drive.positionSpring = 500;
            Debug.Log("turning back");
            hipJoint.angularXDrive = drive;
            hipJoint.angularYZDrive = drive;
            isDead = false;
            CancelInvoke("stunCountDown");
        }
    }

    IEnumerator Jointwaiter()
    {
        //animator.SetBool("Idle", false);
        //animator.SetBool("Walking", false);
        //animator.SetBool("Jump", true);

        ////Wait for 4 seconds
        //yield return new WaitForSecondsRealtime(2);

        //animator.SetBool("Idle", true);
        //animator.SetBool("Jump", false);
        PlayerControllerRagdoll controller = GetComponent<PlayerControllerRagdoll>();
        controller.enabled = false;

        animator.SetBool("Idle", false);
        animator.SetTrigger("Knock Out");

        yield return new WaitForSecondsRealtime(0.8f);

        JointDrive drive = new JointDrive();
        drive.positionSpring = 0;
        Debug.Log("turning to 0 spring");
        hipJoint.angularXDrive = drive;
        hipJoint.angularYZDrive = drive;

        //PlayerControllerRagdoll controller = GetComponent<PlayerControllerRagdoll>();
        //controller.enabled = false;
    }
}
