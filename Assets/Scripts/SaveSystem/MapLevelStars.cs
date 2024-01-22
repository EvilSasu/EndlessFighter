using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapLevelStars : MonoBehaviour
{
    public MapAchievementLevelController mapAchievementLevelController;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public Sprite goodStar;
    public Sprite emptyStar;

    public bool hasBeenPlayed;
    public bool showStar1;
    public bool showStar2;
    public bool showStar3;

    public int MAX_LEVEL;
    public int lastPlayedLevel = 0;
    void Start()
    {
        MAX_LEVEL = transform.parent.childCount - 1;
        LevelDataSS loadedData = LoadLevelStars();
        UnlockNextLevel(loadedData);
        CheckPrevLevel();
        if (loadedData == null)
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else
        {
            showStar1 = loadedData.showStar1SS;
            showStar2 = loadedData.showStar2SS;
            showStar3 = loadedData.showStar3SS;
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            SetStars();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private LevelDataSS LoadLevelStars()
    {
        string jsonLevelData = PlayerPrefs.GetString(gameObject.name, "");

        if (!string.IsNullOrEmpty(jsonLevelData))
        {
            LevelDataSS loadedLevelData = JsonUtility.FromJson<LevelDataSS>(jsonLevelData);
            return loadedLevelData;
        }
        else
        {
            //Debug.Log("Level data not found in PlayerPrefs.");
            return null;
        }      
    }

    private void CheckPrevLevel()
    {
        if(gameObject.name != "Lvl_1")
        {
            string _levelName = gameObject.name;

            if (_levelName.Length <= 5) // levels 1 - 9
                CalculateForTenLevels(_levelName, -1);

            else if (_levelName.Length > 5 && _levelName.Length <= 6) // levels 10 - 99
                CalculateForHundredLevels(_levelName, -1);
        }       
    }

    private void UnlockNextLevel(LevelDataSS LevelData)
    {
        if(LevelData != null)
        {
            string _levelName = gameObject.name;

            if (_levelName.Length <= 5) // levels 1 - 9
                CalculateForTenLevels(_levelName, 1);

            else if (_levelName.Length > 5 && _levelName.Length <= 6) // levels 10 - 99
                CalculateForHundredLevels(_levelName, 1);    
        }
    }

    private void CheckIfDataLevelExists(int lvl, int levelCharsAmount)
    {
        string _levelName = gameObject.name;
        string _prevLvlString = _levelName.Substring(0, _levelName.Length - levelCharsAmount) + lvl;
        string jsonLevelData = PlayerPrefs.GetString(_prevLvlString, "");

        if (!string.IsNullOrEmpty(jsonLevelData))
        {
            LevelDataSS loadedLevelData = JsonUtility.FromJson<LevelDataSS>(jsonLevelData);
            if (loadedLevelData.levelHasBeenPlayedSS)
            {
                gameObject.SetActive(true);
                if(lastPlayedLevel < lvl)
                {
                    lastPlayedLevel = lvl;
                    mapAchievementLevelController.SetLastPlayedLevel(lastPlayedLevel);
                }                
            }         
            else
                gameObject.SetActive(false);
        }
        else
        {
            //Debug.Log("No prev level data not found in PlayerPrefs.");
            gameObject.SetActive(false);
        }       
    }

    private void CalculateForTenLevels(string _levelName, int _intValueOfLevel)
    {
        char _lastChar = _levelName[_levelName.Length - 1];
        int _nextLvl = int.Parse(_lastChar.ToString()) + _intValueOfLevel;

        if (_intValueOfLevel > 0)
            CheckLevelInMaxLevel(_nextLvl, 1);
        else
            CheckIfDataLevelExists(_nextLvl, 1);
    }

    private void CalculateForHundredLevels(string _levelName, int _intValueOfLevel)
    {
        string lastTwoChars = _levelName.Substring(_levelName.Length - 2);
        int _nextLvl = int.Parse(lastTwoChars) + _intValueOfLevel;

        if(_intValueOfLevel > 0)
            CheckLevelInMaxLevel(_nextLvl, 2);
        else
            CheckIfDataLevelExists(_nextLvl, 2);
    }

    private void CheckLevelInMaxLevel(int nextLevel, int levelCharsAmount)
    {
        string _levelName = gameObject.name;
        if (nextLevel <= MAX_LEVEL)
        {
            string _nextLvlString = _levelName.Substring(0, _levelName.Length - levelCharsAmount) + nextLevel;
            GameObject _nextLevel = GameObject.Find(_nextLvlString);

            if (_nextLevel != null)
            {
                Debug.Log(_nextLevel);
                _nextLevel.SetActive(true);
            }
            else
            {
                _nextLevel.SetActive(false);
            }
        }
    }

    private void SetStars()
    {
        if (showStar3)
        {
            star1.GetComponent<Image>().sprite = goodStar;
            star2.GetComponent<Image>().sprite = goodStar;
            star3.GetComponent<Image>().sprite = goodStar;
            hasBeenPlayed = true;
        }
        else if (showStar2)
        {
            star1.GetComponent<Image>().sprite = goodStar;
            star2.GetComponent<Image>().sprite = goodStar;
            star3.GetComponent<Image>().sprite = emptyStar;
            hasBeenPlayed = true;
        }
        else if (showStar1)
        {
            star1.GetComponent<Image>().sprite = goodStar;
            star2.GetComponent<Image>().sprite = emptyStar;
            star3.GetComponent<Image>().sprite = emptyStar;
            hasBeenPlayed = true;
        }
        else
        {
            star1.GetComponent<Image>().sprite = emptyStar;
            star2.GetComponent<Image>().sprite = emptyStar;
            star3.GetComponent<Image>().sprite = emptyStar;
            hasBeenPlayed = false;
        }
    }
}
