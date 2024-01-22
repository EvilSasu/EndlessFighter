using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementData
{
    public List<Achievement> achievements = new List<Achievement>();

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
}
