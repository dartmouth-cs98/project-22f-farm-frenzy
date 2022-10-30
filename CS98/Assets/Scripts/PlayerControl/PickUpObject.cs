using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; //reference to your hands/the position where you want your object to go
    bool canpickup; //a bool to see if you can or cant pick up the item
    GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    bool hasItem; // a bool to see if you have an item in your hand
                  // Start is called before the first frame update
    void Start()
    {
        canpickup = false;    //setting both to false
        hasItem = false;
    }

    
    public void PickUp(InputAction.CallbackContext context)
    {
        if (context.action.triggered && canpickup && !hasItem) // if you enter thecollider of the objecct
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
            (ObjectIwantToPickUp.GetComponent(typeof(Collider)) as Collider).isTrigger = false;
            ObjectIwantToPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
            ObjectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
            hasItem = true;
        }
        else if(context.action.triggered && hasItem)
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again

            ObjectIwantToPickUp.transform.parent = null; // make the object no be a child of the hands

            hasItem = false;
        }
    }
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "object" && !hasItem) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canpickup = false; //when you leave the collider set the canpickup bool to false

    }
}

