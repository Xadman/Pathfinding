using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player Instance
    {
        get { return instance; }
     
    }

    public int level;
    public int xpPoints;
    public Vector3 currentPosition;

    public Player(Player player)
    {

        level = player.level;
        xpPoints = player.xpPoints;
        currentPosition = player.currentPosition;
    }

    private void Awake()
    {
        if( instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

}

