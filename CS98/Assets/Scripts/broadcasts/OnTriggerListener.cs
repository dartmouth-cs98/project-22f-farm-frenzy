using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class OnTriggerListener : MonoBehaviour
{
//This is what the script uses to treat the triggers as if they were parts of a list.
    [HideInInspector]
    public List<OnTriggerBroadcaster> triggers = new List<OnTriggerBroadcaster>();
 
    //Handles the Activation and Deactivation of the Trigger system.
    public virtual void OnEnable()
    {
 
 
        //Subscribes to the callback
        foreach (OnTriggerBroadcaster trigger in triggers)
        {
            if (trigger != null)
            {
                trigger.onTriggerEntered += OnTriggerEntered;
                trigger.onTriggerExited +=  OnTriggerExited;
                trigger.onCollisionEntered += OnCollisionEntered;
                trigger.onCollisionExited +=  OnCollisionExited;
            }
        }
    }
    private void OnDisable()
    {
        // Unsubscribes from the callback.
        foreach (OnTriggerBroadcaster trigger in triggers)
        {
            if (trigger != null)
            {
                trigger.onTriggerEntered -= OnTriggerEntered;
                trigger.onTriggerExited -=  OnTriggerExited;
                trigger.onCollisionEntered -= OnCollisionEntered;
                trigger.onCollisionExited -=  OnCollisionExited;
            }
        }
    }
 
    //The Functions that are ran through the main Script once they recieve orders from the Broadcasters
    public virtual void OnTriggerEntered(Collider zone, Collider activator) { }
    public virtual void OnTriggerExited(Collider zone, Collider activator) { }
    public virtual void OnCollisionEntered(Collider zone, Collision activator) { }
    public virtual void OnCollisionExited(Collider zone, Collision activator) { }
 
 
}
//Pretty much plug and play except you don't actually have to plug it in to anywhere. Just leave it somewhere in the project and it will work.
 
//The Thread this system is based on. https://forum.unity.com/threads/checking-for-a-specific-collider.815118/

