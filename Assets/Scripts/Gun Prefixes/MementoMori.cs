using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MementoMori : Gun
{
    public float stackTimer = 5;
    public int stacks;
    public override void Shoot()
    {
        base.Shoot();
        if(enemy!=null && enemy.Hp <= 0)
        {
            stackTimer = 5;
            if (stacks <= 4)
            {
                stacks++;
            }
            reloadSpeed = 1.5f * (10 - stacks) / 10;
            Debug.Log(reloadSpeed);
        }
    }
    public void FixedUpdate()
    {
        stackTimer -= Time.deltaTime;
        if(stackTimer <= 0)
        {
            stacks = 0;
            reloadSpeed = 1.5f;
        }
    }
}
