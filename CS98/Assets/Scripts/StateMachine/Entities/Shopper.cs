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
    public ChatBubble _chatBubble;
    public float lifetime;
    public bool lifelimit = false;
    public bool timeToDie = false;

    // to change
    public String fruit_wanted;
    public Vector3 birthplace = new Vector3(0,0,1);

    private void Awake()
    {
        var playerDetector = gameObject.AddComponent<PlayerDetector>();
        var _chatBubble = gameObject.GetComponent<ChatBubble>();
        fruit_wanted = null;

        _stateMachine = new StateMachine();

        // state inits
        var roam = new Roam(this, navMeshAgent, animator, playerDetector, _chatBubble);
        var seePlayer = new SeePlayer(this, playerDetector, animator);
        var exit = new Exit(this, navMeshAgent, animator);


        // transitions
        At(roam, seePlayer, HasTarget());
        At(seePlayer, roam, NoFruit());
        At(seePlayer, roam, TradeComplete());
        // transit from roam to see player
        At(seePlayer, roam, LostTarget());

        _stateMachine.AddAnyTransition(exit, () => lifelimit);

        // func bool checks
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> HasTarget() => () => playerDetector._detectedPlayer != null;
        Func<bool> NoFruit() => () => playerDetector._detectedPlayer != null && playerDetector._detectedFruit == null;
        Func<bool> TradeComplete() => () => seePlayer.tradeComplete;
        Func<bool> LostTarget() => () => playerDetector.playerInRange == false;

        // start state
        _stateMachine.SetState(roam);
        Debug.Log("state " + _stateMachine._currentState);
        //StartCoroutine(waiter());
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Tick();
    }

    public void Die()
    {
        Destroy(gameObject);
    }


    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(10);
        _stateMachine._currentState.OnExit();
    }
}
