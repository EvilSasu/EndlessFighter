using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillController : MonoBehaviour
{
    public List<ActiveSkill> activeSkillList = new List<ActiveSkill>();

    public void ActivateSkillInUI(Skill skill, Unit unit) 
    {
        foreach(ActiveSkill skillPanel in activeSkillList)
        {
            if(skillPanel.skillName == skill.skillName)
            {
                skillPanel.StartSkill(skill, unit);
                skillPanel.gameObject.SetActive(true);
            }               
        }
    }
}
