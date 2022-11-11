using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public CameraMultiTarget cameraMultiTarget;

    private List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame updates

    // Update is called once per frame
    void LateUpdate()
    {
    
    }

    public void OnPlayerJoined() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("one") ;
        foreach (GameObject p in players)
        {
            targets.Add(p);
        }
        cameraMultiTarget.SetTargets(targets.ToArray());
    }

}
