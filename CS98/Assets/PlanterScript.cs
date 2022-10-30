using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanterScript : MonoBehaviour
{

    GameObject EnteringObject;
    public GameObject[] growables;
    bool spaceAvailable = true;

    private void FixedUpdate()
    {
        if(EnteringObject)
        {
            if(EnteringObject.transform.parent != this.transform)
            {
                spaceAvailable = true;
                CancelInvoke();
                EnteringObject = null;
            }
        }

    }


    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "object" && spaceAvailable) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            EnteringObject = other.gameObject;

            // Check if object is not held
            if(EnteringObject.transform.parent)
            {
                if (EnteringObject.transform.parent.tag == "Hand")
                {
                    return;
                }
            }
            if(spaceAvailable)
            {
                EnteringObject.transform.parent = this.transform;
                EnteringObject.transform.position = this.transform.position + (new Vector3(0, 1, 0));
                (EnteringObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
                EnteringObject.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                spaceAvailable = false; // Holding something
                Invoke("finishGrowing", 2);//this will happen after 2 seconds
            }
        }
    }

    private void finishGrowing()
    {
        if(EnteringObject.transform.parent == this.transform)
        {
            GameObject newObj = Instantiate(growables[0]);
            newObj.transform.parent = EnteringObject.transform.parent;
            newObj.transform.position = EnteringObject.transform.position;
            if (EnteringObject) { Destroy(EnteringObject); }
            spaceAvailable = true; // Holding something
        }
        

    }
}
