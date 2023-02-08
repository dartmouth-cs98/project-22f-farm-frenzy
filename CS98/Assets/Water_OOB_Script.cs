using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_OOB_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform SpawnPosition;
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = SpawnPosition.position;

        }
        else if(collision.gameObject.tag == "object")
        {
            collision.gameObject.transform.position = SpawnPosition.position;
        }

    }
}
