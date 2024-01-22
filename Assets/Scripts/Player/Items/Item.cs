using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public ItemType type;
    public ItemRarity rarity;
    public int minDamage;
    public int maxDamage;
    public ItemDamageAndDefenseType damageType;
    public int physicalDefense;
    public int magicDefense;
    public int rangeDefense;
    public List<WeaponBonus> weaponBonuses;
    public List<Requirement> requirements;
    public int itemImageId;
}
