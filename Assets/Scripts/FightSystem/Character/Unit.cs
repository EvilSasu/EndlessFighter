using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isPlayer;
    public string unitName;
    public int unitLevel;
    public float maxHp;
    public float maxHpWithPassives;
    public float currentHp;
    public HealthManager healthManager;

    public int strength;
    public int strengthWithPassives;
    public int strengthWithPassivesAndBuffs;

    public int agility;
    public int agilityWithPassives;
    public int agilityWithPassivesAndBuffs;

    public int intelligence;
    public int intelligenceWithPassives;
    public int intelligenceWithPassivesAndBuffs;

    public int defense;
    public int defenseWithPassives;
    public int defenseWithPassivesAndBuffs;

    public int physicalDefense;
    public int physicalDefenseWithPassives;
    public int physicalDefenseWithPassivesAndBuffs;

    public int magicDefense;
    public int magicDefenseWithPassives;
    public int magicDefenseWithPassivesAndBuffs;

    public int rangeDefense;
    public int rangeDefenseWithPassives;
    public int rangeDefenseWithPassivesAndBuffs;

    public int charisma;

    private int _baseStrenght = 5;
    private int _baseAgility = 5;
    private int _baseIntelligence = 5;
    private int _baseDefense = 5;
    private int _baseCharisma = 5;

    public float physicalDmg;
    public float physicalDmgWithPassives;
    public float physicalDmgWithPassivesAndBuffs;

    public float rangeDmg;
    public float rangeDmgWithPassives;
    public float rangeDmgWithPassivesAndBuffs;

    public float magicDmg;
    public float magicDmgWithPassives;
    public float magicDmgWithPassivesAndBuffs;

    public float dodgeChance;
    public float dodgeChanceWithPassives;
    public float dodgeChanceWithPassivesAndBuffs;

    public float critChance;
    public float critChanceWithPassives;
    public float critChanceWithPassivesAndBuffs;

    public float critDmgMultiplication;
    public float critDmgMultiplicationWithPassives;
    public float critDmgMultiplicationWithPassivesAndBuffs;

    public PlayerData playerData;
    public FightAnimationController fightAnimationController;
    public List<SkillInfoSS> unlockedSkillInfoList = new List<SkillInfoSS>();

    private float _baseHp = 50;
    private float _basePhysicalDmg = 5;
    private float _baseRangeDmg = 10;
    private float _baseMagicDmg = 20;

    public float bonusExp = 1; // 1 = 100%, 1.2 = 120%
    public float bonusGold = 1; // 1 = 100%, 1.2 = 120%
    public float energyRegenerationAmount = 1;  // 1 = 100%, 1.2 = 120%

    public float bonusStrengthTrainingSpeedPercent;
    public float bonusAgilityTrainingSpeedPercent;
    public float bonusIntelligenceTrainingSpeedPercent;
    public float bonusDefenseTrainingSpeedPercent;

    public float bonusBuildingSpeedPercent;
    public float cooldownAmount = 1; // 1 = 100%, 0.5 = 50%

    private void Awake()
    {
        CalculateAll();
    }

    private void Start()
    {
        CalculateAll();
        fightAnimationController = GetComponent<FightAnimationController>();
    } 

    public void SetupStatsFromPlayerData()
    {

    }

    public void CalculateAll()
    {
        if (isPlayer)
        {
            if (playerData != null)
            {
                isPlayer = playerData.isPlayer;
                unitLevel = playerData.unitLevel;
                unitName = playerData.unitName;
                maxHp = playerData.maxHp;
                maxHpWithPassives = playerData.maxHpWithPassives;
                currentHp = playerData.currentHp;

                // Stats
                strength = playerData.strength;
                strengthWithPassives = playerData.strengthWithPassives;
                strengthWithPassivesAndBuffs = playerData.strengthWithPassivesAndBuffs;
                agility = playerData.agility;
                agilityWithPassives = playerData.agilityWithPassives;
                agilityWithPassivesAndBuffs = playerData.agilityWithPassivesAndBuffs;
                intelligence = playerData.intelligence;
                intelligenceWithPassives = playerData.intelligenceWithPassives;
                intelligenceWithPassivesAndBuffs = playerData.intelligenceWithPassivesAndBuffs;
                defense = playerData.defense;
                defenseWithPassives = playerData.defenseWithPassives;
                defenseWithPassivesAndBuffs = playerData.defenseWithPassivesAndBuffs;
                charisma = playerData.charisma;
                unlockedSkillInfoList = playerData.unlockedSkillInfoList;

                // Damage
                physicalDmg = playerData.physicalDmg;
                physicalDmgWithPassives = playerData.physicalDmgWithPassives;
                physicalDmgWithPassivesAndBuffs = playerData.physicalDmgWithPassivesAndBuffs;
                magicDmg = playerData.magicDmg;
                magicDmgWithPassives = playerData.magicDmgWithPassives;
                magicDmgWithPassivesAndBuffs = playerData.magicDmgWithPassivesAndBuffs;
                rangeDmg = playerData.rangeDmg;
                rangeDmgWithPassives = playerData.rangeDmgWithPassives;
                rangeDmgWithPassivesAndBuffs = playerData.rangeDmgWithPassivesAndBuffs;

                // Defense
                physicalDefense = playerData.physicalDefense;
                physicalDefenseWithPassives = playerData.physicalDefenseWithPassives;
                physicalDefenseWithPassivesAndBuffs = playerData.physicalDefenseWithPassivesAndBuffs;
                magicDefense = playerData.magicDefense;
                magicDefenseWithPassives = playerData.magicDefenseWithPassives;
                magicDefenseWithPassivesAndBuffs = playerData.magicDefenseWithPassivesAndBuffs;
                rangeDefense = playerData.rangeDefense;
                rangeDefenseWithPassives = playerData.rangeDefenseWithPassives;
                rangeDefenseWithPassivesAndBuffs = playerData.rangeDefenseWithPassivesAndBuffs;

                // Dodge
                dodgeChance = playerData.dodgeChance;
                dodgeChanceWithPassives = playerData.dodgeChanceWithPassives;
                dodgeChanceWithPassivesAndBuffs = playerData.dodgeChanceWithPassivesAndBuffs;

                // Crit
                critChance = playerData.critChance;
                critChanceWithPassives = playerData.critChanceWithPassives;
                critChanceWithPassivesAndBuffs = playerData.critChanceWithPassivesAndBuffs;
                critDmgMultiplication = playerData.critDmgMultiplication;
                critDmgMultiplicationWithPassives = playerData.critDmgMultiplicationWithPassives;
                critDmgMultiplicationWithPassivesAndBuffs = playerData.critDmgMultiplicationWithPassivesAndBuffs;

                cooldownAmount = playerData.cooldownAmount;
            }
        }
        else
        {
            CalculateStatsForEnemy();
            SetupStatsWithPassive();
            CalculateStatsWithPassives();
            CalculateDmg();
            SetupStatsWithPassiveAndBuffs();

            CalculateMaxHp();
        }         
    }

    private void CalculateMaxHp()
    {
        maxHp = _baseHp;
        maxHp = (int)(1.25f * maxHp * unitLevel);
        maxHpWithPassives = maxHp;
        currentHp = maxHpWithPassives;
    }

    private void CalculateDmg()
    {
        CalculatePhysicalDmg();
        CalculateRangeDmg();
        CalculateMagicDmg();
    }

    private void CalculatePhysicalDmg()
    {
        physicalDmgWithPassives = physicalDmgWithPassives + (int)(1.25 * strengthWithPassives);
    }

    private void CalculateRangeDmg()
    {
        rangeDmgWithPassives = rangeDmgWithPassives + (int)(1.5 * agilityWithPassives);
    }

    private void CalculateMagicDmg()
    {
        magicDmgWithPassives = magicDmgWithPassives + (int)(2 * intelligenceWithPassives);
    }

    private void CalculateStatsForEnemy()
    {
        if (!isPlayer)
        {
            CalculateEnemyStrenght();
            CalculateEnemyAgility();
            CalculateEnemyInteligence();
            CalculateEnemyDefense();
            CalculateEnemyCharisma();
        }
    }

    private void CalculateEnemyStrenght()
    {
        int _minRange = (int)(0.75f * (float)(_baseStrenght * unitLevel));
        int _maxRange = (int)(1.25f * (float)(_baseStrenght * unitLevel));
        strength = Random.Range(_minRange, _maxRange);
    }

    private void CalculateEnemyAgility()
    {
        int _minRange = (int)(0.75f * (float)(_baseAgility * unitLevel));
        int _maxRange = (int)(1.25f * (float)(_baseAgility * unitLevel));
        agility = Random.Range(_minRange, _maxRange);
    }

    private void CalculateEnemyInteligence()
    {
        int _minRange = (int)(0.75f * (float)(_baseIntelligence * unitLevel));
        int _maxRange = (int)(1.25f * (float)(_baseIntelligence * unitLevel));
        intelligence = Random.Range(_minRange, _maxRange);
    }

    private void CalculateEnemyDefense()
    {
        int _minRange = (int)(0.75f * (float)(_baseDefense * unitLevel));
        int _maxRange = (int)(1.25f * (float)(_baseDefense * unitLevel));
        defense = Random.Range(_minRange, _maxRange);
    }

    private void CalculateEnemyCharisma()
    {
        int _minRange = (int)(0.75f * (float)(_baseCharisma * unitLevel));
        int _maxRange = (int)(1.25f * (float)(_baseCharisma * unitLevel));
        charisma = Random.Range(_minRange, _maxRange);
    }

    private void SetupStatsWithPassive()
    {
        strengthWithPassives = strength;
        agilityWithPassives = agility;
        intelligenceWithPassives = intelligence;
        defenseWithPassives = defense;
        maxHpWithPassives = maxHp;

        SetupDodgeChance();
        SetupCritChanceAndDmg();
        SetupeDefense();
        SetupDamagesWithPassive();
    }

    private void SetupCritChanceAndDmg()
    {
        float baseCritChance = 5f;
        float baseCritDamageMultiplication = 1.5f;

        critChance = baseCritChance;
        critChanceWithPassives = critChance;

        critDmgMultiplication = baseCritDamageMultiplication;
        critDmgMultiplicationWithPassives = critDmgMultiplication;
    }

    private void SetupeDefense()
    {
        physicalDefense = (int)(defense * 0.5f);
        physicalDefenseWithPassives = physicalDefense;
        magicDefense = (int)(defense * 0.2f);
        magicDefenseWithPassives = magicDefense;
        rangeDefense = (int)(defense * 0.3f);
        rangeDefenseWithPassives = rangeDefense;
    }

    private void SetupDamagesWithPassive()
    {
        physicalDmg = _basePhysicalDmg;
        magicDmg = _baseMagicDmg;
        rangeDmg = _baseRangeDmg;

        physicalDmgWithPassives = physicalDmg;
        magicDmgWithPassives = magicDmg;
        rangeDmgWithPassives = rangeDmg;
    }

    private void SetupStatsWithPassiveAndBuffs()
    {
        strengthWithPassivesAndBuffs = strengthWithPassives;
        agilityWithPassivesAndBuffs = agilityWithPassives;
        intelligenceWithPassivesAndBuffs = intelligenceWithPassives;
        defenseWithPassivesAndBuffs = defenseWithPassives;

        physicalDefenseWithPassivesAndBuffs = physicalDefenseWithPassives;
        magicDefenseWithPassivesAndBuffs = magicDefenseWithPassives;
        rangeDefenseWithPassivesAndBuffs = rangeDefenseWithPassives;

        critChanceWithPassivesAndBuffs = critChanceWithPassives;
        critDmgMultiplicationWithPassivesAndBuffs = critDmgMultiplicationWithPassives;

        physicalDmgWithPassivesAndBuffs = physicalDmgWithPassives;
        magicDmgWithPassivesAndBuffs = magicDmgWithPassives;
        rangeDmgWithPassivesAndBuffs = rangeDmgWithPassives;

        dodgeChanceWithPassivesAndBuffs = dodgeChanceWithPassives;
    }

    private void SetupDodgeChance()
    {
        dodgeChance = (int)(agility * 0.1f);
        dodgeChanceWithPassives = dodgeChance;
        if (dodgeChanceWithPassives > 80)
            dodgeChanceWithPassives = 80;
    }


    private void CalculateStatsWithPassives()
    {
        foreach (var skill in unlockedSkillInfoList)
        {
            if (skill.skillType == SkillTypeEnum.Passive)
            {
                switch (skill.skillName)
                {
                    case SkillName.PhysicalDmgUp:
                        physicalDmgWithPassives += (int)(physicalDmg * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.PhysicalDefenseUp:
                        physicalDefenseWithPassives += (int)(physicalDefense * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.RangeDefense:
                        rangeDefenseWithPassives += (int)(rangeDefense * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.HpUp:
                        maxHpWithPassives += (int)(maxHp * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.SpartanDefense:
                        defenseWithPassives += (int)(strengthWithPassives * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.WeaponMaster: // need to create if for weapon type
                        strengthWithPassives += (int)(strength * skill.skillLevel * skill.bonusPerLevel / 100);
                        defenseWithPassives += (int)(defense * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.StrengthTrainingUp:
                        bonusStrengthTrainingSpeedPercent += (skill.skillLevel * skill.bonusPerLevel);
                        break;
                    case SkillName.DefenseTrainingUp:
                        bonusDefenseTrainingSpeedPercent += (skill.skillLevel * skill.bonusPerLevel);
                        break;

                    case SkillName.MagicDmgUp:
                        magicDmgWithPassives += (int)(magicDmg * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.SkillCooldownDown:
                        cooldownAmount = cooldownAmount - (skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.MagicDefenseUp:
                        magicDefenseWithPassives += (int)(magicDefense * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.IntelligenceUp:
                        intelligenceWithPassives += (int)(intelligence * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.IntelligenceTrainingUp:
                        bonusIntelligenceTrainingSpeedPercent += (skill.skillLevel * skill.bonusPerLevel);
                        break;
                    case SkillName.ExpUp:
                        bonusExp = bonusExp + (skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.PhysicalDmgUpFromIntelligence:
                        physicalDefenseWithPassives += (int)(intelligence * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.BuildingSpeedBonus:
                        bonusBuildingSpeedPercent += (skill.skillLevel * skill.bonusPerLevel);
                        break;

                    case SkillName.RangeDmgUp:
                        rangeDmgWithPassives += (int)(rangeDmg * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.DodgeBonus:
                        dodgeChanceWithPassives += (int)(dodgeChance * skill.skillLevel * skill.bonusPerLevel / 100);
                        if (dodgeChanceWithPassives > 80)
                            dodgeChanceWithPassives = 80;
                        break;
                    case SkillName.AgilityBonus:
                        agilityWithPassives += (int)(agility * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.AgilityTrainingUp:
                        bonusAgilityTrainingSpeedPercent += (skill.skillLevel * skill.bonusPerLevel);
                        break;
                    case SkillName.CritDmgUp:
                        critDmgMultiplicationWithPassives += (int)(critDmgMultiplication * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.CritChanceUp:
                        critChanceWithPassives += (int)(critChance * skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.GoldDropUp:
                        bonusGold = bonusGold + (skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                    case SkillName.EnergyRegenerationUp:
                        energyRegenerationAmount = energyRegenerationAmount + (skill.skillLevel * skill.bonusPerLevel / 100);
                        break;
                }
            }
        }
    }
}
