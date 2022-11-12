using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTest : MonoBehaviour
{
    private int stunTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stunTime > 0) {
            Debug.Log("ooooooh i'm stunned");
        }
    }

    public void getStun(int sec)
    {
        stunTime = sec;
        //animator.SetBool("Idle", false);
        //animator.SetBool("Knock Out", true);
        //PlayerControllerRagdoll controller = GetComponent<PlayerControllerRagdoll>();
        //controller.enabled = false;
        InvokeRepeating("stunCountDown", 0f, 1f);
    }

    private void stunCountDown()
    {
        stunTime = stunTime - 1;
        if (stunTime == 0)
        {
            //PlayerControllerRagdoll controller = GetComponent<PlayerControllerRagdoll>();
            //controller.enabled = true;
            //animator.SetBool("Idle", true);
            //animator.SetBool("Knock Out", false);
            CancelInvoke("stunCountDown");
        }
    }
}
