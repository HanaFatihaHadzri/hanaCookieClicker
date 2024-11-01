using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //private string saveFilePath;
    private const string SaveKey = "GameSaveData";
    public UpgradeManager upgradeManager;

    public void SaveGame(int totalCookiesClicked, int cookiesMultiplier, int cookiesIdleIncrement,
        float masterVolume, float musicVolume, float soundVolume, float brightness)
    {
        GameData data = new GameData
        {
            totalCookiesClicked = totalCookiesClicked,
            cookiesMultiplier = cookiesMultiplier,
            cookiesIdleIncrement = cookiesIdleIncrement,
            masterVolume = masterVolume,
            musicVolume = musicVolume,
            soundVolume = soundVolume,
            brightness = brightness
        };

        upgradeManager.SaveUpgrades(data);        

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Game Saved: " + json);
    }

    public GameData LoadGame()
    {
        if(PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            GameData data = JsonUtility.FromJson<GameData>(json);

            Debug.Log("Game Loaded: " + json);
            return data;
        }
        else
        {
            Debug.Log("No save data found.");
            return null;
        }
    }

    public void DeleteSaveData()
    {
        if(PlayerPrefs.HasKey(SaveKey))
        {
            PlayerPrefs.DeleteKey(SaveKey);
            PlayerPrefs.Save();

            Debug.Log("Saved data deleted !");
        }
        else
        {
            Debug.Log("No save data to delete !");
        }
    }
}
