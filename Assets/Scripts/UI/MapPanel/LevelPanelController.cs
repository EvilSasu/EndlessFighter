using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class LevelPanelController : MonoBehaviour
{
    public PlayerData playerData;
    public GameData gameData;
    public LevelLoader levelLoader;
    public TextMeshProUGUI nameOfLevel;
    public TextMeshProUGUI amountOfEnemiesText;
    public TextMeshProUGUI levelsOfEnemiesText;
    public Button button;
    public Image star1;
    public Image star2;
    public Image star3;
    
    public GameObject levelPanel;

    public GameObject enemiesGroup;
    public GameObject enemyPanel;

    public GameObject dangerLevelGroup;
    public GameObject skull;

    public List<Sprite> enemiesSprites = new List<Sprite>();
    public GameObject blocker;


    private string startTextFormat = "<color=#FCDB00><b>";
    private string endTextFormat = "</color></b>";

    private int minEnemyLevel;
    private int maxEnemyLevel;
    private GameObject levelObj;
    private int dangerLevel;
    private List<GameObject> newSkulls = new List<GameObject>();
    private List<GameObject> newEnemiesPanels = new List<GameObject>();
    private ListWithEnemyLevels takenListWithEnemyLevels;
    public void SetupPanel(GameObject levelObject)
    {
        blocker.SetActive(true);
        levelPanel.SetActive(true);
        levelObj = levelObject;
        MapLevelStars mapLevelStars = levelObj.GetComponent<MapLevelStars>();
        takenListWithEnemyLevels = levelObject.GetComponent<ListWithEnemyLevels>();
        nameOfLevel.text = new string(startTextFormat + "Level " + (mapLevelStars.lastPlayedLevel + 1) + endTextFormat);
        amountOfEnemiesText.text = new string("Amount of enemies: " + startTextFormat + takenListWithEnemyLevels.enemyLevels.Count + endTextFormat);

        FindMinAndMaxEnemiesLevel(takenListWithEnemyLevels);

        levelsOfEnemiesText.text = new string("Levels of enemies: " + startTextFormat + minEnemyLevel + "-" + maxEnemyLevel + endTextFormat);

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = CalculateEnergyCost((mapLevelStars.lastPlayedLevel + 1)).ToString();

        if (takenListWithEnemyLevels.enemyLevels.Count > gameData.currentEnergy)
            button.interactable = false;
        else
            button.interactable = true;

        SetStars(mapLevelStars);
        CalculateDangerLevel();
        CalcluateTypiesOfEnemeis(takenListWithEnemyLevels);
    }
    
    public void StartLevel()
    {
        takenListWithEnemyLevels.SaveEnemiesToPlayerPrefs();
        gameData.gameObject.GetComponent<EnergySystem>().currentEnergy -= takenListWithEnemyLevels.enemyLevels.Count;
        levelLoader.LoadScene("FightScene1");
    }

    private void CalculateDangerLevel()
    {
        int redDangerLevel = 0;
        dangerLevel = ((maxEnemyLevel - playerData.unitLevel) / 5);

        if (dangerLevel <= 0)
            dangerLevel = 1;

        if(dangerLevel >= 6)
        {
            redDangerLevel = dangerLevel / 5;
            dangerLevel = dangerLevel % 5;
        }

        for (int i = 0; i < redDangerLevel; i++)
        {
            GameObject newObj = Instantiate(skull, skull.transform.position, Quaternion.identity);
            newObj.transform.SetParent(dangerLevelGroup.transform, false);
            newObj.GetComponent<Image>().color = new Color(1, 0, 0, 1);
            newSkulls.Add(newObj);
            newObj.SetActive(true);
        }
        for (int i = 0; i < dangerLevel; i++)
        {
            GameObject newObj = Instantiate(skull, skull.transform.position, Quaternion.identity);
            newObj.transform.SetParent(dangerLevelGroup.transform, false);
            newSkulls.Add(newObj);
            newObj.SetActive(true);
        }            
    }

    private void CalcluateTypiesOfEnemeis(ListWithEnemyLevels listWithEnemyLevels)
    {
        int amountOfHumans = 0;
        int amountOfOrcs = 0;

        int count = listWithEnemyLevels.enemyLevels.Count;

        for (int i = 0; i < count; i++)
        {
            if (listWithEnemyLevels.enemyLevels[i].type == enemyType.Human)
                amountOfHumans++;
            else if(listWithEnemyLevels.enemyLevels[i].type == enemyType.Orc)
                amountOfOrcs++;
        }

        if(amountOfHumans > 0)
        {
            GameObject newObj = Instantiate(enemyPanel, enemyPanel.transform.position, Quaternion.identity);
            newObj.transform.SetParent(enemiesGroup.transform, false);
            newObj.transform.GetChild(0).GetComponent<Image>().sprite = enemiesSprites[0];
            newObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = amountOfHumans.ToString();
            newObj.SetActive(true);
            newEnemiesPanels.Add(newObj);
        }
        if (amountOfOrcs > 0)
        {
            GameObject newObj = Instantiate(enemyPanel, enemyPanel.transform.position, Quaternion.identity);
            newObj.transform.SetParent(enemiesGroup.transform, false);
            newObj.transform.GetChild(0).GetComponent<Image>().sprite = enemiesSprites[1];
            newObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = amountOfOrcs.ToString();
            newObj.SetActive(true);
            newEnemiesPanels.Add(newObj);
        }
    }

    public void RemoveOldPanelsAndSkulls()
    {
        for (int i = newSkulls.Count - 1; i >= 0; i--)
        {
            Destroy(newSkulls[i]);
            newSkulls.RemoveAt(i);
        }
        for (int i = newEnemiesPanels.Count - 1; i >= 0; i--)
        {
            Destroy(newEnemiesPanels[i]);
            newEnemiesPanels.RemoveAt(i);
        }
    }

    private void FindMinAndMaxEnemiesLevel(ListWithEnemyLevels listWithEnemyLevels)
    {
        minEnemyLevel = FindMin(listWithEnemyLevels.enemyLevels);
        maxEnemyLevel = FindMax(listWithEnemyLevels.enemyLevels);
    }

    private int FindMin(List<EnemyInfo> numbers)
    {
        if (numbers.Count == 0)
        {
            return 0;
        }

        int min = numbers[0].level;
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i].level < min)
            {
                min = numbers[i].level;
            }
        }

        return min;
    }

    private int FindMax(List<EnemyInfo> numbers)
    {
        if (numbers.Count == 0)
        {
            return 0;
        }

        int max = numbers[0].level;
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i].level > max)
            {
                max = numbers[i].level;
            }
        }

        return max;
    }

    private void SetStars(MapLevelStars mapLevelStars)
    {
        if (mapLevelStars.showStar3)
        {
            star1.sprite = mapLevelStars.goodStar;
            star2.sprite = mapLevelStars.goodStar;
            star3.sprite = mapLevelStars.goodStar;
        }
        else if (mapLevelStars.showStar2)
        {
            star1.sprite = mapLevelStars.goodStar;
            star2.sprite = mapLevelStars.goodStar;
            star3.sprite = mapLevelStars.emptyStar;
        }
        else if (mapLevelStars.showStar1)
        {
            star1.sprite = mapLevelStars.goodStar;
            star2.sprite = mapLevelStars.emptyStar;
            star3.sprite = mapLevelStars.emptyStar;
        }
        else
        {
            star1.sprite = mapLevelStars.emptyStar;
            star2.sprite = mapLevelStars.emptyStar;
            star3.sprite = mapLevelStars.emptyStar;
        }
    }

    private int CalculateEnergyCost(int level)
    {
        if (level <= 4)
            return 7;
        else if (level > 4 && level <= 8)
            return 9;
        else if (level > 8 && level <= 12)
            return 12;
        else if (level > 12 && level <= 16)
            return 15;

        return 10;
    }
}
