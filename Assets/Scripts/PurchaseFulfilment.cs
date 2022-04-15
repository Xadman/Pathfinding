using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseFulfilment : MonoBehaviour
{
    private Player player;


    private void Awake()
    {
        player = Player.Instance;
    }

    public void GrantCoins(int coinsToGrant)
    {
        player.coins += coinsToGrant;
        SaveSystem.SavePlayer(player);
    }
}
