[System.Serializable]
public class Requirement
{
    public RequirementType type;
    public AttributeType attributeType; // checked only if RequirementType = Attribute
    public SkillName skillName;
    public int requiredLevel;
    public int baseRequiredLevel;
    public bool isUnlocked;
}