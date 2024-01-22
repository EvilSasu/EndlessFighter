using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyLevelsDataSS 
{
    public List<EnemyInfo> enemyLevels = new List<EnemyInfo>();
    public string nameOfLevel;
    public long TimeToBeat;
}
