using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_OOB_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector3(Random.Range(-10, -5), 5, 20);

        }

    }
}