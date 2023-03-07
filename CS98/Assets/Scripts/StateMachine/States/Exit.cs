using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exit : IState
{
    private readonly Shopper _shopper;
    private readonly NavMeshAgent _navMeshAgent;
    public bool finished = false;


    public Exit(Shopper shopper, NavMeshAgent navMeshAgent)
    {
        _shopper = shopper;
        _navMeshAgent = navMeshAgent;

    }

    public void Tick()
    {
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = false;
        //finished = false;
    }

    public void OnExit()
    {

    }

}
