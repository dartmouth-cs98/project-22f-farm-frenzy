using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public void playerInteract()
    {
        FindObjectOfType<TriggerDialogue>().playerInteract();
    }
}
