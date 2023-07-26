using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;

    public float damage = 10f;
    public float range = 100f;

    public void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
        cam= go.GetComponent<Camera>();
    }
    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("a");
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Damage(damage);
            }
        }
    }
}
