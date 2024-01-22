using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataShowValuesController : MonoBehaviour
{
    public List<SetValueToText> setValueToTexts = new List<SetValueToText>();

    public void UpdateAllValues()
    {
        foreach (var value in setValueToTexts)
        {
            value.UpdateValues();
        }
    }
}
