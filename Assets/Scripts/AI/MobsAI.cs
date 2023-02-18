using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MobsAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float changePositionTime = 5f;
    [SerializeField] private float moveDistance = 10f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(RandomNavSphere), changePositionTime, repeatRate: changePositionTime);
    }
    

    Vector3 RandomNavSphere(float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);

        return navHit.position;
    }

    private void MoveAnimal()
    {
        navMeshAgent.SetDestination(RandomNavSphere(moveDistance));
    }
}
