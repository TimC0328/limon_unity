using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool blockMovement = true;
    public bool hasDialog = false;

    public Dialog dialog;
    public PlayDialog dialogPlayer;

    public void HandleCollision(Player player)
    {
        if (hasDialog)
        {
            dialogPlayer.InitDialog(dialog, player);
            player.canMove = false;
        }
    }

    
}
