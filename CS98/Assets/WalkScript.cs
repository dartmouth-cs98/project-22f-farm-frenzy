using UnityEngine;

public class WalkScript : MonoBehaviour
{
    public float speed = 1f;
    public float range = 10f;
    public float avoidDistance = 5f;
    public LayerMask avoidLayer;

    private Transform player;
    private Vector3 targetPosition;

    float closestDistance = float.MaxValue;
    Transform closestTransform;

    private void Start()
    {

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");


        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestTransform = target.transform;
                closestDistance = distance;
                player = target.transform;
            }
        }

        targetPosition = RandomPosition();
    }

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestTransform = target.transform;
                closestDistance = distance;
                player = target.transform;
            }
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = RandomPosition();
        }

        Vector3 direction = (targetPosition - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, avoidDistance, avoidLayer))
        {
            targetPosition = RandomPosition();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private Vector3 RandomPosition()
    {
        Vector3 randomPosition = player.position + Random.onUnitSphere * range;
        randomPosition.y = 7;
        return randomPosition;
    }
}
