﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that controls the enemies health
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public int health;

    private AudioSource audio;

    public WaveSpawner spawner; //Reference to the spawner that created this enemy

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //If hit by a bullet and I am out of health kill me
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            audio.Play();

            if (health <= 0)
            {
                audio.Play();
                Destroy(gameObject);
            }

        }
    }

    /// <summary>
    /// This removes the reference to this object in the spawners
    /// list of spawned enemies
    /// </summary>
    private void OnDestroy()
    {
        spawner.spawnedEnemies.Remove(gameObject);
    }
}
