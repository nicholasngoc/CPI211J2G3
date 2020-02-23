using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun gun;
    public Transform bulletSpawnPoint;

    private void Start()
    {
        gun.bulletSpawnPoint = bulletSpawnPoint;
    }

    private void Update()
    {
        transform.localScale = transform.localScale;

        if (Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }
    }
}
