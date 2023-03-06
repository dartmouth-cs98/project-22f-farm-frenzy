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
    private MonoBehaviour mono;
    GameObject _dieFX;

    public Exit(Shopper shopper, NavMeshAgent navMeshAgent, Animator animator, GameObject dieFX, MonoBehaviour monob)
    {
        _shopper = shopper;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _dieFX = dieFX;
        mono = monob;
    }

    public void Tick()
    {
        if (_navMeshAgent.remainingDistance < 0.05f)
        {
            // die!
            //    //_shopper.timeToDie = true;
            //    Debug.Log(_navMeshAgent.remainingDistance);
            //    Debug.Log(_navMeshAgent.destination);
                Debug.Log("back to birthplace");
            //_dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
            //_dieFX.GetComponent<ParticleSystem>().Stop();
            //_dieFX.GetComponent<ParticleSystem>().Play();
            _shopper.Die();
        }
    }

    public void OnEnter()
    {
        //TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        _navMeshAgent.destination = _shopper.birthplace;
        _animator.SetFloat(Speed, 1f);
        _animator.SetBool("walk", true);
        _animator.SetBool("idle", false);
        Debug.Log("state exiting.");
        _shopper.Die();
        //Debug.Log(_navMeshAgent.remainingDistance);

        //_dieFX.transform.localScale = new Vector3(.15f, .15f, .15f);
        //_dieFX.GetComponent<ParticleSystem>().Stop();
        //_dieFX.GetComponent<ParticleSystem>().Play();
    }

    public void OnExit()
    {
        Debug.Log("exit out of exit to birthplave");
    }

}
