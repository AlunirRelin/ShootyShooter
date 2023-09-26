using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class Player : NetworkBehaviour
{
    public NetworkManagerHUD networkManagerHUD;
    Vector3 direction = new Vector3(1,0,1);
    public int[] resources;
    public float MaxHp;
    public float Hp;
    public float playerDamage = 1;
    public float playerReload = 1;
    public float playerTPS = 1;
    public GameObject[] TowerSet;
    public Slider playerHP;
    public TextMeshProUGUI[] resourcesText;
    public Transform respawnPoint;
    [Client]
    private void Start()
    {
        networkManagerHUD = GetComponent<NetworkManagerHUD>();
        playerHP = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        GameObject resourceGo = GameObject.FindGameObjectWithTag("Resource");
        GameObject RespawnGO = GameObject.FindGameObjectWithTag("Resp");
        respawnPoint = RespawnGO.transform;
        int i = 0;
        foreach (Transform child in resourceGo.transform)
        {
            resourcesText[i] = child.gameObject.GetComponent<TextMeshProUGUI>();
            i++;
        }
        if (!isLocalPlayer) { return; }
            Debug.Log("parented");
            var children = gameObject.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                if( child.GetComponent<CinemachineVirtualCamera>() == null)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("TheSelf"); ;
                }
            }
    }
    void Update()
    {
        if(!isLocalPlayer){ return; }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        playerHP.value = Hp;
        resourcesText[0].text = resources[0].ToString(); 
        resourcesText[1].text = resources[1].ToString();
        resourcesText[2].text = resources[2].ToString();
        resourcesText[3].text = resources[3].ToString();
    }
    public void Damage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = MaxHp;
            gameObject.transform.position = respawnPoint.position;
        }
    }
    public bool TellIfOwned()
    {
        Debug.Log("telling");
        return isOwned;
    }
}
