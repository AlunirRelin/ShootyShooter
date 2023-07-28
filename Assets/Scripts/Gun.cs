using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactFx;
    public GameObject missFx;
    public GameObject DmgTextObj;
    public TextMeshPro DmgText;

    public float damage = 10f;
    public float fallofRange = 100f;
    public float fireRate = 15;
    public float FireCooldown = 0f;
    public void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
        cam= go.GetComponent<Camera>();
    }
    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, fallofRange))
        {
            Debug.Log("a");
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Debug.Log(hit.transform.name);
            if(enemy != null)
            {
                enemy.Damage(damage);
                Instantiate(impactFx, hit.point, Quaternion.LookRotation(hit.normal));
                Vector3 dmgPos = hit.point;
                dmgPos.y += 1;
                DmgText.text = damage.ToString();
                Instantiate(DmgText, dmgPos, gameObject.transform.rotation);
            }
            else
            {
                Instantiate(missFx, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
