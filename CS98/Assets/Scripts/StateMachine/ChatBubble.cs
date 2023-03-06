using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{

    public void Create(string fruit)
    {
        // TODO: try "setActive"
        //chatBubbleTrans = Instantiate(sprite_popup, parent);
        //backgroundRenderer = sprite_popup.transform.Find("background").GetComponent<SpriteRenderer>();
        //fruitRenderer = sprite_popup.GetComponent<SpriteRenderer>();
        spriteSwitch fruit_swtich = GetComponentInChildren<spriteSwitch>();
        fruit_swtich.Create(fruit);

        //chatBubbleTrans.transform.rotation = Quaternion.LookRotation(chatBubbleTrans.transform.position - cameraTransform.position);
    }

    private void Awake()
    {
        //backgroundRenderer = transform.Find("background").GetComponent<SpriteRenderer>();
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
