using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetSkillPointsToText : MonoBehaviour
{
    public PlayerData playerData;
    private TextMeshProUGUI skillPointsText;

    private void Awake()
    {
        skillPointsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetupFreeSkillPoints();
    }

    public void SetupFreeSkillPoints()
    {
        string startTextFormat = "<color=#FCDB00><b>";
        string endTextFormat = "</color></b>";
        if (playerData.freeSkillPoints > 0)
        {
            skillPointsText.text = new string("Free Skill Points: " + startTextFormat + playerData.freeSkillPoints.ToString() + endTextFormat);
        }
        else
        {
            skillPointsText.text = new string("Free Skill Points: <b>0</b>");
        }
    }
}
