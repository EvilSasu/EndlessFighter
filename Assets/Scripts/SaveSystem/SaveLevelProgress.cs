using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SaveLevelProgress : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public Sprite goodStar;
    public Sprite emptyStar;
    public GameObject player;
    public FightAchievementUpdate fightAchievementUpdate;
    public PlayerData playerData;

    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textExp;
    public TextMeshProUGUI textGold;
    public TextMeshProUGUI textDiamonds;

    public string nameOfLevel;
    public long timeToBeat;
    public long _endTime;
    private DateTime _startTime;
    private int amountOfEarnStars = 0;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public int earnedExp = 0;
    public int earnedGold = 0;
    public int earnedDiamonds = 0;

    void Start()
    {
        _startTime = DateTime.Now;
    }

    public void AddGold(int val)
    {
        this.earnedGold += val;
        fightAchievementUpdate.UpdateAchievementData(val, 0);
        playerData.AddGold(val);
    }

    public void AddExp(int val)
    {
        this.earnedExp += val;
        playerData.AddExp(val);
    }

    public void AddDiamonds(int val)
    {
        this.earnedDiamonds += val;
        fightAchievementUpdate.UpdateAchievementData(0, val);
        playerData.AddDiamonds(val);
    }
    
    public void CalculateDataForPlayerData()
    {      
        playerData.SavePlayerDataToPlayerPrefs();               
    }

    public void LevelCompleted(bool playerWin)
    {
        TimeSpan levelDuration = DateTime.Now - _startTime;
  
        _endTime = (long)levelDuration.TotalSeconds;

        StartCoroutine(WaitBeforeShowingEndPanel(1f, playerWin));
    }

    private void SaveLevelDataToPlayerPrefs()
    {
        LevelDataSS levelData = new LevelDataSS();
        levelData.nameOfLevelSS = nameOfLevel;
        levelData.levelHasBeenPlayedSS = true;

        switch (amountOfEarnStars)
        {
            case 3: { levelData.showStar1SS = true; levelData.showStar2SS = true; levelData.showStar3SS = true; break; }
            case 2: { levelData.showStar1SS = true; levelData.showStar2SS = true; levelData.showStar3SS = false; break; }
            case 1: { levelData.showStar1SS = true; levelData.showStar2SS = false; levelData.showStar3SS = false; break; }
            default: { levelData.showStar1SS = false; levelData.showStar2SS = false; levelData.showStar3SS = false; break; } 
        }

        string json = JsonUtility.ToJson(levelData);
        PlayerPrefs.SetString(levelData.nameOfLevelSS, json);
        PlayerPrefs.Save();
    }

    private void CalculateProgress()
    {
        if(_endTime <= timeToBeat)
        {
            amountOfEarnStars = 3;
        }
        else if (_endTime > timeToBeat && _endTime <= (long)(timeToBeat * 1.25))
        {
            amountOfEarnStars = 2;
        }
        else if (_endTime > (long)(timeToBeat * 1.25))
        {
            amountOfEarnStars = 1;
        }
    }

    public void SetAllCollectivesToText()
    {
        SetExpInText();
        SetGoldInText();
        SetDiamondsInText();
    }
    
    private void SetupEndLevelPanel(GameObject endPanel)
    {
        SetupVariablesForEndPanel(endPanel);
        SetStars();
        SetTimeInText();
        SetExpInText();
        SetGoldInText();
        SetDiamondsInText();
    }

    private void SetupVariablesForEndPanel(GameObject endPanel)
    {
        textTime = endPanel.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        textExp = endPanel.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        textGold = endPanel.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        textDiamonds = endPanel.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void SetStars()
    {
        if(amountOfEarnStars >= 3)
        {
            star1.GetComponent<Image>().sprite = goodStar;
            star2.GetComponent<Image>().sprite = goodStar;
            star3.GetComponent<Image>().sprite = goodStar;
        }
        else if(amountOfEarnStars == 2)
        {
            star1.GetComponent<Image>().sprite = goodStar;
            star2.GetComponent<Image>().sprite = goodStar;
            star3.GetComponent<Image>().sprite = emptyStar;
        }
        else if (amountOfEarnStars == 1)
        {
            star1.GetComponent<Image>().sprite = goodStar;
            star2.GetComponent<Image>().sprite = emptyStar;
            star3.GetComponent<Image>().sprite = emptyStar;
        }
        else
        {
            star1.GetComponent<Image>().sprite = emptyStar;
            star2.GetComponent<Image>().sprite = emptyStar;
            star3.GetComponent<Image>().sprite = emptyStar;
        }
    }

    private void SetTimeInText()
    {
        long minutes = _endTime / 60;
        long secundes = _endTime % 60;

        string timeText = string.Format("{0:D2}:{1:D2}", minutes, secundes);
        textTime.text = timeText;
    }

    private void SetExpInText()
    {
        string expText = earnedExp.ToString();
        textExp.text = expText;
    }

    private void SetGoldInText()
    {
        string goldText = earnedGold.ToString();
        textGold.text = goldText;
    }

    private void SetDiamondsInText()
    {
        string diamondsText = earnedDiamonds.ToString();
        textDiamonds.text = diamondsText;
    }

    IEnumerator WaitBeforeShowingEndPanel(float time, bool playerWin)
    {
        CalculateDataForPlayerData();
        yield return new WaitForSeconds(time);
        if (playerWin)
        {
            CalculateProgress();
            SaveLevelDataToPlayerPrefs();
            winPanel.SetActive(true);

            SetupEndLevelPanel(winPanel);
        }
        else
        {
            CalculateProgress();
            losePanel.SetActive(true);
            SetupEndLevelPanel(losePanel);
        }
    }
}
