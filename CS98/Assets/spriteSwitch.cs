using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSwitch : MonoBehaviour
{

    [SerializeField] private Sprite appleSprite;
    [SerializeField] private Sprite carrotSprite;
    [SerializeField] private SpriteRenderer fruitRenderer;
    private GameObject camera;
    //private SpriteRenderer fruitRenderer;

    public enum FruitType
    {
        Apple,
        Carrot
    }

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("camera").Length != 0)
            camera = GameObject.FindGameObjectsWithTag("camera")[0];
        Debug.Log(camera);
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
        //transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.y, 0f);
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }
}
