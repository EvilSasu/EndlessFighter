using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCircleController : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI levelText;

    private void Start()
    {
        levelText.text = new string("Level: " + playerData.unitLevel.ToString());
    }
}
