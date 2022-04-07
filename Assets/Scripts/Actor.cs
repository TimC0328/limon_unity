using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool hasDialog = false;
    public bool blockMovement = true;
    public float leftLimit = 0f;
    public float rightLimit = 100f;

    public Dialog dialog;
    public PlayDialog dialogPlayer;


    public void HandleCollision(Player player)
    {
        if (!blockMovement)
            return;
        if (hasDialog)
        {
            dialogPlayer.InitDialog(dialog, player);
            player.SetState(2);
        }
        if (leftLimit != 0f)
            player.leftLimit = leftLimit;
        else if (rightLimit != 0f)
            player.rightLimit = rightLimit;
        
    }

    
}
