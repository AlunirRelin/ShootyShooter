using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerShootingController : NetworkBehaviour
{
    public GameObject[] gunObject;
    public Gun gun;
    public void Update()
    {
        if (!isOwned) { return; }
        if (Input.GetMouseButton(0) && Time.time >= gun.FireCooldown)
        {
            gun.FireCooldown = Time.time + 1f / gun.fireRate;
            gun.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && gunObject[0].activeSelf)
        {
            gunObject[0].SetActive(false); gunObject[1].SetActive(true);
            gun = gunObject[1].GetComponent<Gun>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && gunObject[1].activeSelf)
        {
            gunObject[1].SetActive(false); gunObject[0].SetActive(true);
            gun = gunObject[0].GetComponent<Gun>();
        }
    }
}
