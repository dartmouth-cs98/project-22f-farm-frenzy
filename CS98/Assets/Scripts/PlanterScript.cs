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

    private bool seed_is_here = false;
    private PlayerControllerRagdoll player_ontop = null;

    private void Start()
    {
        growFX.GetComponent<ParticleSystem>().Stop();
        var main = growFX.GetComponent<ParticleSystem>().main;
        //main.simulationSpeed = growingTime / (2*main.duration);

    }


    private void FixedUpdate()
    {
        if(EnteringObject)
        {
            //print("EnteringObject");
            if(EnteringObject.transform.parent != this.transform)
            {
                spaceAvailable = true;
                seed_is_here = false;
                CancelInvoke();
                growFX.GetComponent<ParticleSystem>().Stop();
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
            seed_is_here = true;
            EnteringObject = other.gameObject;

            // Check if object is not held
            if(EnteringObject.transform.parent)
            {
                if (EnteringObject.transform.parent.tag == "Hand" || EnteringObject.transform.parent == null)   // so seed won't plant itself
                {
                    return;
                }
            }
            if(spaceAvailable && player_ontop)
            {
                if(EnteringObject.GetComponent<WalkScript>() != null) {
                    EnteringObject.GetComponent<WalkScript>().enabled = false;
                }
               // fruit score!
                if (seed_is_here && player_ontop)
                {
                    player_ontop.seed_planted++;
                    Debug.Log("seed planted score" + player_ontop.seed_planted);
                }
                EnteringObject.transform.parent = this.transform;
                EnteringObject.transform.position = this.transform.position + (new Vector3(0, 1, 0));
                (EnteringObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
                EnteringObject.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                spaceAvailable = false; // Holding something
                Invoke("finishGrowing", growingTime);
                playGrowFX();
            }
        }
        if (other.gameObject.tag == "Player" && player_ontop == null)
        {
            player_ontop = other.GetComponentInParent<PlayerControllerRagdoll>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponentInParent<PlayerControllerRagdoll>() == player_ontop)
        {
            player_ontop = null;
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
            growFX.GetComponent<ParticleSystem>().Stop();

        }


    }

    void playGrowFX()
    {
        growFX.GetComponent<ParticleSystem>().Stop();
        growFX.GetComponent<ParticleSystem>().Play();
    }
}
