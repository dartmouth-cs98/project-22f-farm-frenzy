using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

internal class Roam : IState
{
    private Shopper _shopper;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private readonly ChatBubble _chatBubble;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private readonly PlayerDetector _playerDetector;
    private static string[] fruits = { "apple", "carrot"};

    // need tuning, not sure
    private float walkRadius = 200f;


    public Roam(Shopper shopper, NavMeshAgent navMeshAgent, Animator animator, PlayerDetector playerDetector, ChatBubble chatbubble)
    {
        _shopper = shopper;
        _navMeshAgent = navMeshAgent;
        _playerDetector = playerDetector;
        _animator = animator;
        _chatBubble = chatbubble;
    }

    public void Tick()
    {
        if (_navMeshAgent.remainingDistance <= 0.8f)
        {
            _navMeshAgent.SetDestination(RandomNavMeshLocation());
        }        
    }

    public void OnEnter()
    {
        //TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(RandomNavMeshLocation());
        _animator.SetFloat(Speed, .8f);
        _animator.SetBool("idle", false);
        _animator.SetBool("walk", true);
        _playerDetector._detectedPlayer = null;
        // set fruit wanted
        if (_shopper.fruit_wanted == null)
        {
            int r = Random.Range(1, 3)-1;
            _shopper.fruit_wanted = fruits[r]; // set to a random new fruit
            //Debug.Log("roam, set fruit to: " + _shopper.fruit_wanted);
            _chatBubble.Create(_shopper.fruit_wanted);
        }
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetBool("idle", true);
        _animator.SetBool("walk", false);
        //_animator.SetFloat(Speed, 0f);
    }

    private Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPos = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * walkRadius;
        randomPos += _shopper.transform.position;
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, walkRadius, 1))
        {
            finalPos = hit.position;
        }

        return finalPos;
    }
}
