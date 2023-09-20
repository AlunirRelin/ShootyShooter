using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApplySlow : Gun
{
    public Slow enemySlow;
    public float timer;
    public float debuff;
    public override void Shoot()
    {
      base.Shoot();
        if(enemy!=null && enemy.Hp> 0)
        {
            enemySlow = enemy.gameObject.AddComponent<Slow>();
            enemySlow.timer = timer;
            enemySlow.speedDebuff = debuff;
        }
    }
}
