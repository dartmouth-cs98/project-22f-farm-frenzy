using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exit : IState
{
    private readonly Shopper _shopper;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public Exit(Shopper shopper, NavMeshAgent navMeshAgent, Animator animator)
    {
        _shopper = shopper;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick()
    {
        if (_navMeshAgent.remainingDistance < 0.05f)
        {
            // die!
            //    //_shopper.timeToDie = true;
            //    Debug.Log(_navMeshAgent.remainingDistance);
            //    Debug.Log(_navMeshAgent.destination);
            //    Debug.Log("back to birthplace");
                _shopper.Die();
        }
    }

    public void OnEnter()
    {
        //TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        _navMeshAgent.destination = _shopper.birthplace;
        _animator.SetFloat(Speed, 1f);

        Debug.Log("state " + "exiting...!!!");
    }

    public void OnExit()
    {
        Debug.Log("exit out of exit to birthplave");
    }
}
