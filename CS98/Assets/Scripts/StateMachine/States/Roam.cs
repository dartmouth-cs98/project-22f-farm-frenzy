using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

internal class Roam : IState
{
    private readonly Shopper _shopper;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    // need tuning, not sure
    private float walkRadius = 15f;


    public Roam(Shopper shopper, NavMeshAgent navMeshAgent, Animator animator)
    {
        _shopper = shopper;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
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
        _animator.SetFloat(Speed, 1f);

        // set fruit wanted
        if (_shopper.fruit_wanted == null) _shopper.fruit_wanted = null;    // set to a new fruit
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        Debug.Log("speed to 0");
        _animator.SetFloat(Speed, 0f);
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
