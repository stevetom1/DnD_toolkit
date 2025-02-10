using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using SQLite;

public class Spells : MonoBehaviour
{
    public string spellName;
    public int spellLevel;
    public SpellCastingTime spellCastingTime;
    public bool spellConcentration;
    public SpellSchool spellSchool;
    public SpellDuration spellDuration;
    public SpellAreaEffect spellAreaEffect;
    public int spellRadius;
    public SpellTargetType spellTargetType;
    public int spellNumberOfTargets;
    public DamageType spellDamageType;
    public int spellRollAmount;
    public DamageDice spellRollDice;
    public Ability spellSavingThrow;
    public SpellSuccessfulThrow spellSuccessfulThrow;
    public List<CharacterClass> spellClasses;


    public TMP_InputField spellNameInputField;
    public TMP_InputField spellLevelInputField;
    public TMP_Dropdown spellCastingTimeDropdown;
    public Toggle spellConcentrationToggle;
    public TMP_Dropdown spellSchoolDropdown;
    public TMP_Dropdown spellDurationDropdown;
    public TMP_Dropdown spellAreaEffectDropdown;
    public TMP_InputField spellRadiusInputField;
    public TMP_Dropdown spellTargetTypeDropdown;
    public TMP_InputField spellNumberOfTargetsInputField;
    public TMP_Dropdown spellDamageTypeDropdown;
    public TMP_InputField spellRollAmountInputField;
    public TMP_Dropdown spellRollDiceDropdown;
    public TMP_Dropdown spellSavingThrowDropdown;
    public TMP_Dropdown spellSuccessfulThrowDropdown;
    public DropdownClass spellClassesText;

    public void SetSpell()
    {
        Spells spells = new Spells();
        spells.spellName = spellNameInputField.text;
        spells.spellLevel = int.Parse(spellLevelInputField.text);
        spells.spellCastingTime = (SpellCastingTime)spellCastingTimeDropdown.value;
        spells.spellConcentration = spellConcentrationToggle.isOn;
        spells.spellSchool= (SpellSchool)spellSchoolDropdown.value;
        spells.spellDuration = (SpellDuration)spellDurationDropdown.value;
        spells.spellAreaEffect = (SpellAreaEffect)spellAreaEffectDropdown.value;
        spells.spellRadius = int.Parse(spellRadiusInputField.text);
        spells.spellTargetType = (SpellTargetType)spellTargetTypeDropdown.value;
        spells.spellNumberOfTargets = int.Parse(spellNumberOfTargetsInputField.text);
        spells.spellDamageType = (DamageType)spellDamageTypeDropdown.value;
        spells.spellRollAmount = int.Parse(spellRollAmountInputField.text);
        spells.spellRollDice = (DamageDice)spellRollDiceDropdown.value;
        spells.spellSavingThrow = (Ability)spellSavingThrowDropdown.value;
        spells.spellSuccessfulThrow = (SpellSuccessfulThrow)spellSuccessfulThrowDropdown.value;
        spells.spellClasses = spellClassesText.characterClass;
    }
}

public enum SpellCastingTime { Action1, BonusAction1, Reaction1, Minute1, Minute10, Hour1, Hour8, Hour12, Hour24 }
public enum SpellSchool { Abjuration, Conjuration, Divination, Enchantment, Evocation, Illusion, Necromancy, Transmutation}
public enum SpellDuration { Instantaneous, Round1, Minute1, Minute10, Hour1, Hour8, Hour12, Hour24 }
public enum SpellAreaEffect { Self, Touch, Line, Cone, Cylinder, Sphere, Cube }
public enum SpellTargetType { Self, All, Friendly, Enemy, Aberration, Beast, Celestial, Construct, Dragon, Elemental, Fey, Fiend, Giant, Humanoid, Monstrosity, Ooze, Plant, Undead }
public enum DamageDice { d4, d6, d8, d10, d12, d20}
public enum Ability { Strenght, Dexterity, Constitution, Inteligence, Wisdom, Charisma}
public enum SpellSuccessfulThrow { Half, Nothing }

