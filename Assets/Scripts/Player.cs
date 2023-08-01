using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public NetworkManagerHUD networkManagerHUD;
    Vector3 direction = new Vector3(1,0,1);
    Rigidbody rb;
    [Client]
    private void Start()
    {
        networkManagerHUD = GetComponent<NetworkManagerHUD>();
    }
    void FixedUpdate()
    {
        if(!isOwned){ return; }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
