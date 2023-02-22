using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : MonoBehaviour
{
    private void Update()
    {
        if(gameObject.transform.parent != null)
        {
            if (gameObject.GetComponent<Outline>() != null && gameObject.transform.parent.name == "HatSpot")
            {

                gameObject.GetComponent<Outline>().enabled = false;
            }
        }
        
    }
}
