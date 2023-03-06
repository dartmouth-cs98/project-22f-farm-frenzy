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
    [SerializeField] private GameObject dieFX;

    // to change
    public String fruit_wanted;
    public Vector3 birthplace = new Vector3(0, 0, 1);

    private void Awake()
    {
        
        var playerDetector = gameObject.AddComponent<PlayerDetector>();
        var _chatBubble = gameObject.GetComponent<ChatBubble>();
        fruit_wanted = null;

        _stateMachine = new StateMachine();

        // state inits
        var roam = new Roam(this, navMeshAgent, animator, playerDetector, _chatBubble);
        var seePlayer = new SeePlayer(this, navMeshAgent, playerDetector, animator, this);
        var exit = new Exit(this, navMeshAgent, animator, dieFX, this);


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
        
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Tick();
    }

    public void Die()
    {
        //playFX();
        //dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        //dieFX.GetComponent<ParticleSystem>().Stop();
        //dieFX.GetComponent<ParticleSystem>().Play();
        //StartCoroutine(testFunction());
        Debug.Log("in shopper: die");
        Destroy(this);
        //StartCoroutine("testFunction");

        //Destroy(gameObject);
    }

    public IEnumerator DoCoroutine(IEnumerator cor)
    {
        while (cor.MoveNext())
            yield return cor.Current;
    }

    private IEnumerator testFunction()
    {
        playFX();
        dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        dieFX.GetComponent<ParticleSystem>().Stop();
        dieFX.GetComponent<ParticleSystem>().Play();
        Debug.Log("here");
        yield return new WaitForSeconds(1f);
        Debug.Log("herehereherehere");
      
        Destroy(gameObject);
    }

    public void playFX(){
        dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        dieFX.GetComponent<ParticleSystem>().Stop();
        dieFX.GetComponent<ParticleSystem>().Play();
    }
}
