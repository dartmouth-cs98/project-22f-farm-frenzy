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
    public bool finished = false;
    private Vector3 dieplace = new Vector3(-22.6f, 6.13f, -4.5f);
    //private Vector3 final = new Vector3(1f,1f,0f);

    public Exit(Shopper shopper, NavMeshAgent navMeshAgent, Animator animator, GameObject dieFX)
    {
        _shopper = shopper;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick()
    {
        Debug.Log(_navMeshAgent.remainingDistance);
        if (_navMeshAgent.remainingDistance == 0)
        {
            finished = true;
        }
        Debug.Log(finished);
    }

    public void OnEnter()
    {
        Debug.Log("exit state!");
        _navMeshAgent.enabled = true;
        finished = false;
        _navMeshAgent.SetDestination(dieplace);
        _animator.SetFloat(Speed, .8f);
        _animator.SetBool("idle", false);
        _animator.SetBool("walk", true);
        //_shopper.GetComponent<ChatBubble>().DestroySprite();
    }

    public void OnExit()
    {
        Debug.Log("exit out of exit to birthplave");
        _animator.SetBool("idle", true);
        _animator.SetBool("walk", false);
        _navMeshAgent.enabled = false;
        finished = false;
    }

}
