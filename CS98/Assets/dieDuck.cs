using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieDuck : MonoBehaviour
{
    public void playFx()
    {
        
        // destroy duck shopper
        Destroy(this.gameObject, 2.2f);
    }

}
