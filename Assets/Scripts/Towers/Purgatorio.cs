using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purgatorio : TowerBase
{
    public override void UpdateTower(int index)
    {
        switch (index)
        {
            case 0:
                range += 5;
                break;
            case 1:
                damage += 3;
                break;
            case 2:
                tps = 0.07f;
                upgraded[2] = true;
                CancelInvoke();
                InvokeRepeating(nameof(Shoot), 0f, tps);
                break;
            case 3:
                type = TowerBase.TowerType.AOE;
                radius = 10;
                upgraded[3] = true;
                break;
        }
    }
}
