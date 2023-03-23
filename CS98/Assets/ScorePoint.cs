using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Math;

public class ScorePoint : MonoBehaviour
{

    public TMP_Text scoreAmount;
    public TMP_Text timer;
    public GameObject scoreFX;

    private float currentTime = 300;
    private bool timerStarted = false; 
    [SerializeField] float startTime;


    // Start is called before the first frame update
    void Start()
    {
        updateScoreUI();
        currentTime = startTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerDialogue.isGameStarted) {
            timerStarted = true;
        }

        if (timerStarted) {
            currentTime -= Time.deltaTime;
            timer.text = currentTime.ToString("f1");
        }

        if (currentTime <= 0) {
            timerStarted = false;
            currentTime = 300;
        }
        
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
            FindObjectOfType<AudioManager>().PlayAudio("ScoreSound");
            int amountToScore = 1;
            if(collision.gameObject.GetComponent<SpecialFruitScript>() != null)
            {
                amountToScore += 1;
            }
            if (tag == "Red")
            {
                KeepScore.RedScore += amountToScore;
            }
            else if (tag == "Blue")
            {
                KeepScore.BlueScore += amountToScore;
            }
            else
            {
                KeepScore.Score += amountToScore;
            }

            Debug.Log("collision");
            scoreFX.GetComponent<ParticleSystem>().Play();
            Destroy(collision.gameObject);
            updateScoreUI();

        }

    }

    private void updateScoreUI() {
        if (tag == "Red")
        {
            scoreAmount.text = KeepScore.RedScore.ToString("0");
        }
        else if (tag == "Blue")
        {
            scoreAmount.text = KeepScore.BlueScore.ToString("0");
        }
        else
        {
            scoreAmount.text = KeepScore.Score.ToString("0");
        }

    }
}
