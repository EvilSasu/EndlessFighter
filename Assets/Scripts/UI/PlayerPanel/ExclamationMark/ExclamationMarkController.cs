using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMarkController : MonoBehaviour
{
    public PlayerData playerData;
    public AchievementManager achievementManager;
    public List<ExclamationMark> exclamationMarks = new List<ExclamationMark>();
    public bool thereIsFreeSkillPoint;
    public bool thereIsFreeAttributesPoint;
    public bool thereIsSomethingInPlayerPanel;
    public bool thereIsAchievementUnlocked;

    private bool hasChecked = false;

    private void Start()
    {
        StartCoroutine(LaterStart(0.2f));
    }

    private void Update()
    {
        if(!hasChecked)
            StartCoroutine(LaterStart(0.2f));
    }

    private IEnumerator LaterStart(float time)
    {
        hasChecked = true;
        yield return new WaitForSeconds(time);
        CheckData();
        hasChecked = false;
    }

    private void CheckData()
    {
        if (playerData.freeAtributesPoints > 0 || playerData.freeSkillPoints > 0 || achievementManager.CheckIfAnyAchievemntUnlocked())
            thereIsSomethingInPlayerPanel = true;
        else
            thereIsSomethingInPlayerPanel = false;

        if (playerData.freeAtributesPoints > 0)
            thereIsFreeAttributesPoint = true;
        else
            thereIsFreeAttributesPoint = false;

        if (playerData.freeSkillPoints > 0)
            thereIsFreeSkillPoint = true;
        else
            thereIsFreeSkillPoint = false;

        if (achievementManager.CheckIfAnyAchievemntUnlocked())
            thereIsAchievementUnlocked = true;
        else
            thereIsAchievementUnlocked = false;


        foreach (var item in exclamationMarks)
        {
            if (item.status == EnumStatusInVillage.AnyFromPlayerPanel)
            {
                if(thereIsSomethingInPlayerPanel)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            }
            else if (item.status == EnumStatusInVillage.AttributesPoints)
            {
                if (thereIsFreeAttributesPoint)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            }
            else if (item.status == EnumStatusInVillage.SkillPoints)
            {
                if (thereIsFreeSkillPoint)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            }
            else if(item.status == EnumStatusInVillage.AnyAchievementUnlocked)
            {
                if (thereIsAchievementUnlocked)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            }
        }
    }
}
