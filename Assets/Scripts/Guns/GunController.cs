using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun[] gunSelection;
    private int _currentGun = 0;
    public Transform bulletSpawnPoint;

    private bool _canShoot = true;

    private void Start()
    {
        foreach(Gun gun in gunSelection)
        {
            gun.bulletSpawnPoint = bulletSpawnPoint;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _canShoot)
        {
            gunSelection[_currentGun].Shoot();
            StartCoroutine(ShootDelay(gunSelection[_currentGun].fireRate));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _currentGun++;

            if(_currentGun > gunSelection.Length - 1)
            {
                _currentGun = 0;
            }

            print("Current Gun = " + _currentGun);
        }
    }

    private IEnumerator ShootDelay(float delay)
    {
        _canShoot = false;

        yield return new WaitForSeconds(delay);

        _canShoot = true;

        yield return null;
    }
}
