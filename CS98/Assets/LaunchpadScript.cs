using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchpadScript : MonoBehaviour
{
    public Transform launchDirection;
    public float launchForce = 1;

    private PlayerControllerRagdoll player_ontop = null;
    private GameObject fruit_ontop = null;

    private void OnTriggerEnter(Collider other)
    {
        // Scorable and has no parent, meaning it is not currently carried
        if (other.gameObject.tag == "Scorable" && other.transform.parent == null)
        {
            fruit_ontop = other.gameObject;
            FindObjectOfType<AudioManager>().PlayAudio("LaunchSound");
            other.gameObject.GetComponent<Rigidbody>().AddForce((launchDirection.position - other.transform.position) * launchForce, ForceMode.Impulse);
            if (fruit_ontop && player_ontop)
            {
                player_ontop.scored_fruits++;
                Debug.Log("fruit score" + player_ontop.scored_fruits);
            }
        }
        if (other.gameObject.tag == "Player" && player_ontop == null)
        {
            player_ontop = other.GetComponentInParent<PlayerControllerRagdoll>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponentInParent<PlayerControllerRagdoll>() == player_ontop)
        {
            player_ontop = null;
        }

        if (other.gameObject.tag == "Scorable" && other.gameObject == fruit_ontop) {
            fruit_ontop = null;
        }
    }
}
