using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManagerScript : MonoBehaviour
{

    public string[] currentGadget = { "None", "Passive" };
    public GameObject currentPlayer;
    private float startingJump, startingSpeed;
    public float gadgetTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startingJump = currentPlayer.GetComponent<PlayerController>().jumpForce;
        startingSpeed = currentPlayer.GetComponent<PlayerController>().speed;

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
        currentPlayer.GetComponent<PlayerController>().jumpForce = startingJump;
        currentPlayer.GetComponent<PlayerController>().speed = startingSpeed;

    }

    private void shieldHandler()
    {
        throw new NotImplementedException();
    }

    private void speedBoostHandler()
    {
        currentPlayer.GetComponent<PlayerController>().speed = startingSpeed * 2f;
    }

    private void highJumpHandler()
    {
        currentPlayer.GetComponent<PlayerController>().jumpForce = startingJump * 2f;
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
}
