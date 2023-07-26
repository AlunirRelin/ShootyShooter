using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using Unity.VisualScripting;

public class Enemy : NetworkBehaviour
{
    public float Hp = 20;
    public List<Transform> patrolPoints;
    public NavMeshAgent agent;
    [SerializeField]
    private float stopDistance;
    // Start is called before the first frame update
    void Start()
    {
        GameObject patrolPointsList = GameObject.FindGameObjectWithTag("PatrolPoints");
        foreach(Transform child in patrolPointsList.transform)
        {
            patrolPoints.Add(child);
        }
        agent.destination = patrolPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= stopDistance)
        {
            Debug.Log("turn");
            patrolPoints.RemoveAt(0);
            agent.destination = patrolPoints[0].position;
        }
    }
    public void Damage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
