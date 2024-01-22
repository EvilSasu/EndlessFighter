using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AchievementManager : MonoBehaviour
{
    // Data with achievement progress
    public int currentPlayerLevel;
    public int lastComplitedLevelOnMap;
    public int currentStrength;
    public int currentAgility;
    public int currentIntelligence;
    public int currentDefense;
    public int currentEnemiesDefeated;
    public int goldEarnedSoFar;
    public int diamondsEarnedSoFar;
    // End of Data progress
    private GameData gameData;

    private AchievementData achievementData;

    public List<Achievement> achievements = new List<Achievement>();

    public void SaveAchievementData()
    {
        LoadDataToAchievementData(false);

        string json = JsonUtility.ToJson(achievementData);
        PlayerPrefs.SetString("AchievementData", json);
        PlayerPrefs.Save();
    }

    private void LoadAchievementData()
    {
        if (PlayerPrefs.HasKey("AchievementData"))
        {
            achievements.Clear();
            string json = PlayerPrefs.GetString("AchievementData");
            achievementData = JsonUtility.FromJson<AchievementData>(json);
            LoadDataToThisObjectFromAchievementData();
        }
        else
        {
            AddStartingAchievements();
            achievementData = new AchievementData();
            LoadDataToAchievementData(true);
        }
    }

    private void LoadDataToAchievementData(bool isNewData) 
    {
        achievementData.achievements = achievements;
        if (isNewData)
        {
            SetBaseValuesToProgressAchievementData();
        }
        else
        {
            LoadDataFromPlayerData();
            achievementData.currentPlayerLevel = currentPlayerLevel;
            achievementData.lastComplitedLevelOnMap = lastComplitedLevelOnMap;
            achievementData.currentStrength = currentStrength;
            achievementData.currentAgility = currentAgility;
            achievementData.currentIntelligence = currentIntelligence;
            achievementData.currentDefense = currentDefense;
            achievementData.currentEnemiesDefeated = currentEnemiesDefeated;
            achievementData.goldEarnedSoFar = goldEarnedSoFar;
            achievementData.diamondsEarnedSoFar = diamondsEarnedSoFar;
        }       
    }
    public void LoadDataFromPlayerData()
    {
        currentPlayerLevel = gameData.playerData.unitLevel;
        currentStrength = gameData.playerData.strength;
        currentAgility = gameData.playerData.agility;
        currentIntelligence = gameData.playerData.intelligence;
        currentDefense = gameData.playerData.defense;
    }

    public void SetLastPlayedLevel(int lastLevel)
    {
        if(lastComplitedLevelOnMap < lastLevel)
            lastComplitedLevelOnMap = lastLevel;
    }

    private void LoadDataToThisObjectFromAchievementData()
    {
        achievements = achievementData.achievements;

        currentPlayerLevel = achievementData.currentPlayerLevel;
        lastComplitedLevelOnMap = achievementData.lastComplitedLevelOnMap;
        currentStrength = achievementData.currentStrength;
        currentAgility = achievementData.currentAgility;
        currentIntelligence = achievementData.currentIntelligence;
        currentDefense = achievementData.currentDefense;
        currentEnemiesDefeated = achievementData.currentEnemiesDefeated;
        goldEarnedSoFar = achievementData.goldEarnedSoFar;
        diamondsEarnedSoFar = achievementData.diamondsEarnedSoFar;
    }

    private void SetBaseValuesToProgressAchievementData()
    {
        achievementData.currentPlayerLevel = 1;
        achievementData.lastComplitedLevelOnMap = 0;
        achievementData.currentStrength = 5;
        achievementData.currentAgility = 5;
        achievementData.currentIntelligence = 5;
        achievementData.currentDefense = 5;
        achievementData.currentEnemiesDefeated = 0;
        achievementData.goldEarnedSoFar = 0;
        achievementData.diamondsEarnedSoFar = 0;
    }

    private void AddStartingAchievements()
    {
        achievements.Add(new Achievement("I'M GETTING STRONGER!", "Unlock level 5", 5, 0, 1, new Reward("coins", 10), "level"));
        achievements.Add(new Achievement("It wouldn't do any good", "Get your strength to 10", 10, 1, 1, new Reward("experience", 50), "strength"));
        achievements.Add(new Achievement("To slow", "Get your agility to 10", 10, 2, 1, new Reward("experience", 50), "agility"));
        achievements.Add(new Achievement("You're a wizard Harry", "Get your intelligence to 10", 10, 3, 1, new Reward("experience", 50), "intelligence"));
        achievements.Add(new Achievement("OK!", "Get your defense to 10", 10, 4, 1, new Reward("experience", 50), "defense"));
        achievements.Add(new Achievement("Explorator", "Finish 5 levels", 5, 5, 1, new Reward("diamonds", 20), "complitedLevels"));
        achievements.Add(new Achievement("Monster Hunter", "Defeat 50 enemies", 50, 6, 1, new Reward("diamonds", 100), "defeatedEnemies"));
        achievements.Add(new Achievement("Gold Digger", "Collect 100 gold", 100, 7, 1, new Reward("coins", 50), "collectedGold"));
    }

    private IEnumerator LaterAwake(float time)
    {
        yield return new WaitForSeconds(time);
        LoadAchievementData();
        CheckProgress();
    }

    private void CheckProgress()
    {
        foreach(var achievement in achievements)
        {
            if (!achievement.isUnlocked)
            {
                switch (achievement.typeOfDataProgress)
                {
                    case "level":
                        achievement.currentProgress = currentPlayerLevel; break;
                    case "strength":
                        achievement.currentProgress = currentStrength; break;
                    case "agility":
                        achievement.currentProgress = currentAgility; break;
                    case "intelligence":
                        achievement.currentProgress = currentIntelligence; break;
                    case "defense":
                        achievement.currentProgress = currentDefense; break;
                    case "complitedLevels":
                        achievement.currentProgress = lastComplitedLevelOnMap; break;
                    case "defeatedEnemies":
                        achievement.currentProgress = currentEnemiesDefeated; break;
                    case "collectedGold":
                        achievement.currentProgress = goldEarnedSoFar; break;
                    default: break;
                }

                if (achievement.currentProgress >= achievement.targetProgress)
                {
                    achievement.isUnlocked = true;
                }
                else
                {
                    achievement.isUnlocked = false;
                }

            }
        }
    }

    public bool CheckIfAnyAchievemntUnlocked()
    {
        foreach(var achievement in achievements)
        {
            if (achievement.isUnlocked)
            {
                return true;
            }              
        }
        return false;
    }

    public void InitializeData()
    {
        gameData = GetComponent<GameData>();
        //PlayerPrefs.DeleteKey("AchievementData");
        LoadAchievementData();
        CheckProgress();
    }
}
