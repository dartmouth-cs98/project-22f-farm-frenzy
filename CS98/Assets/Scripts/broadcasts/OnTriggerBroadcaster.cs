using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerBroadcaster : MonoBehaviour
{
// This callback is broadcast to all listeners during OnTriggerEnter/Exit.
    public UnityAction<Collider, Collider> onTriggerEntered;
    public UnityAction<Collider, Collider> onTriggerExited;
 
    public UnityAction<Collider, Collision> onCollisionEntered;
    public UnityAction<Collider, Collision> onCollisionExited;
 
    private void OnTriggerEnter(Collider other)
    {
        // If there are any listeners...
        if (onTriggerEntered != null)
        {
            // ...broadcast the callback.
            onTriggerEntered(GetComponent<Collider>(), other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // If there are any listeners...
        if (onTriggerExited != null)
        {
            // ...broadcast the callback.
            onTriggerExited(GetComponent<Collider>(), other);
        }
    }
 
    private void OnCollisionEnter(Collision other)
    {
        // If there are any listeners...
        if (onCollisionEntered != null)
        {
            // ...broadcast the callback.
            onCollisionEntered(GetComponent<Collider>(), other);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        // If there are any listeners...
        if (onCollisionExited != null)
        {
            // ...broadcast the callback.
            onCollisionExited(GetComponent<Collider>(), other);
        }
    }
 
}
 
//Honestly this script is basically plug and play. Just put it on the Game Object that contains the trigger you're trying to connect too.
 
//The Thread this system is based on. https://forum.unity.com/threads/checking-for-a-specific-collider.815118/
 
