using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public string EnemyName { get; set; }
    public int MaxHp { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    [field: SerializeField] public Size EnemySize { get; set; }
    [field: SerializeField] public Type EnemyType { get; set; }
    public int Experience { get; set; }
    public int NumberOfAttacks { get; set; }

    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    [field: SerializeField] public int Charisma { get; set; }

    public int BonusStrength { get; set; }
    public int BonusDexterity { get; set; }
    public int BonusConstitution { get; set; }
    public int BonusIntelligence { get; set; }
    public int BonusWisdom { get; set; }
    [field: SerializeField] public int BonusCharisma { get; set; }

    public int STBonusStrength { get; set; }
    public int STBonusDexterity { get; set; }
    public int STBonusConstitution { get; set; }
    public int STBonusIntelligence { get; set; }
    public int STBonusWisdom { get; set; }
    [field:SerializeField]public int STBonusCharisma { get; set; }

    [field: SerializeField] public List<DamageType> Weakneses { get; set; }
    [field: SerializeField] public List<DamageType> Vulnerability { get; set; }
    [field: SerializeField] public List<DamageType> Immunity { get; set; }
    [field: SerializeField] public List<StatusType> ImmunityAgaintsStatus { get; set; }

    public TMP_InputField enemyNameText;
    public TMP_InputField hpText;
    public TMP_InputField defenseText;
    public TMP_InputField speedText;
    public TMP_Dropdown enemySizeText;
    public TMP_Dropdown enemyTypeText;
    public TMP_InputField experienceText;
    public TMP_InputField numberOfAttacksText;

    public TMP_InputField strengthText;
    public TMP_InputField intelligenceText;
    public TMP_InputField dexterityText;
    public TMP_InputField wisdomText;
    public TMP_InputField constitutionText;
    public TMP_InputField charismaText;

    public TMP_InputField STBonusStrengthText;
    public TMP_InputField STBonusIntelligenceText;
    public TMP_InputField STBonusDexterityText;
    public TMP_InputField STBonusWisdomText;
    public TMP_InputField STBonusConstitutionText;
    public TMP_InputField STBonusCharismaText;

    public DropdownManager weaknesesDropdown;
    public DropdownManager vulnerabilityDropdown;
    public DropdownManager immunityDropdown;
    public DropdownStatus immunityAgaintsStatusDropdown;

    public void Start()
    {
        weaknesesDropdown = GameObject.Find("Weakneses").GetComponent<DropdownManager>();
        vulnerabilityDropdown = GameObject.Find("Vulnerability").GetComponent<DropdownManager>();
        immunityDropdown = GameObject.Find("Immunity").GetComponent<DropdownManager>();
        immunityAgaintsStatusDropdown = GameObject.Find("ImmunityAgaintsStatus").GetComponent<DropdownStatus>();

    }

    public void SetEnemyStatsButton()
    {
        EnemyName = enemyNameText.text;
        MaxHp = int.Parse(hpText.text);
        Defense = int.Parse(defenseText.text);
        Speed =  int.Parse(speedText.text);
        EnemySize = (Size)enemySizeText.value;
        EnemyType = (Type)enemyTypeText.value;
        Experience = int.Parse(experienceText.text);
        NumberOfAttacks = int.Parse(numberOfAttacksText.text);

        Strength = int.Parse(strengthText.text);
        Dexterity = int.Parse(dexterityText.text);
        Constitution = int.Parse(constitutionText.text);
        Intelligence = int.Parse(intelligenceText.text);
        Wisdom = int.Parse(wisdomText.text);
        Charisma = int.Parse(charismaText.text);

        BonusStrength = Mathf.FloorToInt((Strength - 10) / 2f);
        BonusDexterity = Mathf.FloorToInt((Dexterity - 10) / 2f);
        BonusConstitution = Mathf.FloorToInt((Constitution - 10) / 2f);
        BonusIntelligence = Mathf.FloorToInt((Intelligence - 10) / 2f);
        BonusWisdom = Mathf.FloorToInt((Wisdom - 10) / 2f);
        BonusCharisma = Mathf.FloorToInt((Charisma - 10) / 2f);

        STBonusStrength = int.Parse(STBonusStrengthText.text);
        STBonusDexterity = int.Parse(STBonusDexterityText.text);
        STBonusConstitution = int.Parse(STBonusConstitutionText.text);
        STBonusIntelligence = int.Parse(STBonusIntelligenceText.text);
        STBonusWisdom = int.Parse(STBonusWisdomText.text);
        STBonusCharisma = int.Parse(STBonusCharismaText.text);
}

    public void SetVulnerabilityButton()
    {
        Weakneses = weaknesesDropdown.damageType;
        Vulnerability = vulnerabilityDropdown.damageType;
        Immunity = immunityDropdown.damageType;
        ImmunityAgaintsStatus = immunityAgaintsStatusDropdown.statusType;
    }

}

public enum Size { Fine, Diminutive, Tiny, Small, Medium, Large, Huge, Gargantuan, Colossal }
public enum Type { Aberration, Beast, Celestial, Construct, Dragon, Elemental, Fey, Fiend, Giant, Humanoid, Monstrosity, Ooze, Plant, Undead }
