using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public CameraMultiTarget cameraMultiTarget;
    Renderer rend;

    private List<GameObject> targets = new List<GameObject>();
    private List<GameObject> playerNames = new List<GameObject>();
    private List<String> playerTeams = new List<String>();
    [SerializeField] private Material teamOne;
    [SerializeField] private Material teamTwo;
    public Material[] mat_list;


    // Start is called before the first frame updates
    void Start() {
        rend = GetComponent<Renderer>();
        rend.enabled = true; 
        rend.sharedMaterial = mat_list[0];
    }
        

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (GameObject p in playerNames) {
            if (playerTeams[count] == "Red") {
                PlayerControllerRagdoll.col = "Red";
            } else {
                // teamTwo.color = Color.blue;
                PlayerControllerRagdoll.col = "Blue";
                // rend.sharedMaterial = mat_list[2];
            }
            count +=1;
        }
    }
    

    public void OnPlayerJoined() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); 
        
 
        for (int i=0; i<players.Length; i++) {
            if (i % 2 == 0) {
                playerNames.Add(players[i]);
                playerTeams.Add("Red");
            } else {
                playerNames.Add(players[i]);
                playerTeams.Add("Blue");

            }
            targets.Add(players[i]);

        }
 
        cameraMultiTarget.SetTargets(targets.ToArray());
    }

    

}
