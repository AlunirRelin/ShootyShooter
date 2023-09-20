using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadilha : TowerBase
{
    public override void UpdateTower(int index)
    {
        switch (index)
        {
            case 0:
                range += 5;
                radius += 5;
                break;
            case 1:
                damage += 3;
                break;
            case 2:
                if (tps <= 0.5)
                {
                    upgraded[2] = true;
                }
                CancelInvoke();
                InvokeRepeating(nameof(Shoot), 0f, tps);
                break;
            case 3:
                debuff = 20;
                upgraded[3] = true;
                break;
        }
    }
}
