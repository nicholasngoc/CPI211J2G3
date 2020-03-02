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

    public AudioClip baseDamage;
    private AudioSource baseAudio;
    public Transform currentTarget;
    public float redirectDelay;

    [Header("Base Damage")]
    private IEnumerator _baseDamageDelayRoutine;
    public float baseDamageDelayTime;

    private void Awake()
    {
        _baseDamageDelayRoutine = null;
    }

    private void Start()
    {
        baseAudio = GetComponent<AudioSource>();
        StartCoroutine(RedirectRoutine());
        baseAudio.clip = baseDamage;
    }

    /// <summary>
    /// Damages the base if they are colliding and the damage delay
    /// is not active
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base") && _baseDamageDelayRoutine == null)
        {
            collision.gameObject.GetComponent<BaseController>().health--;
            baseAudio.Play();

            _baseDamageDelayRoutine = BaseDamagDelay();
            StartCoroutine(_baseDamageDelayRoutine);
        }
    }

    /// <summary>
    /// Coroutine that adds a delay to the enemy damaging the base
    /// </summary>
    /// <returns></returns>
    private IEnumerator BaseDamagDelay()
    {
        yield return new WaitForSeconds(baseDamageDelayTime);

        _baseDamageDelayRoutine = null;

        yield return null;
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
