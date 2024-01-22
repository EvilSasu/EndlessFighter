using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetValueToText : MonoBehaviour
{
    public PlayerData playerData;
    public Type type;
    public enum Type
    {
        Gold,
        Diamonds,
        Name,
        Level
    }
    private TextMeshProUGUI m_TextMeshProUGUI;
    
    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        if (type == Type.Gold)
        {
            m_TextMeshProUGUI.text = playerData.gold.ToString();
        }           
        else if (type == Type.Diamonds)
        {
            m_TextMeshProUGUI.text = playerData.diamonds.ToString();
        }
        else if(type == Type.Name)
        {
            if (playerData.unitName.ToString().Length >= 2)
                m_TextMeshProUGUI.text = playerData.unitName.ToString();
            else
                m_TextMeshProUGUI.text = new string("User Name");
        }
        else if(type == Type.Level)
        {
            m_TextMeshProUGUI.text = playerData.unitLevel.ToString();
        }           
    }

    public void UpdateValues()
    {
        if (type == Type.Gold)
        {
            m_TextMeshProUGUI.text = playerData.gold.ToString();
        }           
        else if (type == Type.Diamonds)
        {
            m_TextMeshProUGUI.text = playerData.diamonds.ToString();
        }
        else if(type == Type.Name)
        {
            if (playerData.unitName.ToString().Length >= 2)
                m_TextMeshProUGUI.text = playerData.unitName.ToString();
            else
                m_TextMeshProUGUI.text = new string("User Name");
        }
        else if(type == Type.Level)
        {
            m_TextMeshProUGUI.text = playerData.unitLevel.ToString();
        } 
    }
}
