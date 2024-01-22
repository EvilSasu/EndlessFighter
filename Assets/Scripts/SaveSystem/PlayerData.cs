using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    #region Fields
    public bool isPlayer;
    public string unitName;
    public int unitLevel;
    public float maxHp;
    public float maxHpWithPassives;
    public float currentHp;

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
    public int gold;
    public int maxExp;
    public int currentExp;
    public int diamonds;
    public int freeAtributesPoints;
    public int freeSkillPoints;
    public List<SkillInfoSS> unlockedSkillInfoList = new List<SkillInfoSS>();

    private int _baseLevelNeededExp = 50;
    private float _baseHp = 50;
    private float _basePhysicalDmg = 5;
    private float _baseRangeDmg = 10;
    private float _baseMagicDmg = 20;

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

    public float bonusExp = 1; // 1 = 100%, 1.2 = 120%
    public float bonusGold = 1; // 1 = 100%, 1.2 = 120%
    public float energyRegenerationAmount = 1;  // 1 = 100%, 1.2 = 120%

    public float bonusStrengthTrainingSpeedPercent;
    public float bonusAgilityTrainingSpeedPercent;
    public float bonusIntelligenceTrainingSpeedPercent;
    public float bonusDefenseTrainingSpeedPercent;

    public float bonusBuildingSpeedPercent;
    public float cooldownAmount = 1; // 1 = 100%, 0.5 = 50%

    public float minDamage;
    public float maxDamage;

    public List<ItemDataSS> itemsInInventory;
    public List<ItemDataSS> itemsInEQ;
    public List<Sprite> itemsSprites;
    private InventoryBonusStats inventoryStats;
    #endregion

    #region Public Methods
    public void InitializeData()
    {
        PlayerDataSS playerData = LoadPlayerData();
        if (playerData == null)
            playerData = LoadPlayerData();

        FromDataToGameObejct(playerData);
        CalculateLevel();
        CalculateMaxHp();

        SetupStatsWithPassive();
        CalculateStatsWithPassives();
        

        if (freeSkillPoints < 0)
            freeSkillPoints = 0;
        if (freeAtributesPoints < 0)
            freeAtributesPoints = 0;

        inventoryStats = GetComponent<InventoryBonusStats>();
        inventoryStats.SetPlayerData(this);
        inventoryStats.UpdateValuesAfterLoad(itemsInEQ);

        CalculateDmg();
        SetupStatsWithPassiveAndBuffs();
        SetupDodgeChance();
    }

    public void AddExp(int exp)
    {
        currentExp += (int)(exp * bonusExp);
        CalculateLevel();
    }

    public void AddGold(int earnedGold)
    {
        gold += (int)(earnedGold * bonusGold);
    }

    public void AddDiamonds(int earnedDiamonds)
    {
        diamonds += earnedDiamonds;
    }

    public void SavePlayerDataToPlayerPrefs()
    {
        PlayerDataSS playerData = new PlayerDataSS();
        playerData.isPlayer = isPlayer;
        playerData.unitName = unitName;
        playerData.unitLevel = unitLevel;
        playerData.maxHp = maxHp;
        playerData.currentHp = maxHp;
        playerData.strength = strength;
        playerData.agility = agility;
        playerData.intelligence = intelligence;
        playerData.defense = defense;
        playerData.charisma = charisma;
        playerData.gold = gold;
        playerData.maxExp = maxExp;
        playerData.currentExp = currentExp;
        playerData.diamonds = diamonds;
        playerData.freeSkillPoints = freeSkillPoints;
        playerData.freeAtributesPoints = freeAtributesPoints;
        playerData.unlockedSkillInfoList = unlockedSkillInfoList;
        playerData.itemsInInventory = itemsInInventory;
        playerData.itemsInEQ = itemsInEQ;
        playerData.inventoryStats = inventoryStats;
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
    }

    public void UpdateItemsInEQList(int index)
    {
        ItemDataSS removedItem = itemsInEQ[index];
        itemsInEQ.RemoveAt(index);
        itemsInInventory.Add(removedItem);
        inventoryStats.UpdateValuesAfterLoad(itemsInEQ);
        SavePlayerDataToPlayerPrefs();
    }

    public void UpdateItemsInInventoryList(int index)
    {
        ItemDataSS removedItem = itemsInInventory[index];
        itemsInInventory.RemoveAt(index);
        itemsInEQ.Add(removedItem);
        inventoryStats.UpdateValuesAfterLoad(itemsInEQ);
        SavePlayerDataToPlayerPrefs();
    }

    #endregion

    #region Private Methods
    private void CalculateLevel()
    {
        maxExp = (int)((_baseLevelNeededExp * unitLevel) * 1.25f);
        while (currentExp >= maxExp)
        {
            maxExp = (int)((_baseLevelNeededExp * unitLevel) * 1.25f);
            if (currentExp >= maxExp)
            {
                currentExp = currentExp - maxExp;
                unitLevel++;
                freeSkillPoints++;
                freeAtributesPoints += 5;
            }
        }
    }

    private PlayerDataSS LoadPlayerData()
    {
        string jsonPlayerData = PlayerPrefs.GetString("PlayerData", "");

        if (!string.IsNullOrEmpty(jsonPlayerData))
        {
            PlayerDataSS playerData = JsonUtility.FromJson<PlayerDataSS>(jsonPlayerData);
            return playerData;
        }
        else
        {
            CreateNewPlayerData();
            return null;
        }
    }

    private void CreateNewPlayerData()
    {
        PlayerDataSS playerData = new PlayerDataSS();
        playerData.isPlayer = true;
        playerData.unitName = "User";
        playerData.unitLevel = 1;
        playerData.maxHp = 50;
        playerData.currentHp = 50;
        playerData.strength = 5;
        playerData.agility = 5;
        playerData.intelligence = 5;
        playerData.defense = 5;
        playerData.charisma = 5;
        playerData.gold = 0;
        playerData.maxExp = _baseLevelNeededExp;
        playerData.currentExp = 0;
        playerData.diamonds = 50;
        playerData.freeSkillPoints = 0;
        playerData.freeAtributesPoints = 0;
        playerData.unlockedSkillInfoList = new List<SkillInfoSS>();
        playerData.itemsInInventory = new List<ItemDataSS>();
        playerData.itemsInEQ = new List<ItemDataSS>();
        playerData.inventoryStats = new InventoryBonusStats();
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
    }

    private void FromDataToGameObejct(PlayerDataSS dataSS)
    {
        if (dataSS != null)
        {
            isPlayer = dataSS.isPlayer;
            unitName = dataSS.unitName;
            unitLevel = dataSS.unitLevel;
            maxHp = dataSS.maxHp;
            currentHp = dataSS.currentHp;
            strength = dataSS.strength;
            agility = dataSS.agility;
            intelligence = dataSS.intelligence;
            defense = dataSS.defense;
            charisma = dataSS.charisma;
            gold = dataSS.gold;
            maxExp = dataSS.maxExp;
            currentExp = dataSS.currentExp;
            diamonds = dataSS.diamonds;
            freeAtributesPoints = dataSS.freeAtributesPoints;
            freeSkillPoints = dataSS.freeSkillPoints;
            unlockedSkillInfoList = dataSS.unlockedSkillInfoList;
            itemsInInventory = dataSS.itemsInInventory;
            itemsInEQ = dataSS.itemsInEQ;
            inventoryStats = dataSS.inventoryStats;
        }
        else
        {
            //Debug.Log("Failed to load player data from JSON");
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

    private void CalculateStatsWithPassives()
    {
        foreach(var skill in unlockedSkillInfoList)
        {
            if(skill.skillType == SkillTypeEnum.Passive)
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

    private void SetupStatsWithPassive()
    {
        strengthWithPassives = strength;
        agilityWithPassives = agility;
        intelligenceWithPassives = intelligence;
        defenseWithPassives = defense;
        maxHpWithPassives = maxHp;

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
    }

    private void SetupDodgeChance()
    {
        dodgeChance = (int)(agilityWithPassives * 0.1f);
        if (dodgeChance > 80)
            dodgeChance = 80;
        dodgeChanceWithPassives = dodgeChance;
        dodgeChanceWithPassivesAndBuffs = dodgeChanceWithPassives;        
    }
    #endregion
}
