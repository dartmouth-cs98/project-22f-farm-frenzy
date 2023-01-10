using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MerchantNavScript : MonoBehaviour
{
    [SerializeField] private Transform[] movePositionTransformArray;
    private Transform currentTransform;
    private NavMeshAgent navMeshAgent;

    // Time
    private float nextActionTime = 5.0f; // Time to start
    public float period = 5.0f; // How long between intervals

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentTransform = movePositionTransformArray[Random.Range(0, movePositionTransformArray.Length)];
        navMeshAgent.destination = currentTransform.position;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            // execute block of code here

            currentTransform = movePositionTransformArray[Random.Range(0, movePositionTransformArray.Length)];
            navMeshAgent.destination = currentTransform.position;

        }
    }
}
