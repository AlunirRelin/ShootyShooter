using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public NetworkManagerHUD networkManagerHUD;
    Vector3 direction = new Vector3(1,0,1);
    Rigidbody rb;
    public int[] resources;
    public float MaxHp;
    public float Hp;
    public float playerDamage = 1;
    public float playerReload = 1;
    public float playerTPS = 1;
    [Client]
    private void Start()
    {
        networkManagerHUD = GetComponent<NetworkManagerHUD>();
    }
    void Update()
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
    public void Damage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
        }
    }

}
