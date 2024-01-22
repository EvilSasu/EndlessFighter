using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Achievement
{
    public string title;
    public string description;
    public int targetProgress;
    public int currentProgress;
    public bool isUnlocked;
    public int imageId;
    public int levelOfAchievement;
    public Reward reward;
    public string typeOfDataProgress;

    public Achievement(string title, string description, int targetProgress, int imageId, int levelOfAchievement, Reward reward, string typeOfDataProgress)
    {
        this.title = title;
        this.description = description;
        this.targetProgress = targetProgress;
        this.currentProgress = 0;
        this.isUnlocked = false;
        this.imageId = imageId;
        this.levelOfAchievement = levelOfAchievement;
        this.reward = reward;
        this.typeOfDataProgress = typeOfDataProgress;
    }
}

[Serializable]
public class Reward
{
    public string rewardType;
    public int amount;

    public Reward(string rewardType, int amount)
    {
        this.rewardType = rewardType;
        this.amount = amount;
    }
}

