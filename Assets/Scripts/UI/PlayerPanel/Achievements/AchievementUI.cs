using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public Transform content;
    public GameObject achievementPanelPrefab;
    public AchievementManager achievementManager;
    public GameData gameData;
    public PlayerData playerData;
    public PlayerDataShowValuesController playerDataShowValuesController;
    public List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> rewardSprites = new List<Sprite>();

    private void Start()
    {
        StartCoroutine(LaterAwake(0.12f));
    }

    public void CreateAchievementPanels()
    {
        foreach (Achievement achievement in achievementManager.achievements)
        {
            GameObject panel = Instantiate(achievementPanelPrefab, content);
            panel.SetActive(true);
            TextMeshProUGUI titleText = panel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descriptionText = panel.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            Image image = panel.transform.Find("Image").GetChild(0).GetComponent<Image>();
            Transform button = panel.transform.Find("Button");
            Transform progressBar = panel.transform.Find("ProgressBar");

            if(achievement.levelOfAchievement < 2)
                titleText.text = achievement.title;
            else
                titleText.text = achievement.title + " " + achievement.levelOfAchievement;

            descriptionText.text = achievement.description;

            image.sprite = sprites[achievement.imageId];
            button.GetChild(2).GetComponent<TextMeshProUGUI>().text = achievement.reward.amount.ToString();

            if (achievement.reward.rewardType == "experience")
                button.GetChild(2).GetChild(0).GetComponent<Image>().sprite = rewardSprites[0];
            if (achievement.reward.rewardType == "coins")
                button.GetChild(2).GetChild(0).GetComponent<Image>().sprite = rewardSprites[1];
            if (achievement.reward.rewardType == "diamonds")
                button.GetChild(2).GetChild(0).GetComponent<Image>().sprite = rewardSprites[2];

            progressBar.GetComponent<Image>().fillAmount = (float)achievement.currentProgress / achievement.targetProgress;

            SetupAchievementPanel(panel, achievement);
        }
    }

    public void CreateNewSingleAchievementPanel(Achievement achievement)
    {
        achievementManager.achievements.Add(achievement);
        GameObject panel = Instantiate(achievementPanelPrefab, content);
        panel.SetActive(true);
        TextMeshProUGUI titleText = panel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = panel.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        Image image = panel.transform.Find("Image").GetChild(0).GetComponent<Image>();
        Transform button = panel.transform.Find("Button");
        Transform progressBar = panel.transform.Find("ProgressBar");

        if (achievement.levelOfAchievement < 2)
            titleText.text = achievement.title;
        else
            titleText.text = achievement.title + " " + achievement.levelOfAchievement;

        descriptionText.text = achievement.description;

        image.sprite = sprites[achievement.imageId];
        button.GetChild(2).GetComponent<TextMeshProUGUI>().text = achievement.reward.amount.ToString();

        if (achievement.reward.rewardType == "experience")
            button.GetChild(2).GetChild(0).GetComponent<Image>().sprite = rewardSprites[0];
        else if (achievement.reward.rewardType == "coins")
            button.GetChild(2).GetChild(0).GetComponent<Image>().sprite = rewardSprites[1];
        else if (achievement.reward.rewardType == "diamonds")
            button.GetChild(2).GetChild(0).GetComponent<Image>().sprite = rewardSprites[2];

        progressBar.GetComponent<Image>().fillAmount = (float)achievement.currentProgress / achievement.targetProgress;

        SetupAchievementPanel(panel, achievement);
    }

    private void SetupAchievementPanel(GameObject panel, Achievement achievement)
    {
        panel.GetComponent<AchievementPanel>().title = achievement.title;
        panel.GetComponent<AchievementPanel>().description = achievement.description;
        panel.GetComponent<AchievementPanel>().targetProgress = achievement.targetProgress;
        panel.GetComponent<AchievementPanel>().currentProgress = achievement.currentProgress;
        panel.GetComponent<AchievementPanel>().isUnlocked = false;
        panel.GetComponent<AchievementPanel>().imageId = achievement.imageId;
        panel.GetComponent<AchievementPanel>().levelOfAchievement = achievement.levelOfAchievement;
        panel.GetComponent<AchievementPanel>().rewardType = achievement.reward.rewardType;
        panel.GetComponent<AchievementPanel>().amount = achievement.reward.amount;
        panel.GetComponent<AchievementPanel>().achievement = achievement;
        panel.GetComponent<AchievementPanel>().typeOfDataProgress = achievement.typeOfDataProgress;

        panel.GetComponent<AchievementPanel>().CheckNumber();
    }

    private IEnumerator LaterAwake(float time)
    {
        yield return new WaitForSeconds(time);
        CreateAchievementPanels();
    }
}
