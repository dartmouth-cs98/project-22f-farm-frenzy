using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.transform.name == "Capsule") {
            KeepScore.Score += 1;
            Debug.Log("collision");
            Destroy (gameObject);

        }

    }
}
