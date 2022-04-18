using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.KevinNipper.Pathfinding;
public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player Instance
    {
        get { return instance; }

    }
    public string playerId;
    public int xpPoints;
    public Vector3 currentPosition;
    public int coins;

    public Player(Player player)
    {
        playerId = player.playerId;
        coins = player.coins;
        level = player.level;
        xpPoints = player.xpPoints;
        currentPosition = player.currentPosition;
        coins = player.coins;

    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void GrantCoins(int coinsToGrant)
    {
        coins += coinsToGrant;
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayerData(SavePlayerData data)

    {
        playerId = data.playerId;
        coins = data.coins;
        level = data.level;
        xpPoints = data.xpPoints;
        currentPosition = new Vector3(data.currentPositionX, data.currentPositionY, data.currentPositionZ);
     
    }

    public void ResetPlayerData()
    {
        playerId = "";
        coins = 0;
        level = 1;
        xpPoints = 0;
        currentPosition = new Vector3(0, 1.3f, 0);
       
    }

private void OnApplicationQuit()
    {
        SaveSystem.SavePlayer(instance);
        CloudSaveSample.CloudSaveSample.
    }
}

