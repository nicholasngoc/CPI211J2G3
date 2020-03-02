using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple class that handles the player's health
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float _maxHealth;

    public AudioClip playerDamage;
    private AudioSource hurtAudio;
    private IEnumerator damageDelayRoutine;
    public float damageDelay;

    private void Awake()
    {
        hurtAudio = GetComponent<AudioSource>();
        hurtAudio.clip = playerDamage;
        _maxHealth = health;
        GameObject.Find("PlayerUI").GetComponent<UIControl>().maxHealthPlayer = _maxHealth;
        GameObject.Find("PlayerUI").GetComponent<UIControl>().currentHealthPlayer = health;
    }

    private void Update()
    {
        GameObject.FindGameObjectWithTag("HealthSlide").GetComponent<Slider>().value = health;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && damageDelayRoutine == null)
        {
            hurtAudio.Play();
            health--;

            if (health <= 0.0)
            {
                SceneManager.LoadScene("GameOver");
            }
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
