using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; //reference to your hands/the position where you want your object to go
    public GameObject myHat; //reference to your hands/the position where you want your object to go
    bool canpickup; //a bool to see if you can or cant pick up the item
    GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    bool hasItem; // a bool to see if you have an item in your hand
    bool hasHat;
                  // Start is called before the first frame update
    void Start()
    {
        canpickup = false;    //setting both to false
        hasItem = false;
    }

    
    public void PickUp(InputAction.CallbackContext context)
    {
        //Object is a hat
        if(ObjectIwantToPickUp.GetComponent<HatScript>() != null)
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
            ObjectIwantToPickUp.GetComponent<Rigidbody>().mass = 0;   //makes the rigidbody not be acted upon by forces
            (ObjectIwantToPickUp.GetComponent(typeof(Collider)) as Collider).enabled = false;
            ObjectIwantToPickUp.transform.position = myHat.transform.position; // sets the position of the object to your hand position
            ObjectIwantToPickUp.transform.rotation = myHat.transform.rotation; // sets the position of the object to your hand position
            ObjectIwantToPickUp.transform.parent = myHat.transform; //makes the object become a child of the parent so that it moves with the hands
            hasHat = true;

        }

        else if (context.action.triggered && canpickup && !hasItem) // if you enter thecollider of the objecct
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
            (ObjectIwantToPickUp.GetComponent(typeof(Collider)) as Collider).isTrigger = false;
            if(ObjectIwantToPickUp.GetComponent<WalkScript>() != null)
            {
                ObjectIwantToPickUp.GetComponent<WalkScript>().enabled = false;
            }
            ObjectIwantToPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
            ObjectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
            hasItem = true;
        }
        else if(context.action.triggered && hasItem)
        {
            dropItem();
        }
    }

    public void dropItem()
    {
        if(hasItem)
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again

            ObjectIwantToPickUp.transform.parent = null; // make the object not be a child of the hands

            if (ObjectIwantToPickUp.GetComponent<WalkScript>() != null)
            {
                // Moves again when planted
                //ObjectIwantToPickUp.GetComponent<WalkScript>().enabled = true;
            }

            hasItem = false;
        }
        
    }
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if ((other.gameObject.CompareTag("object") || other.gameObject.CompareTag("Scorable")) && !hasItem) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
            other.gameObject.AddComponent<Outline>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("object") || other.gameObject.CompareTag("Scorable")) && !hasItem) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
            other.gameObject.AddComponent<Outline>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canpickup = false; //when you leave the collider set the canpickup bool to false
        if(other.gameObject.GetComponent<Outline>())
        {
            Destroy(other.gameObject.GetComponent<Outline>());
        }


    }

    private void FixedUpdate()
    {
         if(myHands.transform.childCount == 0 && hasItem)
        {
            ObjectIwantToPickUp = null;
            hasItem = false;
        }
    }
}

