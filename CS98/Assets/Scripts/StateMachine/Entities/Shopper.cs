using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Shopper : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        // navMesh for romaing
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();
        //var playerDetector = gameObject.AddComponent<PlayerDetector>();

        _stateMachine = new StateMachine();

        // state inits
        var roam = new Roam(this, navMeshAgent, animator);

        // transitions



        // func bool checks
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        
    }

    // Update is called once per frame
    private void Update() => _stateMachine.Tick();

}
