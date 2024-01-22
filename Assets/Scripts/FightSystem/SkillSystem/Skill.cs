using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillName skillName;
    public string description;
    public float cooldown;
    public int skillLevel;
    public float bonusPerLevel;
    private Image _image;
    private Image _childImage;
    private Button _button;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        if(transform.GetChild(0).GetComponent<Image>() != null)
            _childImage = transform.GetChild(0).GetComponent<Image>();
    }

    public Image GetImage()
    {
        return _image;
    }

    public Image GetChildImage()
    {
        return _childImage;
    }

    public Button GetButton()
    {
        return _button;
    }
}
