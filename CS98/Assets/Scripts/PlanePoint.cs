using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanePoint : MonoBehaviour
{

    public TMP_Text scoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        updateScoreUI();
        
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
            updateScoreUI();

        }

    }

    private void updateScoreUI() {
        scoreAmount.text = KeepScore.Score.ToString("0");

    }
}
