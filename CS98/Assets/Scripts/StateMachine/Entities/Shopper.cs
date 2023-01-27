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
    //public PlayerDetector playerDetector;

    // to change
    public String fruit_wanted;

    private void Awake()
    {
        var playerDetector = gameObject.AddComponent<PlayerDetector>();
        //var playerDetector = gameObject.AddComponent<PlayerDetector>();
        fruit_wanted = null;

        _stateMachine = new StateMachine();

        // state inits
        var roam = new Roam(this, navMeshAgent, animator, playerDetector);
        var seePlayer = new SeePlayer(this, playerDetector, animator);


        // transitions
        _stateMachine.AddAnyTransition(roam, () => playerDetector.playerInRange == false);
        At(roam, seePlayer, HasTarget());
        At(seePlayer, roam, NoFruit());
        At(seePlayer, roam, TradeComplete());
        // transit from roam to see player

        _stateMachine.AddAnyTransition(roam, () => !playerDetector.playerInRange);

        // func bool checks
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> HasTarget() => () => playerDetector._detectedPlayer != null;
        Func<bool> NoFruit() => () => playerDetector._detectedPlayer != null && playerDetector._detectedFruit == null;
        Func<bool> TradeComplete() => () => seePlayer.tradeComplete;


        // start state
        _stateMachine.SetState(roam);
        Debug.Log("state " + _stateMachine._currentState);
        //StartCoroutine(waiter());
    }

    // Update is called once per frame
    private void Update() => _stateMachine.Tick();


    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(10);
        _stateMachine._currentState.OnExit();
    }
}
