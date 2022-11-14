using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//The Main Script class as usual. REMEMBER TO RENAME THIS TO THE SCRIPT'S FILE NAME WHEN YOU COPY EVERYTHING.
public class MultiTriggerTestScript : OnTriggerListener
{
    public override void OnEnable() //Must Include.
    {
        TriggerInitialize(); //Must Include.
    }
 
    #region Trigger Broadcasters to Listen for.
    //Example Syntax of a TriggerBroadcaster object: ------- public OnTriggerBroadcaster trigger1;
    public OnTriggerBroadcaster trigger1;
    public OnTriggerBroadcaster trigger2;
    public OnTriggerBroadcaster trigger3;
    public PlayerControllerRagdoll controller;

    //for every TriggerBroadcaster object you must add it to the 'triggers.Add' portion of this override.
    void TriggerInitialize(){{if (triggers.Count == 0){
                //Here  VVVVVV
                triggers.Add(trigger1);
                triggers.Add(trigger2);
                triggers.Add(trigger3);
            }base.OnEnable();}}
    #endregion
 
    #region Surrogate Functions for OnTrigger/Collision Enter/Exit.
    //Stay is not included cause it's more preformant have the intended code in Update, and for it to only run when a switch is on that is turned on by Enter and turned off by Exit.
 
    //Once called they will return two variables, "activator" which is what the collider hit, (just like the normal Trigger Functions,) and "zone" which is the collider itself.
    //In order to tell which Collider is being activated check if zone = the intended TriggerBroadcasting object.
    //Individual functions can be left out if not neccesary.
 
    public override void OnTriggerEntered(Collider zone, Collider activator)
    {
    }
    public override void OnTriggerExited (Collider zone, Collider activator)
    {
        // Debug.Log(activator.name + " left zone " + zone.name);
    }
 
    public override void OnCollisionEntered(Collider zone, Collision activator)
    {
        //Debug.Log(activator.collider.name + " entered zone " + zone.name);
        //if(activator.collider.gameObject.CompareTag("Player") && (activator.collider.name == "Character1_Head"))
        if (!controller.isDead && activator.collider.gameObject.CompareTag("Player") && activator.collider.name == "Character1_Head")
        {
            GameObject ancestor = activator.collider.transform.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            if (!ancestor.GetComponent<PlayerControllerRagdoll>().isDead) {
                ancestor.GetComponent<PlayerControllerRagdoll>().getStun(5);
            }
        }

        if(activator.collider.CompareTag("Player"))
        {
            // Debug.Log("hit the enemy team-------!!");
        }
    }
    public override void OnCollisionExited(Collider zone, Collision activator)
    {

    }
 
    //IN CASE OF FUNCTION DELETION.
 
    //public override void OnTriggerEntered(Collider zone, Collider activator)
    //public override void OnTriggerExited (Collider zone, Collider activator)
    //public override void OnCollisionEntered(Collider zone, Collision activator)
    //public override void OnCollisionExited(Collider zone, Collision activator)
 
    #endregion
 
}
 
//The Thread this system is based on. https://forum.unity.com/threads/checking-for-a-specific-collider.815118/
 