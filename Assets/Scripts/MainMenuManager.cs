using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.LoadPlayerData(SaveSystem.LoadPlayer());
    }
    public  void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

}
