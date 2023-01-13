using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Shopper : MonoBehaviour
{
    private StateMachine _stateMachine;
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        //var playerDetector = gameObject.AddComponent<PlayerDetector>();

        _stateMachine = new StateMachine();

        // state inits
        var roam = new Roam(this, navMeshAgent, animator);
        

        // transitions



        // func bool checks
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);



        // start state
        _stateMachine.SetState(roam);
        Debug.Log("state " + _stateMachine._currentState);
        //StartCoroutine(waiter());
        

    }

    // Update is called once per frame
    private void Update() => _stateMachine.Tick();


    //IEnumerator waiter()
    //{
    //    yield return new WaitForSeconds(10);
    //    _stateMachine._currentState.OnExit();
    //}
}
