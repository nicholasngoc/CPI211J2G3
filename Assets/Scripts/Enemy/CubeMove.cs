using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public Transform currentTarget;
    private float chargeTime;
    private float chargeRadius;

    private UnityEngine.AI.NavMeshAgent agent;
    private float alarm;
    private bool charging;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(currentTarget.position);
        alarm = 0;
        charging = false;

        chargeTime = 2;
        chargeRadius = 20;

        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // not charging or finished charging
        if (alarm <= 0)
        {
            // turn agent on
            agent.isStopped = false;

            // if charging 
            if (charging)
            {
                // set max speed and max acceleration
                agent.speed = 10;
                agent.acceleration = 100;

                // if near target, finished charging 
                // thus reset flags, speed, and acceleration to chase mode
                if (agent.remainingDistance < 1)
                {
                    charging = false;
                    agent.speed = 5;
                    agent.acceleration = 10;
                }
            }
            else // else not charging
            {
                // only update every 30 frames or every half second
                if(Time.frameCount % 30 == 0)
                {
                    // recalculate path
                    agent.SetDestination(currentTarget.position);

                    // if near target, enter charging mode
                    if (agent.remainingDistance <= chargeRadius)
                    {
                        agent.isStopped = true;
                        charging = true;
                        alarm = chargeTime;
                    }
                }
            }
        }
        else // charging
        {
            // if target exited range, set flags to continue chasing
            if ((currentTarget.position - transform.position).magnitude > chargeRadius)
            {
                charging = false;
                alarm = 0;
            }
            else // else increment timer
                alarm -= Time.deltaTime;
        }
    }
}
