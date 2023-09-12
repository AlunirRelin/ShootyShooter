using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slow : MonoBehaviour
{
    public NavMeshAgent agent;
    public float oldSpeed;
    public float timer;
    public float speedDebuff;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        oldSpeed = agent.speed;
        agent.speed = (agent.speed * (100 - speedDebuff)) / 100;
    }
    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            agent.speed = oldSpeed;
            Destroy(this);
        }
    }

}
