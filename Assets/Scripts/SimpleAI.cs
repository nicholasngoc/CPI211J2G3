using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Simple class for an AI. It constantly follows a target
/// </summary>
public class SimpleAI : MonoBehaviour
{
    public NavMeshAgent NavAgent
    {
        get
        {
            return GetComponent<NavMeshAgent>();
        }
    }
    public Transform currentTarget;
    public float redirectDelay;

    private void Start()
    {
        StartCoroutine(RedirectRoutine());
    }

    /// <summary>
    /// This coroutine continuosly adjusts the AI's destination
    /// with a delay (this helps performance)
    /// </summary>
    /// <returns></returns>
    private IEnumerator RedirectRoutine()
    {
        while(true)
        {
            NavAgent.SetDestination(currentTarget.position);

            yield return new WaitForSeconds(redirectDelay);
        }
    }
}
