using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banquete : Gun
{
    public override void Shoot()
    {
        base.Shoot();
        if(enemy!= null)
        {
            player.Hp += (damageDealt / 10);
            if(player.Hp >= player.MaxHp)
            {
                player.Hp = player.MaxHp;
            }
        }
    }
}
