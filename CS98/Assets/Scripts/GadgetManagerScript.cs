using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManagerScript : MonoBehaviour
{

    public string[] currentGadget = { "None", "Passive" };
    public GameObject currentPlayer; // The gameobject to affect, in our ragdoll it is the overall parent
    private float startingJump, startingSpeed;
    public float gadgetTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startingJump = currentPlayer.GetComponent<PlayerControllerRagdoll>().jumpForce;
        startingSpeed = currentPlayer.GetComponent<PlayerControllerRagdoll>().speed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!String.Equals(currentGadget[0], "None") && String.Equals(currentGadget[1], "Active"))
        {
            //TODO
        }
        else if(!String.Equals(currentGadget[0], "None") && String.Equals(currentGadget[1], "Passive"))
        {
            switch(currentGadget[0])
            {
                case "HighJump":
                    highJumpHandler();
                    break;
                case "SpeedBoost":
                    speedBoostHandler();
                    break;
                case "Shield":
                    shieldHandler();
                    break;
                default:
                    break;
            }

        }
        else
        {
            resetDefaults();
        }
    }

    private void resetDefaults()
    {
        currentPlayer.GetComponent<PlayerControllerRagdoll>().jumpForce = startingJump;
        currentPlayer.GetComponent<PlayerControllerRagdoll>().speed = startingSpeed;

    }

    private void shieldHandler()
    {
        throw new NotImplementedException();
    }

    private void speedBoostHandler()
    {
        currentPlayer.GetComponent<PlayerControllerRagdoll>().speed = startingSpeed * 2f;
    }

    private void highJumpHandler()
    {
        currentPlayer.GetComponent<PlayerControllerRagdoll>().jumpForce = startingJump * 2f;
    }

    public void setGadget(string[] gadget)
    {
        currentGadget = gadget;
    }

    public string[] getGadget()
    {
        return currentGadget;
    }

    public void resetGadget()
    {
        setGadget(new string[] { "None", "Passive" });
        resetDefaults();
    }

    public bool canPickupGadget()
    {
        if (String.Equals(currentGadget[0], "None"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
