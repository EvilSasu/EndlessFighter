using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartInfoController : MonoBehaviour
{
    public GameObject skillInformation;
    public TextMeshProUGUI startInfo;

    void Update()
    {
        if(skillInformation != null && skillInformation.activeSelf == true)
            startInfo.gameObject.SetActive(false);
        else if(skillInformation != null && skillInformation.activeSelf == false)
            startInfo.gameObject.SetActive(true);
    }
}
