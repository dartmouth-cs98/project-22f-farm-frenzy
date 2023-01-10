using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask IgnoreMe;
    public PlayerControllerRagdoll playerController;
    public Collider mainCollider;
    public float distToGround;

    public void Start()
    {
        playerController.SetGrounded(false);
        distToGround = mainCollider.bounds.extents.y;

    }

    /*public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == playerController.gameObject || other.tag == "Scorable" || other.tag == "Bush")
        {
            return;
        }
        playerController.SetGrounded(true);

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGrounded(false);

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGrounded(true);

    }*/

    private bool IsGrounded()  
    {
        Debug.DrawRay(transform.position, -transform.up * .4f, Color.green, duration: 1);
        //Debug.Log("Raycasting");
        return Physics.Raycast(transform.position, -Vector3.up, (float) (distToGround + .4), ~IgnoreMe);
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            playerController.SetGrounded(true);

        }
        else
        {
            playerController.SetGrounded(false);

        }
    }
}
