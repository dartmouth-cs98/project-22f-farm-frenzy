using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public void playerInteract()
    {
        try {
            FindObjectOfType<TriggerDialogue>().playerInteract();
        }       
        catch (NullReferenceException ex) {
            Debug.Log("Exception");
        }
        
    }
}
