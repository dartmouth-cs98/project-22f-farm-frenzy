using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : IState
{
    private readonly Shopper _shopper;
    // Start is called before the first frame update


    public Die(Shopper shopper)
    {
        _shopper = shopper;
    }

    public void Tick()
    {

    }

    public void OnEnter()
    {
        Debug.Log("die state!");
        //_shopper.GetComponent<ChatBubble>().DestroySprite();
    }

    public void OnExit()
    {
       
    }
}
