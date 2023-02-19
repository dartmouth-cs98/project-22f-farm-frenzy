using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    // Set player to correct spawn point on scene transition
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerParent");
        foreach (GameObject p in players)
        {
            if (transform != null)
            {
                p.transform.position = transform.position;

            }
        }
    }
}
