using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using Unity.VisualScripting;

public class Enemy : NetworkBehaviour
{
    public float Hp;
    public float maxHp = 20;
    public List<Transform> patrolPoints;
    public NavMeshAgent agent;
    public float aggroRange;
    public Player targetPlayer;
    public GameObject[] enemyList;
    [SerializeField]
    private float stopDistance;
    public Animator anim;
    public SphereCollider attRange;
    public float attCooldown;
    public float attTime;
    public float attDamage;
    void Start()
    {
        anim = GetComponent<Animator>();
        Hp = maxHp;
        GameObject patrolPointsList = GameObject.FindGameObjectWithTag("PatrolPoints");
        foreach(Transform child in patrolPointsList.transform)
        {
            patrolPoints.Add(child);
        }
        agent.destination = patrolPoints[0].position;
        InvokeRepeating(nameof(FindTarget), 0f, 0.1f);
    }
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= stopDistance && patrolPoints.Count> 0)
        {
            if (patrolPoints.Count > 1)
            {
                agent.destination = patrolPoints[1].position;
            }
            patrolPoints.RemoveAt(0);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
    void FindTarget()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemyList)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= aggroRange)
        {
            agent.destination = nearestEnemy.transform.position;
            targetPlayer = nearestEnemy.GetComponent<Player>();
        }
    }
    public IEnumerator Attack()
    {
     agent.isStopped = true;
     yield return new WaitForSeconds(attTime);
     agent.isStopped = false;
     
    }
    public void OnTriggerStay(Collider other)
    {
        attCooldown -= Time.deltaTime;
        StartCoroutine(Attack());
        if (attCooldown <= 0)
        {
            targetPlayer.Damage(attDamage);
            attCooldown = attTime;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == targetPlayer.gameObject)
        {
            attCooldown = attTime;
        }
    }
}