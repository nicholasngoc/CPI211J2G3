using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This controls the firing of guns and which gun
/// is selected
/// </summary>
public class GunController : MonoBehaviour
{
    public PlayerController playerController;
    public Transform cameraTransform;

    [Header("Gun and Bullet Stuff")]
    public Gun[] gunSelection;  //Array of guns that can be swapped to
    public GameObject[] gunModels;
    private int _currentGun = 0;    //Index used for gunSelection to determine which gun we are using
    public Transform bulletSpawnPoint;  //Reference obj for where the bullets will spawn

    [Header("Recoil")]
    private float _currentRecoilAngle;
    public float recoilRecover; //This modifies how fast the player recovers from the recoil shot
    private bool _canShoot = true;

    private void Start()
    {
        foreach(Gun gun in gunSelection)
        {
            gun.bulletSpawnPoint = bulletSpawnPoint;
            gun.Reload();
        }
    }

    private void Update()
    {
        //Fires on left click. Gun must have bullets in clip
        if (Input.GetMouseButton(0) && _canShoot && gunSelection[_currentGun].clipCount > 0)
        {
            gunSelection[_currentGun].Shoot();
            StartCoroutine(ShootDelay(gunSelection[_currentGun].fireRate));

            //Disabled parts of the recoil system
            //If to make sure the recoil won't go over the set max vertical angle
            //if (Mathf.Abs(cameraTransform.eulerAngles.x - _currentRecoilAngle) < playerController.maxVerticalAngle || Mathf.Abs(cameraTransform.eulerAngles.x - _currentRecoilAngle) > 360 - playerController.maxVerticalAngle)
            //{
            //    _currentRecoilAngle += gunSelection[_currentGun].recoilAngle;
            //    cameraTransform.eulerAngles -= new Vector3(_currentRecoilAngle, 0, 0);
            //}
        }

        //Disabled parts of the recoil system
        //This block handles recoil recovery
        //if(_currentRecoilAngle > 0)
        //{
        //    cameraTransform.eulerAngles += new Vector3(recoilRecover, 0, 0);
        //    _currentRecoilAngle -= recoilRecover;
        //}

        //Switches the gun by pressing Q. Can be changed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _currentGun++;

            if(_currentGun > gunSelection.Length - 1)
            {
                _currentGun = 0;
            }

            foreach(GameObject gun in gunModels)
            {
                gun.SetActive(false);
            }

            gunModels[_currentGun].SetActive(true);
        }

        //Reloads gun on R
        if(Input.GetKeyDown(KeyCode.R))
        {
            gunSelection[_currentGun].Reload();
        }

        GameObject.Find("PlayerUI").GetComponent<UIControl>().currentReload = gunSelection[_currentGun].clipCount;
        GameObject.Find("PlayerUI").GetComponent<UIControl>().mag = gunSelection[_currentGun].maxBullets;
        GameObject.Find("PlayerUI").GetComponent<UIControl>().currentGun = _currentGun;
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
