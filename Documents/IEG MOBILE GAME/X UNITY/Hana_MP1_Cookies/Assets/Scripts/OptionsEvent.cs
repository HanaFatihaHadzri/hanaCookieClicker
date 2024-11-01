using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsEvent : MonoBehaviour
{
    public Slider masterSlider, soundSlider, musicSlider, brightnessSlider;
    public AudioMixer mainAudioMixer;
    public Image brightnessPanel;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
        LoadSettings();
    }

    public void LoadSettings()
    {
        GameData data = saveManager.LoadGame();

        if(data != null)
        {
            //initialize volume settings
            masterSlider.value = data.masterVolume;
            soundSlider.value = data.soundVolume;
            musicSlider.value = data.musicVolume;

            if(brightnessPanel != null)
            {
                brightnessSlider.value = data.brightness;
            }

            //update default into saved data
            ChangeMasterVolume();
            ChangeMusicVolume();
            ChangeSoundVolume();
            ChangeBrightness();

        }
        else
        {
            //default value if no save data
            //initialize volume settings            
            masterSlider.value = 0.75f;
            soundSlider.value = 1f;
            musicSlider.value = 1f;
            
            // Initialize brightness settings
            if (brightnessPanel != null)
            {
                brightnessSlider.value = 1f;
            }
        }
        
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterSlider.value);
    }
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicSlider.value);
    }
    public void ChangeSoundVolume()
    {
        mainAudioMixer.SetFloat("SoundVol", soundSlider.value);
    }

    public void ChangeBrightness()
    {
        Color panelColor = brightnessPanel.color;
        panelColor.a = 0.5f * (1- brightnessSlider.value);
        brightnessPanel.color = panelColor;
    }

}
