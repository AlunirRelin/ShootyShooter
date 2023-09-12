using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fulgurante : Gun
{
    public float stackTimer = 5;
    public int stacks;
    public override void Shoot()
    {
        base.Shoot();
        if(enemy!=null)
        {
            stackTimer = 5;
            if (stacks < 35)
            {
                stacks++;
            }
            playerMovementController.moveSpeed = 17f / (100 - stacks) * 100;
        }
    }
    public void FixedUpdate()
    {
        stackTimer -= Time.deltaTime;
        if(stackTimer <= 0)
        {
            stacks = 0;
            playerMovementController.moveSpeed = 17f;
        }
    }
}
