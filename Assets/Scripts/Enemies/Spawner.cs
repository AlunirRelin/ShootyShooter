using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spawner : NetworkBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float spawnSpeed = 4;
    public float timer;
    void Start()
    {
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (GameManager.Instance.networkManagerFps.numPlayers > 0 && timer <= 0)
        {
            Instantiate(enemyPrefabs[0],spawnPoint);
            timer = spawnSpeed;
        }
    }
}
