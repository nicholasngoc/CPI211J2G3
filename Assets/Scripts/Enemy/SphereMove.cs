using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereMove : MonoBehaviour
{
    public Transform currentTarget;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 30 == 0) 
            agent.SetDestination(currentTarget.position);
    }
}
