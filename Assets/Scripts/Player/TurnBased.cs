using System;
using UnityEngine;

public class TurnBased : MonoBehaviour
{
    [SerializeField] private PlayerController[] playerControllers;
    
    public void ChangeTurn(PlayerController playerController)
    {
        foreach (var player in playerControllers)
            if (player != playerController)
                player.allowInput = true;
        
        playerController.allowInput = false;
        print(playerController.allowInput);
    }
}
