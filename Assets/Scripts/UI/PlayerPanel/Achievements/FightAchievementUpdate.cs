using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAchievementUpdate : MonoBehaviour
{
    public AchievementManager achievementManager;

    public void UpdateAchievementData(int gold, int diamonds)
    {
        achievementManager.goldEarnedSoFar += gold;
        achievementManager.diamondsEarnedSoFar += diamonds;
    }

    public void UpdateEnemiesDefeated()
    {
        achievementManager.currentEnemiesDefeated++;
    }
}
