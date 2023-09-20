using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Micelio : TowerBase
{
    public float heal;
    public override void UpdateTower(int index)
    {
        switch (index)
        {
            case 0:
                range += 5;
                radius += 5;
                upgraded[0] = true;
                break;
            case 1:
                damage += 3;
                break;
            case 2:
                tps -= 0.2f;
                if (tps <= 0.5)
                {
                    upgraded[2] = true;
                }
                CancelInvoke();
                InvokeRepeating(nameof(Shoot), 0f, tps);
                break;
            case 3:
                upgraded[3] = true;
                break;
        }
    }
    protected override void Shoot()
    {
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
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Enemy>().Damage(damage);
                Player player = collider.GetComponent<Player>();
                player.Hp += 5;
                if (player.Hp > player.MaxHp)
                {
                    player.Hp = player.MaxHp;
                }
            }
        }
    }
}
