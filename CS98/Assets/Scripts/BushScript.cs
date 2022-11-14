using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour
{
    public float slowdownSpeed = .5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //first we make sure that the object that hit the banana is the player.
        if (other.tag == "PlayerParent")
        {
            other.gameObject.SendMessage("SetSpeedMultiplier", .5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //first we make sure that the object that hit the banana is the player.
        if (other.tag == "PlayerParent")
        {
            other.gameObject.SendMessage("SetSpeedMultiplier", 1);
        }
    }
}
