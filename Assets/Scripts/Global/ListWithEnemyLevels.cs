using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListWithEnemyLevels : MonoBehaviour
{
    public List<EnemyInfo> enemyLevels = new List<EnemyInfo>();
    public long timeToBeat;

    private long _baseTimeToBeat = 20;

    private void Awake()
    {
        if (timeToBeat <= 0)
            timeToBeat = _baseTimeToBeat;
    }

    public void SaveEnemiesToPlayerPrefs()
    {
        EnemyLevelsDataSS enemyLevelsDataSS = new EnemyLevelsDataSS();
        foreach(EnemyInfo enemy in enemyLevels)
        {
            enemyLevelsDataSS.enemyLevels.Add(enemy);
        }
        enemyLevelsDataSS.nameOfLevel = gameObject.name;
        enemyLevelsDataSS.TimeToBeat = timeToBeat;
        string json = JsonUtility.ToJson(enemyLevelsDataSS);
        PlayerPrefs.SetString("EnemiesOnNextLevel", json);
        PlayerPrefs.Save();
    }
}
