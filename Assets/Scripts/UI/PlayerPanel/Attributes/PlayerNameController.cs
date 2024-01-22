using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameController : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI nameText;

    private void Start()
    {
        if(playerData.unitName.Length >= 2)
            nameText.text = new string(playerData.unitName.ToString());
        else
            nameText.text = new string("User Name");
    }
}
