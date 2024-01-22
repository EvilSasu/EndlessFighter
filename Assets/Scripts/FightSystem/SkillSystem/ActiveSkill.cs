using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    public SkillName skillName;
    public TextMeshProUGUI timerText;
    private Unit unit;

    public float timerDuration;
    private float currentTimer = 0f;
    private bool isTimerActive = false;
    private bool buffAdded = false;
    private Skill skill;
    private void Start()
    {       
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isTimerActive)
        {
            currentTimer -= Time.deltaTime;
            UpdateTimerDisplay();

            if (!buffAdded)
                AddBuff();

            if (currentTimer <= 0f)
            {
                EndSkill();
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        string minutes = Mathf.Floor(currentTimer / 60).ToString("00");
        string seconds = (currentTimer % 60).ToString("00");

        timerText.text = minutes + ":" + seconds;
    }

    public void StartSkill(Skill skill, Unit unit)
    {
        this.skill = skill;
        this.unit = unit;
        isTimerActive = true;
        currentTimer = timerDuration + 1;
        UpdateTimerDisplay();
        gameObject.SetActive(true);
    }

    private void EndSkill()
    {
        RemoveBuff();
        isTimerActive = false;
        buffAdded = false;
        currentTimer = timerDuration + 1;
        gameObject.SetActive(false);
    }

    private void AddBuff()
    {
        buffAdded = true;

        switch (skillName)
        {
            case SkillName.DefendYourself:
                unit.defenseWithPassivesAndBuffs += (int)(unit.defenseWithPassivesAndBuffs * skill.skillLevel * skill.bonusPerLevel / 100);
                unit.physicalDefenseWithPassivesAndBuffs = (int)(0.5f * unit.defenseWithPassivesAndBuffs);
                unit.magicDefenseWithPassivesAndBuffs = (int)(0.2f * unit.defenseWithPassivesAndBuffs);
                unit.rangeDefenseWithPassivesAndBuffs = (int)(0.3f * unit.defenseWithPassivesAndBuffs);
                break;
            case SkillName.Berserk:
                unit.physicalDmgWithPassivesAndBuffs += (int)(unit.physicalDmgWithPassives * skill.skillLevel * skill.bonusPerLevel / 100);
                break;
            case SkillName.Dodger:
                unit.dodgeChanceWithPassivesAndBuffs += ((unit.dodgeChanceWithPassives * 0.25f) + 
                                                        (unit.dodgeChanceWithPassives * skill.skillLevel * skill.bonusPerLevel / 100));
                break;
            case SkillName.CritFighter:
                unit.critDmgMultiplicationWithPassivesAndBuffs += ((unit.critDmgMultiplicationWithPassives * 0.5f) +
                                                        (unit.critDmgMultiplicationWithPassives * skill.skillLevel * skill.bonusPerLevel / 100));
                break;
            default: break;
        }
    }

    private void RemoveBuff()
    {
        buffAdded = false;

        switch (skillName)
        {
            case SkillName.DefendYourself:
                unit.defenseWithPassivesAndBuffs = unit.defenseWithPassives;
                unit.physicalDefenseWithPassivesAndBuffs = unit.physicalDefenseWithPassives;
                unit.magicDefenseWithPassivesAndBuffs = unit.magicDefenseWithPassives;
                unit.rangeDefenseWithPassivesAndBuffs = unit.rangeDefenseWithPassives;
                break;
            case SkillName.Berserk:
                unit.physicalDmgWithPassivesAndBuffs = unit.physicalDmgWithPassives;
                break;
            case SkillName.Dodger:
                unit.dodgeChanceWithPassivesAndBuffs = unit.dodgeChanceWithPassives;
                break;
            case SkillName.CritFighter:
                unit.critDmgMultiplicationWithPassivesAndBuffs = unit.critDmgMultiplicationWithPassives;
                break;
            default: break;
        }
    }
}
