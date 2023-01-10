using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlanterScript : MonoBehaviour
{

    GameObject EnteringObject;
    public GameObject[] growables;
    bool spaceAvailable = true;

    float amplitude = 0.005f;
    float frequency = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    [SerializeField]
    private GameObject growFX;
    public float growingTime = 5f;


    private void FixedUpdate()
    {
        if(EnteringObject)
        {
            print("EnteringObject");
            if(EnteringObject.transform.parent != this.transform)
            {
                spaceAvailable = true;
                CancelInvoke();
                EnteringObject = null;
            }
            else
            {
                EnteringObject.transform.Rotate(new Vector3(0f, Time.deltaTime * 60, 0f), Space.World);

                // Float up/down with a Sin()
                tempPos = posOffset;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

                EnteringObject.transform.position = EnteringObject.transform.position + tempPos;
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
                Invoke("finishGrowing", growingTime);
            }
        }
    }

    private void finishGrowing()
    {
        if(EnteringObject.transform.parent == this.transform)
        {
            GameObject newObj = Instantiate(growables[Random.Range(0, growables.Length)]);
            newObj.transform.parent = EnteringObject.transform.parent;
            newObj.transform.position = EnteringObject.transform.position;
            if (EnteringObject) { Destroy(EnteringObject); }
            EnteringObject = newObj;
            (EnteringObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
            EnteringObject.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
            spaceAvailable = false; // Holding something
            playGrowFX();
        }
        

    }

    void playGrowFX()
    {
        growFX.GetComponent<ParticleSystem>().Stop();
        growFX.GetComponent<ParticleSystem>().Play();
    }
}
