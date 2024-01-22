using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    public PlayerData playerData;
    public BattleController battleController;
    public List<GameObject> listOfPanelSkills = new List<GameObject>();
    public List<Sprite> listOfSkillSprites = new List<Sprite>();

    private void Start()
    {
        SetupSkillsUI();
    }
    
    private void SetupSkillsUI()
    {
        int indexOfListOfPanelSkills = 0;
        foreach (var skill in playerData.unlockedSkillInfoList)
        {
            if (skill.skillType == SkillTypeEnum.Active)
            {
                listOfPanelSkills[indexOfListOfPanelSkills].GetComponent<Skill>().skillName = skill.skillName;
                listOfPanelSkills[indexOfListOfPanelSkills].GetComponent<Skill>().cooldown = skill.cooldown * playerData.cooldownAmount;
                listOfPanelSkills[indexOfListOfPanelSkills].GetComponent<Skill>().skillLevel = skill.skillLevel;
                listOfPanelSkills[indexOfListOfPanelSkills].GetComponent<Skill>().bonusPerLevel = skill.bonusPerLevel;
                listOfPanelSkills[indexOfListOfPanelSkills].SetActive(true);
                ChooseSpriteForSkill(listOfPanelSkills[indexOfListOfPanelSkills]);
                indexOfListOfPanelSkills++;
            }
        }
    }

    private void ChooseSpriteForSkill(GameObject skillPanel)
    {
        SkillName skillName = skillPanel.GetComponent<Skill>().skillName;
        Image skillImage = skillPanel.GetComponent<Skill>().GetChildImage();
        switch (skillName)
        {
            case SkillName.DoubleAttack:
                skillImage.sprite = listOfSkillSprites[0]; break;
            case SkillName.DefendYourself:
                skillImage.sprite = listOfSkillSprites[1]; break;
            case SkillName.Berserk:
                skillImage.sprite = listOfSkillSprites[2]; break;
            case SkillName.Heal:
                skillImage.sprite = listOfSkillSprites[3]; break;
            case SkillName.Fireball:
                skillImage.sprite = listOfSkillSprites[4]; break;
            case SkillName.LightingStrike:
                skillImage.sprite = listOfSkillSprites[5]; break;
            case SkillName.BowShot:
                skillImage.sprite = listOfSkillSprites[6]; break;
            case SkillName.Dodger:
                skillImage.sprite = listOfSkillSprites[7]; break;
            case SkillName.CritFighter:
                skillImage.sprite = listOfSkillSprites[8]; break;
            default:
                break;
        }
    }

    public void UseSkill(Skill skill)
    {
        StartCoroutine(StartCooldown(skill));
    }

    IEnumerator StartCooldown(Skill skill)
    {
        ActivateSkill(skill);
        skill.GetButton().interactable = false;
        float startTime = Time.time;
        float currentTime = 0f;

        while (currentTime < skill.cooldown)
        {
            currentTime = Time.time - startTime;
            float progress = currentTime / skill.cooldown;

            skill.GetImage().fillAmount = progress;
            skill.GetChildImage().fillAmount = progress;

            yield return new WaitForSeconds(0.1f);
        }
        skill.GetButton().interactable = true;
    }

    private void ActivateSkill(Skill skill)
    {
        switch (skill.skillName)
        {
            case SkillName.DoubleAttack: battleController.AddDoubleAttack(skill); break;
            case SkillName.DefendYourself: battleController.AddDefendYourself(skill); break;
            case SkillName.Berserk: battleController.AddBerserk(skill); break;
            case SkillName.Heal: battleController.AddHeal(skill); break;
            case SkillName.Fireball: battleController.AddFireball(skill); break;
            case SkillName.LightingStrike: battleController.AddLightingStrike(skill); break;
            case SkillName.BowShot: battleController.AddBowShot(skill); break;
            case SkillName.Dodger: battleController.AddDodger(skill); break;
            case SkillName.CritFighter: battleController.AddCritFighter(skill); break;
            default: break;
        }
    }
}
