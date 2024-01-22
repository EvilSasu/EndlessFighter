using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI damageTypeText;
    public TextMeshProUGUI itemBonuses;
    public Image upperBorder;

    private Animator animator;

    private string startTextFormat = "<color=#FCDB00><b>";
    private string endTextFormat = "</color></b>";

    private float currentTimer = 5f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0)
        {
            animator.SetTrigger("Idle");
            currentTimer = 5f;
        }
    }

    public void SetupPanel(Item item)
    {
        nameText.text = item.itemName;
        rarityText.text = item.rarity.ToString();

        switch (item.rarity)
        {
            case ItemRarity.Normal: upperBorder.color = new Color(1f,1f,1f,0.117f); break;
            case ItemRarity.Rare: upperBorder.color = new Color(0f, 0f, 1f, 0.117f); break;
            case ItemRarity.Legendary: upperBorder.color = new Color(1f, 0.92f, 0.016f, 0.117f); break; 
            case ItemRarity.Mythical: upperBorder.color = new Color(0f, 1f, 1f, 0.117f); break;
            case ItemRarity.Godlike: upperBorder.color = new Color(1f, 0f, 0f, 0.117f); break;
        }

        if (item.maxDamage > 0)
        {
            damageText.text = new string("Damage: " + startTextFormat + item.minDamage + "-" + item.maxDamage + endTextFormat);
            damageTypeText.text = new string("Damage Type: "+ startTextFormat + item.damageType.ToString() + endTextFormat);
        }
        else
        {
            damageText.text = new string("Physical Def: "+ startTextFormat + item.physicalDefense + endTextFormat + "\n"
                + "Magic Def: " + startTextFormat + item.magicDefense + endTextFormat + "\n"
                + "Range Def: " + startTextFormat + item.rangeDefense + endTextFormat + "\n");
            damageTypeText.text = new string("");
        }

        itemBonuses.text = new string("Bonuses:");

        int count = item.weaponBonuses.Count;
        for(int i=0; i<count; i++)
        {
            if (i == count - 1)
                SetupLastBonusText(item.weaponBonuses[i]);
            else
                SetupBonusText(item.weaponBonuses[i]);
        }
    }

    private void SetupBonusText(WeaponBonus bonus)
    {
        itemBonuses.text += new string(" " + startTextFormat + bonus.attribute + " +" + bonus.bonusValue + endTextFormat + ", ");
    }

    private void SetupLastBonusText(WeaponBonus bonus)
    {
        itemBonuses.text += new string(" " + startTextFormat + bonus.attribute + " +" + bonus.bonusValue + endTextFormat);
    }
}
