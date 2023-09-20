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
    public float radius;
    public float damage = 10f;
    public float tps;
    public Slow enemySlow;
    public float timer;
    public float debuff;
    public enum TowerType {ST,AOE,NoTarget};
    public TowerType type;
    protected bool[] upgraded = new bool[4];
    void Start()
    {
        InvokeRepeating(nameof(FindTarget), 0f,0.2f);
        InvokeRepeating(nameof(Shoot),0f,tps);
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
    protected virtual void Shoot()
    {
        if (target == null) return;
        switch (type)
        {
            case TowerType.ST:
                ShootST();
                break;
            case TowerType.AOE:
                ShootAOE();
                break;
            case TowerType.NoTarget:
                ShootSelf();
                break;
            default:
                break;
        }
        
    }
    void ShootST()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Damage(damage);
        Instantiate(impactFx, new Vector3(target.position.x, target.position.y + 0.5f, target.position.z), Quaternion.LookRotation(target.forward));
        enemySlow = enemy.gameObject.AddComponent<Slow>();
        enemySlow.timer = timer;
        enemySlow.speedDebuff = debuff;
    }
    void ShootAOE()
    {
        if(target == null) return;
        Collider[] colliders = Physics.OverlapSphere(target.position, radius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().Damage(damage);
                Enemy enemy = collider.GetComponent<Enemy>();
                enemySlow = enemy.gameObject.AddComponent<Slow>();
                enemySlow.timer = timer;
                enemySlow.speedDebuff = debuff;
            }
        }
    }
    void ShootSelf()
    {
        if (target == null) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().Damage(damage);
                Enemy enemy = collider.GetComponent<Enemy>();
                enemySlow = enemy.gameObject.AddComponent<Slow>();
                enemySlow.timer = timer;
                enemySlow.speedDebuff = debuff;
            }
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public virtual void UpdateTower(int index)
    {
       Debug.Log("isso ? um TowerBase e n?o contem upgrades");
       Debug.Log(index);
    }
}
