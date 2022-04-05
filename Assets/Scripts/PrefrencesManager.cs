using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefrencesManager : MonoBehaviour
{
    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1);
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat("MasterVolume", 1);
    }

    public static void SetMusicVolume(float soundLevel)
    {
        PlayerPrefs.SetFloat("MusicVolume", soundLevel);
    }

     public static void SetMasterVolume(float soundLevel)
     {
      PlayerPrefs.SetFloat("MasterVolume", soundLevel);
     }
}
