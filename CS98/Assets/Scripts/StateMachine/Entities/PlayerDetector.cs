using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool playerInRange => _detectedPlayer != null;
    public bool fruitInRange => _detectedFruit != null;

    public string _detectedFruit;

    public PlayerControllerRagdoll _detectedPlayer;


    private void OnTriggerEnter(Collider other)
    {
        // other.GetComponent<PlayerControllerRagdoll>()
        PlayerControllerRagdoll[] hitObj = other.transform.GetComponentsInParent<PlayerControllerRagdoll>();
        
        //if (hitObj.Length > 0) Debug.Log(hitObj[0]);
        if (_detectedPlayer == null && hitObj.Length>0 && hitObj[0].gameObject.tag == "PlayerParent")
        {
            Debug.Log("hit a duck");
            _detectedPlayer = hitObj[0].GetComponent<PlayerControllerRagdoll>();
            // get pick up obj
            //if (_detectedPlayer.GetComponentInChildren<GameObject>)
        }
        if (_detectedFruit == null && other.gameObject.tag == "Scorable")
        {
            Debug.Log("hit a fruit");
            _detectedFruit = other.name;
            Debug.Log(_detectedFruit);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // if the player leaves the collider area, then trade cannot continue
        // only when they continue to stay and then trade script will run and complete
        Debug.Log(other);
        PlayerControllerRagdoll[] hitObj = other.transform.GetComponentsInParent<PlayerControllerRagdoll>();
        if (_detectedPlayer == null && hitObj.Length > 0 && hitObj[0].GetComponent<PlayerControllerRagdoll>() == _detectedPlayer)
        {
            Debug.Log("byebye duck");
            //StartCoroutine(DetectedPlayerAfterDelay());
            _detectedPlayer = null;
        }
        if (other.gameObject.tag == "Scorable" && _detectedFruit!=null)
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
