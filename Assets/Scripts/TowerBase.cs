using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class TowerBase : NetworkBehaviour
{
    public GameObject impactFx;
    public GameObject[] enemyList;
    public GameObject nearestEnemy;
    public Transform target;
    public float range = 15f;
    public float damage = 10f;
    void Start()
    {
        InvokeRepeating(nameof(FindTarget), 0f,0.2f);
        InvokeRepeating(nameof(Shoot),0f,1f);
    }
    private void Update()
    {
        if(target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookrotation.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void FindTarget()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
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
        if(nearestEnemy != null && shortestDistance <= range) 
        {
            target = nearestEnemy.transform;
        }
    }
    void Shoot()
    {
        if (target == null) return;
        Enemy enemy= target.GetComponent<Enemy>();
        enemy.Damage(damage);
        Instantiate(impactFx, new Vector3(target.position.x,target.position.y+0.5f,target.position.z), Quaternion.LookRotation(target.forward));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
