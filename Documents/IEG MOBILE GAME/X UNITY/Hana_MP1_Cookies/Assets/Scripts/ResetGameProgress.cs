using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ResetGameProgress : MonoBehaviour
{
    public SaveManager saveManager;
    private GameManager gameManager;
    public OptionsEvent optionsEvent;
    public TMP_Text lastSaveText;
    public GameObject successResetBanner;

    void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        optionsEvent = FindAnyObjectByType<OptionsEvent>();
    }
    public void ResetGame()
    {
        //delete saved data file
        saveManager.DeleteSaveData();

        //clear last save time
        PlayerPrefs.DeleteKey("LastSaveTime");
        PlayerPrefs.Save();

        if(lastSaveText != null)
        {
            lastSaveText.text = "Last Save: Never";
        }

        gameManager.LoadGameData(); //assume data == null, so it reset to default value from else condition
        optionsEvent.LoadSettings(); //assume data == null, so it reset to default settings from else condition

        successResetBanner.SetActive(true);
        Invoke("OffBanner", 3);
    }

    void OffBanner()
    {
        successResetBanner.SetActive(false);
    }

}
