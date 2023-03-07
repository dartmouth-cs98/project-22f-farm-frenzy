using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{

    public void Create(string fruit)
    {
        //fruitRenderer = sprite_popup.GetComponent<SpriteRenderer>();
        spriteSwitch fruit_swtich = GetComponentInChildren<spriteSwitch>();
        fruit_swtich.Create(fruit);
    }

    public void DestroySprite()
    {
        Destroy(gameObject);
        //Destroy(gameObject.GetComponent<Sprite>());
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

}
