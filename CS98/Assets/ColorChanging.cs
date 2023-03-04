using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ColorChanging : MonoBehaviour
{

    public ParticleSystem splash;
    public Material body;
    public Color changeColor = new Color(0.631f, 0.439f, 0.521f);
    public static int playersReady = 0;
    public static List<int> playersIDs = new List<int>();
    public TMP_Text playerCountUI;
    public static int numOfPlayers;

    private void Start()
    {
        playerCountUI.text = "Players Ready: ";

    }

    private void FixedUpdate()
    {
            numOfPlayers = GameObject.FindGameObjectsWithTag("PlayerParent").Length;

            playerCountUI.text = "Players Ready: " + playersIDs.Count + "/" + numOfPlayers;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GadgetManagerScript>() != null)
        {
            if(playersIDs.IndexOf(other.gameObject.GetInstanceID()) < 0)
            {
                playersIDs.Add(other.gameObject.GetInstanceID());
            }
            print("11111");
            splash.Play();
            GameObject duck = other.transform.parent.parent.Find("duck").gameObject;
            duck.transform.Find("body").GetComponent<Renderer>().material.color = changeColor;
            duck.transform.Find("leftHand").gameObject.GetComponent<Renderer>().material.color = changeColor;
            duck.transform.Find("rightHand").gameObject.GetComponent<Renderer>().material.color = changeColor;

            body.color = changeColor;
        }
    }

    public bool arePlayersReady()
    {
        if (playersIDs.Count > 0 && numOfPlayers > 0)
        {
            if(playersIDs.Count == numOfPlayers)
            {
                return true;
            }
        }
        return false;
    }
}
