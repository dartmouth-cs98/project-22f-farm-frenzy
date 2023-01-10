using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_OOB_Script : MonoBehaviour
{
    public Transform PlayerRespawnPoint;
    public Transform SeedRespawnPoint;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = PlayerRespawnPoint.position;

        }
        else if(collision.gameObject.tag == "object")
        {
            collision.gameObject.transform.position = SeedRespawnPoint.position;
        }

    }
}
