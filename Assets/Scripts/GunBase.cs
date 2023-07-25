using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Inputs;

[RequireComponent(typeof(Animator))]
public class GunBase : MonoBehaviour
{
    [SerializeField]
    private bool addBulletSpread = true;
    [SerializeField]
    private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem shootingSystem;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private ParticleSystem impactParticleSystem;
    [SerializeField]
    private TrailRenderer bulletTrail;
    [SerializeField]
    private float shootDelay = 0.5f;
    [SerializeField]
    private LayerMask mask;

    private Animator animator;
    private float lastShootTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Shoot()
    {
        if(lastShootTime + shootDelay < Time.time)
        {
            //animator.SetBool("IsShooting", true);
            shootingSystem.Play();
            Vector3 direction = GetDirection();

            if(Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                lastShootTime = Time.time;
            }
        }
    }
    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (addBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
                );
            direction.Normalize();
        }
        return direction;
    }
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while(time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        //animator.SetBool("IsShooting", false);
        Trail.transform.position = hit.point;
        Instantiate(impactParticleSystem,hit.point,Quaternion.LookRotation(hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }
}
