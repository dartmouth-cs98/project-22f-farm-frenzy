using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSSWaveSystem : MonoBehaviour
{
    public int hits = 10;
    public int wave = 1;

    public Text hitsText;
    public Text waveText;

    public GameObject gameOverTextObject;

    public SpawnerManager spawnerManager;

    private bool canCountwave = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hitsText.text = "Hits: "+hits.ToString();
        waveText.text = "Wave: "+wave.ToString();

        if(canCountwave == true && wave < 3){
            Invoke("StartWave", 10f);
            canCountwave = false;
        }
    }

    public void GameOver(){
        gameOverTextObject.SetActive(true);
        Time.timeScale = 0f;
    }

    void StartWave(){
        if(wave < 3){
            wave ++;
            spawnerManager.StartSpawner(wave);
            canCountwave = true;
        }
    }

}
