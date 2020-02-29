using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that controls the enemies health
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public int health;

    public WaveSpawner spawner; //Reference to the spawner that created this enemy

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);

        //If hit by a bullet and I am out of health kill me
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health--;

            if (health <= 0)
                Destroy(gameObject);
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
