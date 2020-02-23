using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public int bulletCount;
    public float fireRate;
    public float bulletSpeed;

    public virtual void Shoot()
    {
        for(int x = 0; x < bulletCount; x++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(bulletSpawnPoint.forward * bulletSpeed);
        }
    }
}
