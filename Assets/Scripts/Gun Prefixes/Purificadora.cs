using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purificadora : Gun
{
    public void Update()
    {
        if (reloading)
        {
            playerMovementController.moveSpeed = 25;
        }
        else
        {
            playerMovementController.moveSpeed = 17;
        }
    }
}
