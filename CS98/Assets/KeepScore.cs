using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeepScore : MonoBehaviour
{
    public static int Score = 0;
    public static int RedScore = 0;
    public static int BlueScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        RedScore = 0;
        BlueScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI() {

    }
}
