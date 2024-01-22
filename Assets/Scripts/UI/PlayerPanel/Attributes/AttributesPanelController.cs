using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttributesPanelController : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI AttributesText;
    public GameObject exclamationMark;
    public GameObject StrengthButton;
    public GameObject AgilityButton;
    public GameObject IntelligenceButton;
    public GameObject DefenseButton;

    private void Start()
    {
        SetupFreeAttributesPoints();
        SetupAttributes();
    }

    private void SetupFreeAttributesPoints()
    {
        string startTextFormat = "<color=#FCDB00><b>";
        string endTextFormat = "</color></b>";
        if (playerData.freeAtributesPoints > 0)
        {
            AttributesText.text = new string("Attributes Free Points: " + startTextFormat + playerData.freeAtributesPoints.ToString() + endTextFormat);
            exclamationMark.SetActive(true);
        }
        else
        {
            AttributesText.text = new string("Attributes Free Points: <b>0</b>");
            exclamationMark.SetActive(false);
        }
    }
    private void SetupAttributes()
    {
        StrengthButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.strength.ToString();
        AgilityButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.agility.ToString();
        IntelligenceButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.intelligence.ToString();
        DefenseButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.defense.ToString();
    }

    public void AddAttributePointToStrength()
    {
        if (playerData.freeAtributesPoints > 0)
        {
            playerData.freeAtributesPoints -= 1;
            playerData.strength += 1;
            StrengthButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.strength.ToString();
            SetupFreeAttributesPoints();
        }
        else
            playerData.freeAtributesPoints = 0;

        playerData.SavePlayerDataToPlayerPrefs();
    }

    public void AddAttributePointToAgility()
    {
        if (playerData.freeAtributesPoints > 0)
        {
            playerData.freeAtributesPoints -= 1;
            playerData.agility += 1;
            AgilityButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.agility.ToString();
            SetupFreeAttributesPoints();
        }
        else
            playerData.freeAtributesPoints = 0;

        playerData.SavePlayerDataToPlayerPrefs();
    }

    public void AddAttributePointToIntelligence()
    {
        if (playerData.freeAtributesPoints > 0)
        {
            playerData.freeAtributesPoints -= 1;
            playerData.intelligence += 1;
            IntelligenceButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.intelligence.ToString();
            SetupFreeAttributesPoints();
        }
        else
            playerData.freeAtributesPoints = 0;

        playerData.SavePlayerDataToPlayerPrefs();
    }

    public void AddAttributePointToDefense()
    {
        if (playerData.freeAtributesPoints > 0)
        {
            playerData.freeAtributesPoints -= 1;
            playerData.defense += 1;
            DefenseButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerData.defense.ToString();
            SetupFreeAttributesPoints();
        }
        else
            playerData.freeAtributesPoints = 0;

        playerData.SavePlayerDataToPlayerPrefs();
    }
}
