using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerControllerRagdoll playerController;

    public void Start()
    {
        playerController.SetGrounded(false);

    }

    public void OnTriggerEnter(Collider other)
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

    }
}
