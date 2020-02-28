using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General class that describes a gun. Meant to be inherited
/// to a child if a different style of gun is needed
/// </summary>
public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public int bulletCount;
    public float fireRate;
    public float bulletSpeed;
    public float recoilAngle;   //This is the angle added to the player camera when the gun is shot

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
