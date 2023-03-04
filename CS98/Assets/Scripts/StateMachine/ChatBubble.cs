using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] private Sprite appleSprite;
    [SerializeField] private Sprite carrotSprite;
    private SpriteRenderer backgroundRenderer;
    private SpriteRenderer fruitRenderer;

    public enum FruitType {
        Apple,
        Carrot
    }

    public static void Create(Transform parent, Vector3 localPosition, FruitType fruitType)
    {
        Transform chatBubbleTrans = Instantiate(, parent);
        chatBubbleTrans.localPosition = localPosition;
        chatBubbleTrans.GetComponent<ChatBubble>().Setup(fruitType);
    }

    private void Awake()
    {
        backgroundRenderer = transform.Find("background").GetComponent<SpriteRenderer>();
        fruitRenderer = transform.Find("fruit").GetComponent<SpriteRenderer>();

    }

    private void Setup(FruitType fruitType)
    {
        fruitRenderer.sprite = GetFruitSprite(fruitType);
    }

    private Sprite GetFruitSprite(FruitType fruitType)
    {
        switch (fruitType) {
            default:
            case FruitType.Apple: return appleSprite;
                break;
            case FruitType.Carrot: return carrotSprite;
                break;
        }
    }

}
