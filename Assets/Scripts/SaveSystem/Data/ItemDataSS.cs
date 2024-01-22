using System.Collections.Generic;
[System.Serializable]
public class ItemDataSS
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
    public Item itemRef;
}
