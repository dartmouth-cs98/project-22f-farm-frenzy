using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSSDeadZone : MonoBehaviour
{
    private MSSWaveSystem waveSystem;

    void Start(){
        waveSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<MSSWaveSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if(other.gameObject.tag == "Enemy"){
            if(waveSystem.hits > 0){
                waveSystem.hits --;
            }else{
                waveSystem.GameOver();
            }
        }
    }
}
