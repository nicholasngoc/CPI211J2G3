using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple class that handles the player's health
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float _maxHealth;

    private IEnumerator damageDelayRoutine;
    public float damageDelay;

    [Header("UI")]
    public Slider healthSlider;

    private void Awake()
    {
        _maxHealth = health;
    }

    private void Update()
    {
        if(healthSlider != null)
            healthSlider.value = health / _maxHealth;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && damageDelayRoutine == null)
        {
            health--;

            damageDelayRoutine = DamageDelay();
            StartCoroutine(damageDelayRoutine);
        }
    }

    private IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(damageDelay);

        damageDelayRoutine = null;

        yield return null;
    }
}
