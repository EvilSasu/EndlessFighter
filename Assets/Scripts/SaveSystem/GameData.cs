using System;
using System.Collections;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int maxEnergy;
    public int currentEnergy;
    public DateTime lastEnergyUpdateTime;
    public PlayerData playerData;
    public GameDataSS gameDataSS;

    private int _baseEnergy = 100;

    public void SaveGameDataToPlayerPrefs()
    {
        GameDataSS gameData = new GameDataSS();
        if (maxEnergy < 10) 
        {
            gameData = CreateNewGameData();
        }
        maxEnergy = CalculateMaxEnergy(playerData.unitLevel);
        gameData.maxEnergy = maxEnergy;
        gameData.currentEnergy = currentEnergy;
        gameData.lastEnergyUpdateTime = lastEnergyUpdateTime.ToString();
        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();
    }

    private GameDataSS LoadGameData()
    {
        string jsonGameData = PlayerPrefs.GetString("GameData", "");

        if (!string.IsNullOrEmpty(jsonGameData))
        {
            GameDataSS gameData = JsonUtility.FromJson<GameDataSS>(jsonGameData);
            Debug.Log("Game data found in PlayerPrefs.");
            if(gameData.maxEnergy <= 10)
            {
                gameData = CreateNewGameData();
            }
            return gameData;
        }
        else
        {
            //Debug.Log("Game data not found in PlayerPrefs.");
            return CreateNewGameData();
        }
    }

    private GameDataSS CreateNewGameData()
    {
        GameDataSS gameData = new GameDataSS();
        if(playerData != null)
        {
            if (playerData.unitLevel >= 1)
                gameData.maxEnergy = CalculateMaxEnergy(playerData.unitLevel);
            else
                gameData.maxEnergy = _baseEnergy;
        }
        else
            gameData.maxEnergy = _baseEnergy;

        gameData.currentEnergy = gameData.maxEnergy;
        gameData.lastEnergyUpdateTime = DateTime.Now.ToString();

        maxEnergy = gameData.maxEnergy;
        currentEnergy = gameData.currentEnergy;
        lastEnergyUpdateTime = DateTime.Parse(gameData.lastEnergyUpdateTime);

        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();

        //Debug.Log("New game data created in PlayerPrefs.");
        return gameData;
    }

    private void FromDataToGameObejct(GameDataSS dataSS)
    {
        if (dataSS != null)
        {
            maxEnergy = dataSS.maxEnergy; 
            currentEnergy = dataSS.currentEnergy;
            lastEnergyUpdateTime = DateTime.Parse(dataSS.lastEnergyUpdateTime);
        }
        else
        {
            //Debug.Log("Failed to load game data from JSON");
        }
    }

    private int CalculateMaxEnergy(int level)
    {
        return _baseEnergy + (2 * level);
    }

    IEnumerator LaterAwake(float time)
    {
        yield return new WaitForSeconds(time);
        gameDataSS = LoadGameData();
        FromDataToGameObejct(gameDataSS);
    }

    public void InitializeData()
    {
        gameDataSS = LoadGameData();
        FromDataToGameObejct(gameDataSS);
    }
}
