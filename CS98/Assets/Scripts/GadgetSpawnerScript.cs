using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshCollider))]
public class GadgetSpawnerScript : MonoBehaviour
{
    private bool gadgetAvailable = true;
    public string gadgetName = "Default";
    public string gadgetType = "Passive";
    public float respawnTime = 5f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<GadgetManagerScript>().canPickupGadget() && gadgetAvailable) 
        {
            gadgetAvailable = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            if(other.gameObject.GetComponent<GadgetManagerScript>().getGadget().SequenceEqual(new string[] { "None", "Passive" }))
            {
                other.gameObject.GetComponent<GadgetManagerScript>().setGadget(new string[] { gadgetName, gadgetType });
            }
            Invoke("respawnGadget", respawnTime);
            other.gameObject.GetComponent<GadgetManagerScript>().Invoke("resetGadget", other.gameObject.GetComponent<GadgetManagerScript>().gadgetTimer);

        }
    }

    private void respawnGadget()
    {
        gadgetAvailable = true;
        this.GetComponent<MeshRenderer>().enabled = true;


    }
}
