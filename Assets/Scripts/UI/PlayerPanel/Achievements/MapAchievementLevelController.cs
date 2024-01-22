using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAchievementLevelController : MonoBehaviour
{
    public GameObject gameData;
    public int lastPlayedLevel = 0;

    private void Start()
    {
        StartCoroutine(SetLastLevelToAchievements(0.2f));
    }

    public void SetLastPlayedLevel(int level)
    {
        if(lastPlayedLevel < level)
            lastPlayedLevel = level;
    }

    private IEnumerator SetLastLevelToAchievements(float time)
    {
        yield return new WaitForSeconds(time);
        gameData.GetComponent<AchievementManager>().SetLastPlayedLevel(lastPlayedLevel);
    }
}
