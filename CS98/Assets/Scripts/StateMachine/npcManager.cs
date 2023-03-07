using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class npcManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopper;
    public string GameSceneName = "integrated_map";
    public float pos_x = -25.72f, pos_y = 1f, pos_z = -3.61f;
    public float timer_create = 1;
    public float interval = 30;     // interval between creating each shopper, needs to be long enough for the previous one
                                    // to return to birthplace, die and then pop a new one after a bit
    public float lifetimeOfEach = 5;    // life time of each shopper
    public float timer_destroy = 8;
    private GameObject shopper_created = null;
    private bool has_shopper = false;
    public bool start = false;
    [SerializeField] private GameObject dieFX;
    private Vector3 offset = new Vector3(0f, -4.5f, 0f);
    //[SerializeField] private static TriggerDialogue _triggerDialogue;


    void StartShopper()
    {
        shopper_created = Instantiate(shopper, new Vector3(pos_x, pos_y, pos_z), Quaternion.identity);
        has_shopper = true;
    }

    void Update()
    {
        // TODO: add some form of check to check if game starts, then set start to true
        if (TriggerDialogue.isGameStarted)
        {
            start = true;
        }
        timer_create -= Time.deltaTime;
        // check count down for shopper creation
        //if (timer_create <= 0)
        //{
        // uncomment below if checked game start or not
        if (timer_create <= 0 && start) {
            StartShopper();
            
            timer_create = interval;
        }
        // check within each shopper's life time
        if (has_shopper == true)
        {
            timer_destroy -= Time.deltaTime;

            if (timer_destroy <= 1.5)
            {
                shopper_created.GetComponentInChildren<ChatBubble>().Create("sleep");
                shopper_created.GetComponent<Shopper>().lifelimit = true;
                shopper_created.GetComponentInChildren<ChatBubble>().DestroySprite();
                // play fx effect
                shopper_created.GetComponent<dieDuck>().playFx();
                GameObject fx = Instantiate(dieFX, shopper_created.GetComponent<dieDuck>().transform.position + offset, Quaternion.identity);
                Destroy(fx, 1.2f);

                // refresh for next shopper
                    timer_destroy = lifetimeOfEach;
                has_shopper = false; 
            }
        }

    }
}
