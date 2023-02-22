using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchpadScript : MonoBehaviour
{
    public Transform launchDirection;
    public float launchForce = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Scorable and has no parent, meaning it is not currently carried
        if(other.gameObject.tag == "Scorable" && other.transform.parent == null)
        {

            FindObjectOfType<AudioManager>().PlayAudio("LaunchSound");
            other.gameObject.GetComponent<Rigidbody>().AddForce((launchDirection.position - other.transform.position) * launchForce, ForceMode.Impulse);

        }
    }
}
