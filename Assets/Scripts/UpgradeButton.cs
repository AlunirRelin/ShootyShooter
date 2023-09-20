using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeButton : MonoBehaviour
{
    public Action<int> Upgrade;
    public int index;

    public void Button()
    {
        Upgrade(index);
    }
}
