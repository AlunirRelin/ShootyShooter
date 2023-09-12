using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aurora : Gun
{
    public override void Shoot()
    {
        base.Shoot();
        if(enemy!= null && enemy.Hp == enemy.maxHp - damage)
        {
            enemy.Damage(damage /3);
            DmgText.text = (damage/3).ToString();
            dmgPos.y += 1;
            Instantiate(DmgText, dmgPos, gameObject.transform.rotation);
        }
    }
}
