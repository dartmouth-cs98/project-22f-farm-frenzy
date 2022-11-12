using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorePoint : MonoBehaviour
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

        if (collision.gameObject.tag == "Player") {
            KeepScore.Score += 1;
            Debug.Log("collision");
            Destroy (gameObject);
            updateScoreUI();

        }

    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Scorable")
        {
            KeepScore.Score += 1;
            Debug.Log("collision");
            Destroy(collision.gameObject);
            updateScoreUI();

        }

    }

    private void updateScoreUI() {
        scoreAmount.text = KeepScore.Score.ToString("0");

    }
}
