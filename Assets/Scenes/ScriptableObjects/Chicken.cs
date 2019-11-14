using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Chicken : MonoBehaviour
{
    public float WonderRadius;
    public float WonderTimer;
    public float MovementSpeed;

    private NavMeshAgent Agent;
    private float Timer;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Timer = WonderTimer;
    }

    void Update()
    {
        //Below Line only used for testing at Run time.
        Agent.speed = MovementSpeed;

        Timer += Time.deltaTime;
        if (Timer >= WonderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, WonderRadius, -1);
            Agent.SetDestination(newPos);
            Timer = 0;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
