using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerShootingController : NetworkBehaviour
{
    public Gun gun;
    public void Update()
    {
        if (!isOwned) { return; }
        if (Input.GetMouseButton(0) && Time.time >= gun.FireCooldown)
        {
            gun.FireCooldown = Time.time + 1f / gun.fireRate;
            gun.Shoot();
        }
    }
}
