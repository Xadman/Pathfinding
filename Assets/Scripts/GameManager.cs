using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private Player playerData;

    private void Awake()
    {
        playerData = Player.Instance;
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(Player.Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, playerData.currentPosition, playerPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
