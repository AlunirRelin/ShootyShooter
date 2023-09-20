using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suppresor : TowerBase
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
                tps = 4;
                upgraded[2] = true;
                CancelInvoke();
                InvokeRepeating(nameof(Shoot), 0f, tps);
                break;
            case 3:
                timer = 2f;
                break;
        }
    }
}
