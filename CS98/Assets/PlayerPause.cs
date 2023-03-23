using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPause : MonoBehaviour
{
    // Start is called before the first frame update
    public void playerPause()
    {
        try {
        FindObjectOfType<PauseMenuScript>().playerPause();
        }       
        catch (NullReferenceException ex) {
            Debug.Log("Exception");
        }
    }
}
