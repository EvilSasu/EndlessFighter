using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStatsController : MonoBehaviour
{
    public PlayerData playerData;
    public List<InventorySlot> inventorySlots;
    // TODO: MIN and MAX damage need to be add to playerData and later to Unit
    public int bonusMinDamage;
    public int bonusMaxDamage;

    public int bonusPhysicalDefense;
    public int bonusMagicDefense;
    public int bonusRangeDefense;

    public int bonusStrenght;
    public int bonusAgility;
    public int bonusDefense;
    public int bonusIntelligence;

    private void Awake()
    {
        UpdateBonusStats();
    }

    public void UpdateBonusStats()
    {
        //RemoveBonusStats();
        //SumUpBonusStats();
        //AddBnousStats();
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

    private void SumUpBonusStats()
    {
        ResetBonusStats();
        Item item;
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            if(inventorySlot.transform.childCount > 0 && inventorySlot.itemEnums == ItemEnums.EQSlot)
            {
                item = inventorySlot.transform.GetChild(0).GetComponent<Item>();
                bonusMinDamage += item.minDamage;
                bonusMaxDamage += item.maxDamage;

                bonusPhysicalDefense += item.physicalDefense;
                bonusMagicDefense += item.magicDefense;
                bonusRangeDefense += item.rangeDefense;
                foreach(WeaponBonus bonus in item.weaponBonuses)
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
        }
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
}
