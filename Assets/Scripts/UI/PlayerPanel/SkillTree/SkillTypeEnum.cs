
public enum SkillTypeEnum
{
    Active,
    Passive
}

public enum RequirementType
{
    SkillLevel,
    Attribute
}

public enum AttributeType
{
    None,
    Strength,
    Agility,
    Intelligence,
    Defense,
    PlayerLevel
}

public enum SkillName
{
    None,
    // Active physical
    DoubleAttack,
    DefendYourself,
    Berserk,

    // Passive physical
    PhysicalDmgUp,
    PhysicalDefenseUp,
    RangeDefense,
    HpUp,
    SpartanDefense,
    WeaponMaster,
    StrengthTrainingUp,
    DefenseTrainingUp,

    // Active magic
    Heal,
    Fireball,
    LightingStrike,

    // Passive magic
    MagicDmgUp,
    SkillCooldownDown,
    MagicDefenseUp,
    IntelligenceUp,
    IntelligenceTrainingUp,
    ExpUp,
    PhysicalDmgUpFromIntelligence,
    BuildingSpeedBonus,

    // Active agility
    BowShot,
    Dodger,
    CritFighter,

    // Passive agility
    RangeDmgUp,
    DodgeBonus,
    AgilityBonus,
    AgilityTrainingUp,
    CritDmgUp,
    CritChanceUp,
    GoldDropUp,
    EnergyRegenerationUp
}
