using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class AchievementPanel : MonoBehaviour
{
    public AchievementUI ui;
    public Image progressBar;

    public string title;
    public string description;
    public int targetProgress;
    public int currentProgress;
    public bool isUnlocked;
    public int imageId;
    public int levelOfAchievement;
    public string typeOfDataProgress;

    public string rewardType;
    public int amount;

    public int nextLevelTarget;

    public Achievement achievement;
    private int _baseExpAmount = 50;
    private int _baseGoldAmount = 20;
    private int _baseDiamondsAmount = 10;
    private int newAmount;
    private Animator animator;
    public bool animationStarted = false;
    private GameObject audioManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio");
    }

    private void FixedUpdate()
    {
        CalculateProgress();
    }

    private void OnEnable()
    {
        animationStarted = false;
        if (isUnlocked)
            StartAnimation();
    }

    private void CalculateProgress()
    {
        if (!isUnlocked)
        {         
            switch (typeOfDataProgress)
            {
                case "level":
                    currentProgress = ui.achievementManager.currentPlayerLevel; break;
                case "strength":
                    currentProgress = ui.achievementManager.currentStrength; break;
                case "agility":
                    currentProgress = ui.achievementManager.currentAgility; break;
                case "intelligence":
                    currentProgress = ui.achievementManager.currentIntelligence; break;
                case "defense":
                    currentProgress = ui.achievementManager.currentDefense; break;
                case "complitedLevels":
                    currentProgress = ui.achievementManager.lastComplitedLevelOnMap; break;
                case "defeatedEnemies":
                    currentProgress = ui.achievementManager.currentEnemiesDefeated; break;
                case "collectedGold":
                    currentProgress = ui.achievementManager.goldEarnedSoFar; break;
                default: break;
            }

            achievement.currentProgress = currentProgress;

            if (currentProgress >= targetProgress)
            {
                isUnlocked = true;
                achievement.isUnlocked = isUnlocked;
                progressBar.fillAmount = 1f;
                StartAnimation();
            }
            else
            {
                progressBar.fillAmount = (float)currentProgress / targetProgress;
                isUnlocked = false;
                achievement.isUnlocked = isUnlocked;
            }
        }else
        { 
            progressBar.fillAmount = 1f;
            StartAnimation();
        }
    }

    private void StartAnimation()
    {
        if (!animationStarted)
        {
            animator.SetTrigger("Unlock");
            animationStarted = true;
        }
    }
    public void GiveReward()
    {
        if (isUnlocked)
        {
            if (rewardType == "experience")
            {
                ui.playerData.AddExp(amount);
                rewardType = "coins";
                newAmount = _baseGoldAmount * levelOfAchievement * levelOfAchievement;
            }
            else if (rewardType == "coins")
            {
                ui.playerData.gold += amount;
                rewardType = "diamonds";
                newAmount = _baseDiamondsAmount * levelOfAchievement * 2;
            }
            else if (rewardType == "diamonds")
            {
                ui.playerData.diamonds += amount;
                rewardType = "experience";
                newAmount = _baseExpAmount * levelOfAchievement;
            }


            levelOfAchievement++;

            ui.achievementManager.achievements.Remove(achievement);

            ui.CreateNewSingleAchievementPanel(new Achievement(title, NewDescription(), targetProgress * 2,
                imageId, levelOfAchievement++, new Reward(rewardType, newAmount), typeOfDataProgress));

            ui.playerDataShowValuesController.UpdateAllValues();

            StartCoroutine(StartDestroyAnim());
        }       
    }

    public void CheckNumber()
    {
        string foundNumbers = new string(description.Where(char.IsDigit).ToArray());

        if (!string.IsNullOrEmpty(foundNumbers))
        {
            int number = int.Parse(foundNumbers);
            nextLevelTarget = number;
        }
    }

    private string NewDescription()
    {
        string text = description;
        Match match = Regex.Match(text, @"\d+");

        if (match.Success)
        {
            int newNumber = nextLevelTarget * 2;
            string newText = Regex.Replace(text, @"\d+", newNumber.ToString());

            return newText;
        }
        return text;
    }

    private IEnumerator StartDestroyAnim()
    {
        animator.SetTrigger("Destroy");
        if (audioManager != null)
            audioManager.GetComponent<AudioManager>().PlaySFX("ProgressGet");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
