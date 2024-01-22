using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBonusStats : MonoBehaviour
{
    public int bonusMinDamage;
    public int bonusMaxDamage;

    public int bonusPhysicalDefense;
    public int bonusMagicDefense;
    public int bonusRangeDefense;

    public int bonusStrenght;
    public int bonusAgility;
    public int bonusDefense;
    public int bonusIntelligence;
    private PlayerData playerData;

    public void SetPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    public void UpdateStats(InventoryStatsController inventoryStatsController)
    {
        ResetBonusStats();
        if (inventoryStatsController != null)
        {
            bonusMinDamage = inventoryStatsController.bonusMinDamage;
            bonusMaxDamage = inventoryStatsController.bonusMaxDamage;
            bonusPhysicalDefense = inventoryStatsController.bonusPhysicalDefense;
            bonusMagicDefense = inventoryStatsController.bonusMagicDefense;
            bonusStrenght = inventoryStatsController.bonusStrenght;
            bonusAgility = inventoryStatsController.bonusAgility;
            bonusDefense = inventoryStatsController.bonusDefense;
            bonusIntelligence = inventoryStatsController.bonusIntelligence;
        }
    }

    public void UpdateValuesAfterLoad(List<ItemDataSS> itemsInEQ)
    {
        RemoveBonusStats();
        ResetBonusStats();
        foreach (ItemDataSS item in itemsInEQ)
        {
            bonusMinDamage += item.minDamage;
            bonusMaxDamage += item.maxDamage;

            bonusPhysicalDefense += item.physicalDefense;
            bonusMagicDefense += item.magicDefense;
            bonusRangeDefense += item.rangeDefense;
            foreach (WeaponBonus bonus in item.weaponBonuses)
            {
                switch (bonus.attribute)
                {
                    case WeaponBonusAttribute.Strength: bonusStrenght += bonus.bonusValue; break;
                    case WeaponBonusAttribute.Agility: bonusAgility += bonus.bonusValue; break;
                    case WeaponBonusAttribute.Intelligence: bonusIntelligence += bonus.bonusValue; break;
                    case WeaponBonusAttribute.Defense: bonusDefense += bonus.bonusValue; break;
                }
            }
        }
        AddBnousStats();
    }

    private void ResetBonusStats()
    {
        bonusMinDamage = 0;
        bonusMaxDamage = 0;
        bonusStrenght = 0;
        bonusAgility = 0;
        bonusIntelligence = 0;
        bonusDefense = 0;
        bonusPhysicalDefense = 0;
        bonusMagicDefense = 0;
        bonusRangeDefense = 0;
    }

    private void AddBnousStats()
    {
        playerData.minDamage += bonusMinDamage;
        playerData.maxDamage += bonusMaxDamage;

        playerData.strengthWithPassives += bonusStrenght;
        playerData.agilityWithPassives += bonusAgility;
        playerData.intelligenceWithPassives += bonusIntelligence;
        playerData.defenseWithPassives += bonusDefense;

        playerData.physicalDefenseWithPassives += bonusPhysicalDefense;
        playerData.magicDefenseWithPassives += bonusMagicDefense;
        playerData.rangeDefenseWithPassives += bonusRangeDefense;
    }

    private void RemoveBonusStats()
    {
        playerData.minDamage -= bonusMinDamage;
        playerData.maxDamage -= bonusMaxDamage;

        playerData.strengthWithPassives -= bonusStrenght;
        playerData.agilityWithPassives -= bonusAgility;
        playerData.intelligenceWithPassives -= bonusIntelligence;
        playerData.defenseWithPassives -= bonusDefense;

        playerData.physicalDefenseWithPassives -= bonusPhysicalDefense;
        playerData.magicDefenseWithPassives -= bonusMagicDefense;
        playerData.rangeDefenseWithPassives -= bonusRangeDefense;
    }

}
