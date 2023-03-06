using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSwitch : MonoBehaviour
{

    [SerializeField] private Sprite appleSprite;
    [SerializeField] private Sprite carrotSprite;
    [SerializeField] private SpriteRenderer fruitRenderer;

    //private SpriteRenderer fruitRenderer;

    public enum FruitType
    {
        Apple,
        Carrot
    }

    void Awake()
    {
       // fruitRenderer = transform.Find("fruit").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Create(string fruit)
    {
        if (fruit == "apple")
        {
            fruitRenderer.size += new Vector2(100f, 100f);
            fruitRenderer.sprite = appleSprite;
        }
        else
        {
            fruitRenderer.size -= new Vector2(1000f, 2000f);
            fruitRenderer.sprite = carrotSprite;
        }
    }

    private void Update()
    {
        //Camera myCamera = Camera.main;
        //transform.rotation = Quaternion.Euler(0f, myCamera.rotation.y, 0f);
    }
}
