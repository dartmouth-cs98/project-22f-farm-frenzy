using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopper;
    public float pos_x = -25.72f, pos_y = 1f, pos_z = -3.61f;
    public float timer_create = 1;
    public float interval = 30;     // interval between creating each shopper, needs to be long enough for the previous one
                                    // to return to birthplace, die and then pop a new one after a bit
    public float lifetimeOfEach = 5;    // life time of each shopper
    public float timer_destroy = 8;
    private GameObject shopper_created = null;

    void Awake()
    {

    }


    void StartShopper()
    {
        shopper_created = Instantiate(shopper, new Vector3(pos_x, pos_y, pos_z), Quaternion.identity);
        //new_shopper.transform.position = new Vector3()
    }

    void Update()
    {
        timer_create -= Time.deltaTime;
        if (timer_create <= 0) {
            StartShopper();
            
            timer_create = interval;
        }
        if (shopper_created != null)
        {
            timer_destroy -= Time.deltaTime;
            if (timer_destroy <= 0)
            {
                //shopper_created.GetComponent<Shopper>().lifelimit = true;
                shopper_created.GetComponent<Shopper>().lifelimit = true;
                Destroy(shopper_created.GetComponent<Shopper>());
                Destroy(shopper_created.GetComponent<ChatBubble>());
                //Destroy(shopper_created.GetComponent<NavMeshAgent>());
                shopper_created.GetComponent<dieDuck>().playFx();
                shopper_created.GetComponent<dieDuck>().die = true;

                timer_destroy = lifetimeOfEach;
                shopper_created = null;
            }
        }

    }
}
