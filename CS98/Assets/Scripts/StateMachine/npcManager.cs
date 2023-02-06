using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopper;
    public float pos_x, pos_y, pos_z;
    public float timer = 1;
    public float interval = 5;

    void Awake()
    {

    }


    void StartShopper()
    {
        Instantiate(shopper, new Vector3(pos_x, pos_y, pos_z), Quaternion.identity);
        //new_shopper.transform.position = new Vector3()
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            StartShopper();
            timer = interval;
        }
        
    }
}
