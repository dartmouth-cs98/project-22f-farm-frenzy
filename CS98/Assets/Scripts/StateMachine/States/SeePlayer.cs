using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SeePlayer : IState
{
    private readonly Shopper _shopper;
    private readonly PlayerDetector _playerDetector;
    private readonly Animator _animator;
    public bool tradeComplete = false;

    // stop moving, look at the player
    // if player has the correct fruit -> get it and score
    // if not, quit the state...??

    public SeePlayer(Shopper shopper, PlayerDetector playerDetector, Animator animator)
    {
        _shopper = shopper;
        _playerDetector = playerDetector;
        _animator = animator;
    }


    public void Tick()
    {

    }

    public void OnEnter()
    {
        // look at the player
        _shopper.transform.LookAt(_playerDetector._detectedPlayer.transform);

        /************ ui here *****************/


        // wait a bit for trading to happen?
        _shopper.StartCoroutine(TradeWait());
        // trade funct
        trade();
    }

    public void OnExit()
    {
        tradeComplete = false;
    }

    private void trade()
    {
        // check fruit
        // _shopper.fruit_wanted
        _shopper.fruit_wanted = null;
        tradeComplete = true;
    }

    private IEnumerator TradeWait()
    {
        yield return new WaitForSeconds(1);
    }
}
