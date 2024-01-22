using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerDataSS
{
    public bool isPlayer;
    public string unitName;
    public int unitLevel;
    public float maxHp;
    public float currentHp;
    public int strength;
    public int agility;
    public int intelligence;
    public int defense;
    public int charisma;
    public int gold;
    public int maxExp;
    public int currentExp;
    public int diamonds;
    public int freeAtributesPoints;
    public int freeSkillPoints;
    public List<SkillInfoSS> unlockedSkillInfoList;
    public List<ItemDataSS> itemsInInventory;
    public List<ItemDataSS> itemsInEQ;
    public InventoryBonusStats inventoryStats;
}
