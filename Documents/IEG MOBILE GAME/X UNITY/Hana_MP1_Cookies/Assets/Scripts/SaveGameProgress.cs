using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SaveGameProgress : MonoBehaviour
{
    public SaveManager saveManager;
    private GameManager gameManager;
    public OptionsEvent optionsEvent;
    public UpgradeManager upgradeManager;

    public TMP_Text lastSaveText;
    public GameObject successSaveBanner;

    private float autoSaveInterval = 60f; //auto save every 60sec

    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        optionsEvent = FindAnyObjectByType<OptionsEvent>();
        upgradeManager = FindAnyObjectByType<UpgradeManager>();

        //load & display last save time if available
        string lastSaveTime = PlayerPrefs.GetString("LastSaveTime", "Never");
        if(lastSaveText != null)
        {
            lastSaveText.text = "Last Save: " + lastSaveTime;
        }

        InvokeRepeating("SaveGame", autoSaveInterval, autoSaveInterval); //for autosave

    }

    public void SaveGame()
    {
        saveManager.SaveGame(
            totalCookiesClicked: gameManager.GetTotalCookiesClicked(),
            cookiesMultiplier: gameManager.GetCookiesMultiplier(),
            cookiesIdleIncrement: gameManager.GetCookiesIdleIncrement(),
            masterVolume: optionsEvent.masterSlider.value,
            musicVolume: optionsEvent.musicSlider.value,
            soundVolume: optionsEvent.soundSlider.value,
            brightness: optionsEvent.brightnessSlider.value
            );

        //update last save time
        string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        PlayerPrefs.SetString("LastSaveTime", currentTime);
        PlayerPrefs.Save();

        if(lastSaveText != null)
        {
            lastSaveText.text = "Last Save: " + currentTime;
        }

        successSaveBanner.SetActive(true);
        Invoke("OffBanner", 3);
    }

    void OffBanner()
    {
        successSaveBanner.SetActive(false);
    }
}
