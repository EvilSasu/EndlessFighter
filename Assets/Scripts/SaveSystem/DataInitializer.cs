using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitializer : MonoBehaviour
{
    public PlayerData playerData;
    public GameData gameData;
    public EnergySystem energySystem;
    public AchievementManager achievementData;

    public bool saveData = true;
    private void Awake()
    {
        //RemoveDataKeys();
        InitializeAllData();
    }

    private void OnDestroy()
    {
        if (saveData)
        {
            SaveAllData();
        }
    }

    private void OnApplicationQuit()
    {
        if (saveData)
        {
            SaveAllData();
        }
    }

    public void SaveAllData()
    {
        SavePlayerData();
        SaveGameData();
        SaveAchievementData();
    }

    public void InitializeAllData()
    {
        InitializePlayerData();
        InitializeGameData();
        InitializeEnergySystem();
        InitializeAchievementData();
    }

    public void RemoveDataKeys()
    {
        PlayerPrefs.DeleteKey("PlayerData");
        PlayerPrefs.DeleteKey("GameData");
        PlayerPrefs.DeleteKey("AchievementData");
    }

    public void RemoveAllDataKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    private void InitializePlayerData()
    {
        if(playerData != null)
        {
            playerData.InitializeData();
        }
        else
        {
            playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
            if(playerData != null )
                playerData.InitializeData();
        }
    }
    private void InitializeGameData()
    {
        if (gameData != null)
        {
            gameData.InitializeData();
        }
        else
        {
            gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
            if (gameData != null)
                gameData.InitializeData();
        }
    }
    private void InitializeEnergySystem()
    {
        if (energySystem != null)
        {
            energySystem.InitializeData();
        }
        else
        {
            energySystem = GameObject.FindGameObjectWithTag("GameData").GetComponent<EnergySystem>();
            if (energySystem != null)
                energySystem.InitializeData();
        }
    }
    private void InitializeAchievementData()
    {
        if (achievementData != null)
        {
            achievementData.InitializeData();
        }
        else
        {
            achievementData = GameObject.FindGameObjectWithTag("GameData").GetComponent<AchievementManager>();
            if (achievementData != null)
                achievementData.InitializeData();
        }
    }

    private void SavePlayerData()
    {
        if (playerData != null)
        {
            playerData.SavePlayerDataToPlayerPrefs();
        }
    }
    private void SaveGameData()
    {
        if (gameData != null)
        {
            gameData.SaveGameDataToPlayerPrefs();
        }
    }

    private void SaveAchievementData()
    {
        if (achievementData != null)
        {
            achievementData.SaveAchievementData();
        }
    }
}
