using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public Unit unit;
    public TextMeshProUGUI Healthtext;
    public TextMeshProUGUI LevelText;
    public GameObject damageText;
    public GameObject damageTextParent;

    private void Start()
    {
        unit.healthManager = this;
        int _tmpToShow = (int)unit.currentHp;
        Healthtext.text = _tmpToShow.ToString();
        LevelText.text = unit.unitLevel.ToString();
        StartCoroutine(FixShowedHp());
    }

    public void Setup()
    {
        unit.healthManager = this;
        int _tmpToShow = (int)unit.currentHp;
        Healthtext.text = _tmpToShow.ToString();
        LevelText.text = unit.unitLevel.ToString();
    }

    public void TakeDamage(float damage, string damageType, float critChance, float critDmgMultiplication)
    {
        int chanceOfDodge = (int)unit.dodgeChanceWithPassivesAndBuffs;
        int randomNumberForDodge = Random.Range(1, 101);
        int chanceToCrit = (int)critChance;
        int randomNumberForCrit = Random.Range(1, 101);
        float damageMultiplication;

        if (chanceToCrit < randomNumberForCrit)
            damageMultiplication = 1;
        else
            damageMultiplication = critDmgMultiplication;


        if (chanceOfDodge < randomNumberForDodge)
        {
            float percentOfDamage = CalculatePercentOfDamageToDeal(damage, damageType);
            float finalDamage = (int)(damage * percentOfDamage * damageMultiplication);
            if (unit.currentHp - finalDamage < 0)
                unit.currentHp = 0;
            else
                unit.currentHp -= finalDamage;

            if(damageMultiplication == 1)
                CreateDamageText(finalDamage, damageType, false, false);
            else
                CreateDamageText(finalDamage, damageType, false, true);

            unit.fightAnimationController.PlayAnimGetDmg();
        }
        else
        {
            CreateDamageText(damage, damageType, true, false);
            unit.fightAnimationController.PlayAnimDodge();
        }

        int _tmpToShow = (int)unit.currentHp;
        Healthtext.text = _tmpToShow.ToString();
        healthBar.fillAmount = unit.currentHp / unit.maxHp;
    }

    public void Heal(float amount)
    {
        if (unit.currentHp + (int)amount > unit.maxHp)
            unit.currentHp = unit.maxHp;
        else
            unit.currentHp += (int)amount;

        CreateDamageText((int)amount, "heal", false, false);

        unit.currentHp = Mathf.Clamp(unit.currentHp, 0, unit.maxHp);

        int _tmpToShow = (int)unit.currentHp;

        Healthtext.text = _tmpToShow.ToString();
        healthBar.fillAmount = unit.currentHp / unit.maxHp;
    }

    IEnumerator FixShowedHp()
    {
        yield return new WaitForSeconds(0.1f);
        int _tmpToShow = (int)unit.currentHp;
        Healthtext.text = _tmpToShow.ToString();
        LevelText.text = unit.unitLevel.ToString();
    }

    private float CalculatePercentOfDamageToDeal(float dmg, string typeOfDmg)
    {
        float percentOfDamage = 1f; // 1f = 100%
        if(typeOfDmg == "physical")
        {
            if (dmg >= unit.physicalDefenseWithPassivesAndBuffs)
                return percentOfDamage;
            else
            {
                percentOfDamage = dmg / unit.physicalDefenseWithPassivesAndBuffs;
                return percentOfDamage;
            }
        }else if(typeOfDmg == "magic")
        {
            if (dmg >= unit.magicDefenseWithPassivesAndBuffs)
                return percentOfDamage;
            else
            {
                percentOfDamage = dmg / unit.magicDefenseWithPassivesAndBuffs;
                return percentOfDamage;
            }
        }
        else if (typeOfDmg == "range")
        {
            if (dmg >= unit.rangeDefenseWithPassivesAndBuffs)
                return percentOfDamage;
            else
            {
                percentOfDamage = dmg / unit.rangeDefenseWithPassivesAndBuffs;
                return percentOfDamage;
            }
        }
        return percentOfDamage;
    }

    private void CreateDamageText(float damage, string damageType, bool hasBeenDodged, bool isACrit)
    {
        GameObject newDamageText = Instantiate(damageText, transform.position, Quaternion.Euler(0f,0f,0f), damageTextParent.transform);
        if (!hasBeenDodged)
        {
            switch (damageType)
            {
                case "physical":
                    newDamageText.GetComponent<TextMeshProUGUI>().color = Color.yellow; break;
                case "magic":
                    newDamageText.GetComponent<TextMeshProUGUI>().color = Color.blue; break;
                case "range":
                    newDamageText.GetComponent<TextMeshProUGUI>().color = Color.gray; break;
                case "heal":
                    newDamageText.GetComponent<TextMeshProUGUI>().color = Color.green; break;
                default: break;
            }
            newDamageText.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        }
        else
        {
            newDamageText.GetComponent<TextMeshProUGUI>().color = Color.white;
            newDamageText.GetComponent<TextMeshProUGUI>().text = "MISS!";
        }
        if(isACrit)
        {
            newDamageText.GetComponent<TextMeshProUGUI>().color = Color.red;
            newDamageText.GetComponent<TextMeshProUGUI>().text = "Critical! " + damage.ToString();
        }
        newDamageText.SetActive(true);
    }
}
