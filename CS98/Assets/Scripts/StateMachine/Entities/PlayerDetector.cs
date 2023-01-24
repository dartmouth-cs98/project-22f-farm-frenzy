using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool playerInRange => _detectedPlayer != null;
    public bool fruitInRange => _detectedFruit != null;

    public GameObject _detectedFruit;

    public PlayerControllerRagdoll _detectedPlayer;


    private void OnTriggerEnter(Collider other)
    {
        // other.GetComponent<PlayerControllerRagdoll>()
        if (_detectedPlayer == null && other.gameObject.tag == "Player")
        {
            _detectedPlayer = other.GetComponent<PlayerControllerRagdoll>();
            // get pick up obj
            //if (_detectedPlayer.GetComponentInChildren<GameObject>)
        }
        if (_detectedFruit == null && other.gameObject.tag == "Scorable")
        {
            _detectedFruit = other.GetComponent<GameObject>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControllerRagdoll>() == _detectedPlayer)
        {
            //StartCoroutine(DetectedPlayerAfterDelay());
            _detectedPlayer = null;
        }
        if (other.gameObject.tag == "Scorable" && _detectedFruit)
        {
            //StartCoroutine(DetectedPlayerAfterDelay());
            _detectedFruit = null;
        }
    }

    //private IEnumerator DetectedPlayerAfterDelay()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    _detectedPlayer = null;
    //}

    public Vector3 GetNearestPlayerPosition()
    {
        return _detectedPlayer?.transform.position ?? Vector3.zero;
    }
}
