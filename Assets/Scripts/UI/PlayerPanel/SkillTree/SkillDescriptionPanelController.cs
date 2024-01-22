using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillDescriptionPanelController : MonoBehaviour
{
    public PlayerData playerData;
    public SetSkillPointsToText freePointsText;

    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillLevel;
    public TextMeshProUGUI skillType;
    public TextMeshProUGUI skillRequirements;
    public TextMeshProUGUI skillDescription;
    public TextMeshProUGUI skillUpgradeCost;
    public Button upgradeButton;
    public Image skillImage;
    public List<GameObject> skillInfoList;

    private string startTextFormatGold = "<color=#FCDB00><b>"; // Gold text
    private string startTextFormatRed = "<color=#FF0000><b>"; // Red text
    private string endTextFormat = "</color></b>";

    private SkillInfo skillToUpgrade;
    public void UpdateSkillDescriptionPanel(SkillInfo skillInfo)
    {
        skillToUpgrade = skillInfo;
        bool areAllRequirementsUnlock = false;
        skillName.text = skillInfo.nameToText;
        skillImage.sprite = skillInfo.imageOfSkill.sprite;
        skillLevel.text = new string("Skill level: " + startTextFormatGold + skillInfo.skillLevel.ToString() + endTextFormat);
        skillType.text = new string("Skill Type: " + startTextFormatGold + skillInfo.skillType.ToString() + endTextFormat);
        skillRequirements.text = new string("Requirements: ");

        if (skillInfo.requirements != null && skillInfo.requirements.Count > 0) 
        {
            int index = 0;
            foreach (var requirement in skillInfo.requirements)
            {
                skillToUpgrade.CheckIfRequirementCanBeUnlock(requirement);
                areAllRequirementsUnlock = requirement.isUnlocked;
                if (index == skillInfo.requirements.Count - 1)
                {
                    SetupLastRequirementText(requirement);
                }
                else
                {
                    SetupRequirementText(requirement);
                }

                index++;  
            }
        }
        else
        {
            skillRequirements.text += new string(" None");
        }

        skillDescription.text = new string("Effect: " + startTextFormatGold + skillInfo.description + endTextFormat);

        if(playerData.freeSkillPoints >= skillInfo.skillLevel)
            skillUpgradeCost.text = new string("Upgrade cost: " + startTextFormatGold + skillInfo.skillPointsCost.ToString() + " SP" + endTextFormat);
        else
            skillUpgradeCost.text = new string("Upgrade cost: " + startTextFormatRed + skillInfo.skillPointsCost.ToString() + " SP" + endTextFormat);


        areAllRequirementsUnlock = skillToUpgrade.CheckIfAllRequirementsAreUnlocked(skillInfo.requirements);

        if (playerData.freeSkillPoints >= skillInfo.skillLevel && areAllRequirementsUnlock)
        {
            upgradeButton.interactable = true;
        }
        else
            upgradeButton.interactable = false;
    }

    private void SetupRequirementText(Requirement requirement)
    {
        if(requirement.type == RequirementType.Attribute)
        {
            if (requirement.isUnlocked)
                skillRequirements.text += new string(" " + startTextFormatGold + requirement.attributeType.ToString() + " lvl. " + requirement.requiredLevel + endTextFormat + ",");
            else
                skillRequirements.text += new string(" " + startTextFormatRed + requirement.attributeType.ToString() + " lvl. " + requirement.requiredLevel + endTextFormat + ",");
        }
        else
        {
            if (requirement.isUnlocked)
                skillRequirements.text += new string(" " + startTextFormatGold + requirement.skillName + " lvl. " + requirement.requiredLevel + endTextFormat + ",");
            else
                skillRequirements.text += new string(" " + startTextFormatRed + requirement.skillName + " lvl. " + requirement.requiredLevel + endTextFormat + ",");
        }
    }

    private void SetupLastRequirementText(Requirement requirement)
    {
        if (requirement.type == RequirementType.Attribute)
        {
            if (requirement.isUnlocked)
                skillRequirements.text += new string(" " + startTextFormatGold + requirement.attributeType.ToString() + " lvl. " + requirement.requiredLevel + endTextFormat);
            else
                skillRequirements.text += new string(" " + startTextFormatRed + requirement.attributeType.ToString() + " lvl. " + requirement.requiredLevel + endTextFormat);
        }
        else
        {
            if (requirement.isUnlocked)
                skillRequirements.text += new string(" " + startTextFormatGold + requirement.skillName + " lvl. " + requirement.requiredLevel + endTextFormat);
            else
                skillRequirements.text += new string(" " + startTextFormatRed + requirement.skillName + " lvl. " + requirement.requiredLevel + endTextFormat);
        }
    }

    public void UnlockSkill()
    {
        bool wasInData = false;
        if(skillToUpgrade != null)
        {
            playerData.freeSkillPoints--;
            skillToUpgrade.UpdateSkillUIInfo();
            foreach (var skill in playerData.unlockedSkillInfoList)
            {
                if(skill.skillName == skillToUpgrade.skillName)
                {
                    skill.skillLevel++;
                    wasInData = true;
                    playerData.SavePlayerDataToPlayerPrefs();
                }
            }
            if(!wasInData)
            {
                SkillInfoSS newSkillToAdd = new SkillInfoSS();
                newSkillToAdd.skillName = skillToUpgrade.skillName;
                newSkillToAdd.skillLevel = skillToUpgrade.skillLevel;
                newSkillToAdd.skillType = skillToUpgrade.skillType;
                newSkillToAdd.cooldown = skillToUpgrade.cooldown;
                newSkillToAdd.bonusPerLevel = skillToUpgrade.bonusPerLevel;
                playerData.unlockedSkillInfoList.Add(newSkillToAdd);
                playerData.SavePlayerDataToPlayerPrefs();
            }
            UpdateSkillDescriptionPanel(skillToUpgrade);
            freePointsText.SetupFreeSkillPoints();
        }
        CheckAllSkillsRequirements();
    }

    private void CheckAllSkillsRequirements()
    {
        foreach(var skill in skillInfoList)
        {
            skill.GetComponent<SkillInfo>().SetupUpgradeColor();
        }
    }
}
