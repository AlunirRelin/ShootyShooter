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
        if (Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }
    }
}
