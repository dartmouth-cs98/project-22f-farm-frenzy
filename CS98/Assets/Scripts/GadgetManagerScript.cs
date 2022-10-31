using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManagerScript : MonoBehaviour
{

    public string[] currentGadget = { "None", "Passive" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void setGadget(string[] gadget)
    {
        currentGadget = gadget;
    }

    public string[] getGadget()
    {
        return currentGadget;
    }
}
