using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private GameObject _camera;
    private Transform cameraTransform;
    [SerializeField] private GameObject appleSprite;
    [SerializeField] private GameObject carrotSprite;
    private SpriteRenderer backgroundRenderer;
    private SpriteRenderer fruitRenderer;
    public GameObject sprite_popup;
    GameObject chatBubbleTrans;

    public enum FruitType {
        Apple,
        Carrot
    }

    public void Create(string fruit)
    {
        // TODO: try "setActive"
        //chatBubbleTrans = Instantiate(sprite_popup, parent);
        //backgroundRenderer = sprite_popup.transform.Find("background").GetComponent<SpriteRenderer>();
        //fruitRenderer = sprite_popup.GetComponent<SpriteRenderer>();
        Vector3 offset = new Vector3(0, 2, 0);
        if (fruit == "apple")
        {
            chatBubbleTrans = Instantiate(appleSprite, transform.position + offset, transform.rotation);
            //fruitRenderer.sprite = GetFruitSprite(FruitType.Apple);
        }
        else
        {
            chatBubbleTrans = Instantiate(carrotSprite, transform.position + offset, transform.rotation);
            //fruitRenderer.sprite = GetFruitSprite(FruitType.Carrot);
        }

        //chatBubbleTrans.transform.rotation = Quaternion.LookRotation(chatBubbleTrans.transform.position - cameraTransform.position);
    }

    private void Awake()
    {
        //backgroundRenderer = transform.Find("background").GetComponent<SpriteRenderer>();
        //fruitRenderer = transform.Find("fruit").GetComponent<SpriteRenderer>();
        //cameraTransform = Camera.main.transform;
        //_camera = GameObject.FindGameObjectsWithTag("camera")[0];
        //cameraTransform = _camera.transform;
    }

    //private void Setup(FruitType fruitType)
    //{
    //    fruitRenderer.sprite = GetFruitSprite(fruitType);
    //}

    //private Sprite GetFruitSprite(FruitType fruitType)
    //{
    //    switch (fruitType) {
    //        default:
    //        case FruitType.Apple: return appleSprite;
    //            break;
    //        case FruitType.Carrot: return carrotSprite;
    //            break;
    //    }
    //}

    private void Update()
    {
        //if (chatBubbleTrans) chatBubbleTrans.transform.rotation = Quaternion.LookRotation(chatBubbleTrans.transform.position - cameraTransform.position);
    }

}
