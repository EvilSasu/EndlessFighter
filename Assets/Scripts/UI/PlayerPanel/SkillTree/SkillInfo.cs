using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillInfo : MonoBehaviour
{
    public GameObject controller;
    private TextMeshProUGUI levelText;

    public SkillName skillName;
    public string nameToText;
    public string description;
    public int skillLevel;
    public SkillTypeEnum skillType;
    public List<Requirement> requirements;
    public int skillPointsCost;
    public Image imageOfSkill;

    public PlayerData playerData;
    public float cooldown;
    public float bonusPerLevel;

    private bool skillIsUnlockedInData = false;

    private Color normalColor = new(1f, 1f, 1f);
    private Color upragradableColorForActive = new(0.39f, 1f, 0.39f);
    private Color upragradableColorForPassive = new(0.6f, 1f, 0.6f);
    private bool areAllRequirementsUnlocked = false;
    private Image image;
    private void Awake()
    {
        levelText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        levelText.text = skillLevel.ToString();
        skillPointsCost = skillLevel + 1;
        gameObject.name = skillName.ToString();
        image = GetComponent<Image>();
        imageOfSkill = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        playerData = controller.GetComponent<SkillDescriptionPanelController>().playerData;
        SetupBaseRequiredLevel();
        skillIsUnlockedInData = CheckIfSkillUnlockedInData();
        SetupUpgradeColor();
    }

    private void OnEnable()
    {
        SetupUpgradeColor();
    }

    private void SetupBaseRequiredLevel()
    {
        foreach(var req in requirements)
        {
            req.baseRequiredLevel = req.requiredLevel;
            req.isUnlocked = false;
        }
    }

    private void Lockrequirements()
    {
        foreach (var req in requirements)
        {
            req.isUnlocked = false;
        }
    }

    public void SendDataToDescriptionController()
    {
        controller.SetActive(true);
        if (controller != null)
        {
            controller.GetComponent<SkillDescriptionPanelController>().UpdateSkillDescriptionPanel(this);
        }
    }

    private bool CheckIfSkillUnlockedInData()
    {
        foreach(var skill in playerData.unlockedSkillInfoList)
        {
            if(skill.skillName == skillName)
            {
                skillLevel = skill.skillLevel;
                SetupSkillUI();
                return true;
            }               
        }
        return false;
    }

    private void SetupSkillUI()
    {
        levelText.text = skillLevel.ToString();
        if (skillType == SkillTypeEnum.Active)
            skillPointsCost = skillLevel + 1;
        else
            skillPointsCost = 1;
        foreach(var requirement in requirements)
        {
            requirement.requiredLevel = requirement.baseRequiredLevel * (skillLevel + 1);
            requirement.isUnlocked = false;
        }
        SetupUpgradeColor();
    }

    public void SetupUpgradeColor()
    {
        Lockrequirements();
        TryUnlockRequirements();
        areAllRequirementsUnlocked = CheckIfAllRequirementsAreUnlocked(requirements);

        if (areAllRequirementsUnlocked && playerData.freeSkillPoints > 0)
        {
            if (skillType == SkillTypeEnum.Active)
                image.color = upragradableColorForActive;
            else if (skillType == SkillTypeEnum.Passive)
                image.color = upragradableColorForPassive;
        }            
        else
            image.color = normalColor;
    }

    private void TryUnlockRequirements()
    {
        foreach(var requirement in requirements)
        {
            CheckIfRequirementCanBeUnlock(requirement);
        }
    }

    public bool CheckIfAllRequirementsAreUnlocked(List<Requirement> requirements)
    {
        foreach(var requirement in requirements)
        {
            if(requirement.isUnlocked == false)
                return false;
        }
        return true;
    }

    public void UpdateSkillUIInfo()
    {
        skillLevel++;
        SetupSkillUI();
    }

    public void CheckIfRequirementCanBeUnlock(Requirement requirement)
    {
        if (requirement != null)
        {
            if (requirement.type == RequirementType.Attribute)
            {
                switch (requirement.attributeType)
                {
                    case AttributeType.Strength: if (playerData.strength >= requirement.requiredLevel) { requirement.isUnlocked = true; } break;
                    case AttributeType.Agility: if (playerData.agility >= requirement.requiredLevel) { requirement.isUnlocked = true; } break;
                    case AttributeType.Intelligence: if (playerData.intelligence >= requirement.requiredLevel) { requirement.isUnlocked = true; } break;
                    case AttributeType.Defense: if (playerData.defense >= requirement.requiredLevel) { requirement.isUnlocked = true; } break;
                    case AttributeType.PlayerLevel: if (playerData.unitLevel >= requirement.requiredLevel) { requirement.isUnlocked = true; } break;
                    default: break;
                }
            }
            else
            {
                if (playerData.unlockedSkillInfoList.Count > 0)
                {
                    foreach (var skill in playerData.unlockedSkillInfoList)
                    {
                        if(skill.skillName == requirement.skillName)
                        {
                            if(skill.skillLevel >= requirement.requiredLevel)
                                requirement.isUnlocked = true;
                            else
                                requirement.isUnlocked = false;
                        }
                    }
                }
                else
                    requirement.isUnlocked = false;
            }
        }
    }
}
