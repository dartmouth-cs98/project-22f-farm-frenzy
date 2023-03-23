using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSSProjectile : MonoBehaviour
{
    private Rigidbody body;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddRelativeForce(Vector3.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }else if(other.gameObject.tag == "Obstacle"){
            Destroy(this.gameObject);
        }
    }
}
