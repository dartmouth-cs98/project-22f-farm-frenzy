using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GadgetSpawnerScript : MonoBehaviour
{
    private bool gadgetAvailable = true;
    public string gadgetName = "Default";
    public string gadgetType = "Passive";

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "Player" && gadgetAvailable) 
        {
            gadgetAvailable = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            if(other.gameObject.GetComponent<GadgetManagerScript>().getGadget().SequenceEqual(new string[] { "Null", "Passive" }))
            {
                other.gameObject.GetComponent<GadgetManagerScript>().setGadget(new string[] { gadgetName, gadgetType });
            }
            Invoke("respawnGadget", 5);


        }
    }

    private void respawnGadget()
    {
        gadgetAvailable = true;
        this.GetComponent<MeshRenderer>().enabled = true;


    }
}
