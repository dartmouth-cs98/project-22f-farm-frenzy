using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour
{

    public GameObject controls;

    public void turnOffStartScreen()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    public void turnOnControls()
    {
        controls.SetActive(true);
    }
}
