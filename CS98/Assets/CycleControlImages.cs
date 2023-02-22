using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleControlImages : MonoBehaviour
{

    public Sprite[] sprites;
    int pos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateSprite", 0f,3f);  //1s delay, repeat every 1s
        pos = 0;
    }

    // Update is called once per frame
    void UpdateSprite()
    {
        if(pos == sprites.Length)
        {
            pos = 0;
        }
        gameObject.GetComponent<Image>().sprite = sprites[pos];
        pos++;
    }
}
