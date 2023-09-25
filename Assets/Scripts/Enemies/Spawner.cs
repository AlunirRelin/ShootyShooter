using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Spawner : NetworkBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] enemyPrefabs2;
    public GameObject[] enemyPrefabs3;
    public GameObject[] enemyPrefabs4;
    public GameObject[] enemyPrefabs5;
    public GameObject[] enemyPrefabsFinal;
    public GameObject[] currentList;
    public Transform spawnPoint;
    int SpawnCount = 1;
    public int Wave;
    public int Points;
    public float spawnSpeed = 4;
    public float timer;
    public TextMeshProUGUI waveText;
    void Start()
    {
        InvokeRepeating(nameof(ChangeWave), 0f, 1f);
        GameObject spawnerGO = GameObject.FindGameObjectWithTag("Wave");
        waveText = spawnerGO.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (GameManager.Instance.networkManagerFps.numPlayers >= 0 && timer <= 0)
        {
            int randomPath = Random.Range(-6, 6);
            Vector3 SpawnRandomPosition = new(spawnPoint.position.x + randomPath, spawnPoint.position.y, spawnPoint.position.z);
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)],SpawnRandomPosition, Quaternion.identity);
            enemy.name = enemy.name + " " + SpawnCount;
            enemy.GetComponent<Enemy>().randomPath = randomPath;
            SpawnCount++;
            timer = spawnSpeed;
        }
    }
     void ChangeWave() 
    {
        switch (Points)
        {
            case >500:
                currentList = enemyPrefabsFinal;
                waveText.text = "Onda: FINAL";
                break;
            case > 300:
                currentList = enemyPrefabs5;
                waveText.text = "Onda: 5";
                break;
            case > 200:
                currentList = enemyPrefabs4;
                waveText.text = "Onda: 4";
                break;
            case > 100:
                currentList = enemyPrefabs3;
                waveText.text = "Onda: 3";
                break;
            case > 50:
                currentList = enemyPrefabs2;
                waveText.text = "Onda: 2";
                break;
        }
    }
}
