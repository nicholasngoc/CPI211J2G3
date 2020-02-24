using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This controls the firing of guns and which gun
/// is selected
/// </summary>
public class GunController : MonoBehaviour
{
    public Gun[] gunSelection;  //Array of guns that can be swapped to
    private int _currentGun = 0;    //Index used for gunSelection to determine which gun we are using
    public Transform bulletSpawnPoint;  //Reference obj for where the bullets will spawn

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
        //Fires on left click
        if (Input.GetMouseButton(0) && _canShoot)
        {
            gunSelection[_currentGun].Shoot();
            StartCoroutine(ShootDelay(gunSelection[_currentGun].fireRate));
        }

        //Switches the gun by pressing Q. Can be changed
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

    /// <summary>
    /// Coroutine that waits for the specified period of time to be able to shoot again
    /// </summary>
    /// <param name="delay">How long to wait to shoot again</param>
    /// <returns></returns>
    private IEnumerator ShootDelay(float delay)
    {
        _canShoot = false;

        yield return new WaitForSeconds(delay);

        _canShoot = true;

        yield return null;
    }
}
