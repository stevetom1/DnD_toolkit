using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using SQLite;

public class Enemy : MonoBehaviour
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }


    [field: SerializeField] public string EnemyName { get; set; }
    [field: SerializeField] public int MaxHp { get; set; }

    [field: SerializeField] public int Health { get; set; }

    [field: SerializeField] public int Defense { get; set; }
    [field: SerializeField] public int Speed { get; set; }
    [field: SerializeField] public Size EnemySize { get; set; }
    [field: SerializeField] public Type EnemyType { get; set; }
    [field: SerializeField] public int Experience { get; set; }
    [field: SerializeField] public int NumberOfAttacks { get; set; }

    [field: SerializeField] public int Strength { get; set; }
    [field: SerializeField] public int Dexterity { get; set; }
    [field: SerializeField] public int Constitution { get; set; }
    [field: SerializeField] public int Intelligence { get; set; }
    [field: SerializeField] public int Wisdom { get; set; }
    [field: SerializeField] public int Charisma { get; set; }

    [field: SerializeField] public int BonusStrength { get; set; }
    [field: SerializeField] public int BonusDexterity { get; set; }
    [field: SerializeField] public int BonusConstitution { get; set; }
    [field: SerializeField] public int BonusIntelligence { get; set; }
    [field: SerializeField] public int BonusWisdom { get; set; }
    [field: SerializeField] public int BonusCharisma { get; set; }

    [field: SerializeField] public int STBonusStrength { get; set; }
    [field: SerializeField] public int STBonusDexterity { get; set; }
    [field: SerializeField] public int STBonusConstitution { get; set; }
    [field: SerializeField] public int STBonusIntelligence { get; set; }
    [field: SerializeField] public int STBonusWisdom { get; set; }
    [field:SerializeField]public int STBonusCharisma { get; set; }

    [field: SerializeField] public List<DamageType> Weakneses { get; set; }
    [field: SerializeField] public List<DamageType> Vulnerability { get; set; }
    [field: SerializeField] public List<DamageType> Immunity { get; set; }
    [field: SerializeField] public List<StatusType> ImmunityAgaintsStatus { get; set; }
    [field: SerializeField] public List<EnemyAction> EnemyAction { get; set; }

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

    public EnemyAction enemyActionList;

    private DatabaseManager databaseManager;

    public int hexX;
    public int hexY;
    public void Start()
    {
        Health = MaxHp;

        /*weaknesesDropdown = GameObject.Find("Weakneses").GetComponent<DropdownManager>();
        vulnerabilityDropdown = GameObject.Find("Vulnerability").GetComponent<DropdownManager>();
        immunityDropdown = GameObject.Find("Immunity").GetComponent<DropdownManager>();
        immunityAgaintsStatusDropdown = GameObject.Find("ImmunityAgaintsStatus").GetComponent<DropdownStatus>();*/
    }

    public void SetEnemyActionList()
    {
        enemyActionList = GameObject.Find("Enemy").GetComponent<EnemyAction>();
        EnemyAction = enemyActionList.enemyActionList;
    }


    public void CreateEnemyButton()
    {
        DatabaseManager databaseManager = GameObject.Find("DatabaseManager")?.GetComponent<DatabaseManager>();
        if (databaseManager == null)
        {
            Debug.LogError("DatabaseManager object not found in the scene!");
            return;
        }

        Enemy enemy = gameObject.AddComponent<Enemy>();
        try
        {
            enemy.EnemyName = enemyNameText.text;
            enemy.MaxHp = int.Parse(hpText.text);
            enemy.Defense = int.Parse(defenseText.text);
            enemy.Speed = int.Parse(speedText.text);
            enemy.EnemySize = (Size)enemySizeText.value;
            enemy.EnemyType = (Type)enemyTypeText.value;
            enemy.Experience = int.Parse(experienceText.text);
            enemy.NumberOfAttacks = int.Parse(numberOfAttacksText.text);

            enemy.Strength = int.Parse(strengthText.text);
            enemy.Dexterity = int.Parse(dexterityText.text);
            enemy.Constitution = int.Parse(constitutionText.text);
            enemy.Intelligence = int.Parse(intelligenceText.text);
            enemy.Wisdom = int.Parse(wisdomText.text);
            enemy.Charisma = int.Parse(charismaText.text);

            enemy.BonusStrength = Mathf.FloorToInt((enemy.Strength - 10) / 2f);
            enemy.BonusDexterity = Mathf.FloorToInt((enemy.Dexterity - 10) / 2f);
            enemy.BonusConstitution = Mathf.FloorToInt((enemy.Constitution - 10) / 2f);
            enemy.BonusIntelligence = Mathf.FloorToInt((enemy.Intelligence - 10) / 2f);
            enemy.BonusWisdom = Mathf.FloorToInt((enemy.Wisdom - 10) / 2f);
            enemy.BonusCharisma = Mathf.FloorToInt((enemy.Charisma - 10) / 2f);

            enemy.STBonusStrength = int.Parse(STBonusStrengthText.text);
            enemy.STBonusDexterity = int.Parse(STBonusDexterityText.text);
            enemy.STBonusConstitution = int.Parse(STBonusConstitutionText.text);
            enemy.STBonusIntelligence = int.Parse(STBonusIntelligenceText.text);
            enemy.STBonusWisdom = int.Parse(STBonusWisdomText.text);
            enemy.STBonusCharisma = int.Parse(STBonusCharismaText.text);

            enemy.Weakneses = weaknesesDropdown.damageType;
            enemy.Vulnerability = vulnerabilityDropdown.damageType;
            enemy.Immunity = immunityDropdown.damageType;
            enemy.ImmunityAgaintsStatus = immunityAgaintsStatusDropdown.statusType;

            if (enemyActionList != null && enemyActionList.enemyActionList != null)
            {
                enemy.EnemyAction = enemyActionList.enemyActionList;
                Debug.Log($"Enemy actions count: {enemy.EnemyAction.Count}");
            }
            else
            {
                Debug.LogWarning("Enemy actions list is null or empty.");
            }

            databaseManager.SaveEnemy(enemy);
            Debug.Log($"Enemy '{enemy.EnemyName}' saved successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to create enemy: {ex.Message}");
        }
    }



}