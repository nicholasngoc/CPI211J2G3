﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modified class of Gun that fires a shotgun spread
/// </summary>
public class Shotgun : Gun
{
    public float forceModifier;    //This influences the force added onto the bullet on creation
    public float initRadius;    //This influences the position that the bullet spawn from the bulletSpawnPoint

    public override void Shoot()
    {
        for (int x = 0; x < bulletCount; x++)
        {  
            //Random angle and radius to for a spawn position
            float randRadius = Random.Range(0, initRadius);
            float randAngle = Random.Range(0, Mathf.PI * 2);

            Vector3 spawnPos = bulletSpawnPoint.position + new Vector3(randRadius * Mathf.Cos(randAngle), randRadius * Mathf.Sin(randAngle), 0);

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, bulletSpawnPoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Modified force the bullet will receive
                Vector3 direction = bulletSpawnPoint.forward + new Vector3(Random.Range(-forceModifier, forceModifier), Random.Range(-forceModifier, forceModifier), 0);
                rb.AddForce(direction * bulletSpeed);
            }
        }
    }
}
