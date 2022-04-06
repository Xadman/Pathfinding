using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

  public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;


    private static AudioManager Instance { get { return instance; } }

    public AudioMixer masterMixer;

    public Slider musicSlider, masterSlider;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        masterMixer.SetFloat("MasterVol", PrefrencesManager.GetMasterVolume());
        masterMixer.SetFloat("MusicVol", PrefrencesManager.GetMusicVolume());

        if (masterSlider != null)
           masterSlider.value = PrefrencesManager.GetMasterVolume();

        if (masterSlider != null)
           masterSlider.value = PrefrencesManager.GetMusicVolume();
    }

    public void ChangemusicVolume(float soundlevel)
    {
        masterMixer.SetFloat("MusicVol", soundlevel);
        PrefrencesManager.SetMusicVolume(soundlevel);
    }
   
        public void ChangeSoundVolume(float soundlevel)
    {
        masterMixer.SetFloat("MasterVol", soundlevel);
            PrefrencesManager.SetMasterVolume(soundlevel);
    }
    
 


    }

