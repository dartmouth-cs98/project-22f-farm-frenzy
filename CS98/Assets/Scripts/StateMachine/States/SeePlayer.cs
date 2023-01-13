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
        // UI text pop up

        // trade funct
    }

    public void OnExit()
    {
        tradeComplete = false;
    }
}
