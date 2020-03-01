using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CylinderMove : MonoBehaviour
{
    public Transform currentTarget;

    private NavMeshAgent agent;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 30 == 0)
        {
            agent.SetDestination(currentTarget.position);
            body.AddForce(new Vector3(0, 200, 0));
        }

        //if (agent.velocity.y == 0)
            //agent.velocity = new Vector3(agent.velocity.x, 20, agent.velocity.z);
    }
}
