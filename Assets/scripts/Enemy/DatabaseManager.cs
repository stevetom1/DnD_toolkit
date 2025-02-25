using SQLite;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using System;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    private SQLiteConnection db;

    private void Start()
    {
        string dbPath = $"{Application.persistentDataPath}/MyDb.db";

        db = new SQLiteConnection(dbPath);
        Debug.Log($"Database created at: {dbPath}");
        InitializeDatabase();
    }


    private void InitializeDatabase()
    {
        try
        {
            db.CreateTable<DbEnemy>();
            db.CreateTable<DbEnemyAction>();
            db.CreateTable<DbSpell>();
            Debug.Log("Database tables created successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to initialize database: {ex.Message}");
        }
    }

    public void SaveEnemy(Enemy enemy)
    {
        DbEnemy dbEnemy = new DbEnemy();
        dbEnemy.EnemyName = enemy.EnemyName;
        dbEnemy.MaxHp = enemy.MaxHp;
        dbEnemy.Defense = enemy.Defense;
        dbEnemy.Speed = enemy.Speed;
        dbEnemy.EnemySize = EnumToString(enemy.EnemySize);
        dbEnemy.EnemyType = EnumToString(enemy.EnemyType);
        dbEnemy.Experience = enemy.Experience;
        dbEnemy.NumberOfAttacks = enemy.NumberOfAttacks;
        dbEnemy.Strength = enemy.Strength;
        dbEnemy.Dexterity = enemy.Dexterity;
        dbEnemy.Constitution = enemy.Constitution;
        dbEnemy.Intelligence = enemy.Intelligence;
        dbEnemy.Wisdom = enemy.Wisdom;
        dbEnemy.Charisma = enemy.Charisma;

        dbEnemy.BonusStrength = enemy.BonusStrength;
        dbEnemy.BonusDexterity = enemy.BonusDexterity;
        dbEnemy.BonusConstitution = enemy.BonusConstitution;
        dbEnemy.BonusIntelligence = enemy.BonusIntelligence;
        dbEnemy.BonusWisdom = enemy.BonusWisdom;
        dbEnemy.BonusCharisma = enemy.BonusCharisma;

        dbEnemy.STBonusStrength = enemy.STBonusStrength;
        dbEnemy.STBonusDexterity = enemy.STBonusDexterity;
        dbEnemy.STBonusConstitution = enemy.STBonusConstitution;
        dbEnemy.STBonusIntelligence = enemy.STBonusIntelligence;
        dbEnemy.STBonusWisdom = enemy.STBonusWisdom;
        dbEnemy.STBonusCharisma = enemy.STBonusCharisma;

        dbEnemy.Weakneses = ListToString(enemy.Weakneses);
        dbEnemy.Vulnerability = ListToString(enemy.Vulnerability);
        dbEnemy.Immunity = ListToString(enemy.Immunity);
        dbEnemy.ImmunityAgaintsStatus = ListToString(enemy.ImmunityAgaintsStatus);
        //dbEnemy.EnemyAction = ActionListToString(enemy.EnemyAction);


        if (db == null)
        {
            Debug.LogError("Database connection is not initialized.");
            return;
        }
        // try
        {
            db.Insert(dbEnemy);
            Debug.Log($"Enemy new ID: {dbEnemy.Id}");
            foreach (var action in enemy.EnemyAction)
            {
                DbEnemyAction dbEnemyAction = new DbEnemyAction();
                dbEnemyAction.EnemyId = dbEnemy.Id;
                dbEnemyAction.ActionName = action.ActionName;
                dbEnemyAction.Reach = action.Reach;
                dbEnemyAction.Bonus = action.Bonus;
                dbEnemyAction.DamageType = EnumToString(action.DamageType);
                dbEnemyAction.DamageCount = action.DamageCount;
                dbEnemyAction.DamageDice = action.DamageDice;
                dbEnemyAction.BonusDamage = action.BonusDamage;
                //dbEnemyAction.EnemyActionEffect
                db.Insert(dbEnemyAction);
            }
            Debug.Log($"Enemy '{dbEnemy.EnemyName}' saved successfully.");
        }
        /*catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save enemy: {ex.Message}");
        }*/
    }

    public List<Enemy> GetAllMonsters()
    {
        if (db == null)
        {
            Debug.LogError("Database connection is not initialized.");
            return null;
        }
        try
        {
            var monsters = db.Table<Enemy>().ToList();
            foreach (var monster in monsters)
            {
                monster.EnemyAction = db.Table<EnemyAction>()
                    .Where(a => a.EnemyId == monster.Id).ToList();
            }
            return monsters;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to fetch monsters: {ex.Message}");
            return null;
        }
    }


    public void SaveSpell(Spells spells)
    {
        DbSpell dbSpell = new DbSpell();

        dbSpell.spellName = spells.spellName;
        dbSpell.spellLevel = spells.spellLevel;
        dbSpell.spellCastingTime = EnumToString(spells.spellCastingTime);
        dbSpell.spellConcentration = spells.spellConcentration;
        dbSpell.spellSchool = EnumToString(spells.spellSchool);
        dbSpell.spellDuration = EnumToString(spells.spellDuration);
        dbSpell.spellAreaEffect = EnumToString(spells.spellAreaEffect);
        dbSpell.spellRadius = spells.spellRadius;
        dbSpell.spellTargetType = EnumToString(spells.spellTargetType);
        dbSpell.spellNumberOfTargets = spells.spellNumberOfTargets;
        dbSpell.spellDamageType = EnumToString(spells.spellDamageType);
        dbSpell.spellRollAmount = spells.spellRollAmount;
        dbSpell.spellRollDice = EnumToString(spells.spellRollDice);
        dbSpell.spellSavingThrow = EnumToString(spells.spellSavingThrow);
        dbSpell.spellSuccessfulThrow = EnumToString(spells.spellSuccessfulThrow);
        dbSpell.spellClasses = ListToString(spells.spellClasses);
        db.Insert(dbSpell);
    }

    public List<Spells> GetAllSpells()
    {
        if (db == null)
        {
            Debug.LogError("Database connection is not initialized.");
            return null;
        }
        try
        {
            var spells = db.Table<Spells>().ToList();
            return spells;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to fetch monsters: {ex.Message}");
            return null;
        }
    }


    private string EnumToString<T>(T enumValue) where T : Enum
    {
        return enumValue.ToString();
    }

    private T StringToEnum<T>(string enumString) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumString);
    }

    private string ListToString<T>(List<T> list) where T : Enum
    {
        string result = "";
        for (int i = 0; i < list.Count; i++)
        {
            result = result + list[i].ToString() + ",";
        }
        return result;
    }

    private List<T> StringToList<T>(string stringList) where T : Enum
    {
        List<string> list = new List<string>();
        list = stringList.Split(',').ToList();
        List<T> result = new List<T>();
        foreach(string str in list)
        {
            result.Add(StringToEnum<T>(str));
        }
        return result;
    }



    public class DbEnemy
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        public string EnemyName { get; set; }
        public int MaxHp { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public string EnemySize { get; set; }
        public string EnemyType { get; set; }
        public int Experience { get; set; }
        public int NumberOfAttacks { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public int BonusStrength { get; set; }
        public int BonusDexterity { get; set; }
        public int BonusConstitution { get; set; }
        public int BonusIntelligence { get; set; }
        public int BonusWisdom { get; set; }
        public int BonusCharisma { get; set; }

        public int STBonusStrength { get; set; }
        public int STBonusDexterity { get; set; }
        public int STBonusConstitution { get; set; }
        public int STBonusIntelligence { get; set; }
        public int STBonusWisdom { get; set; }
        public int STBonusCharisma { get; set; }

        public string Weakneses { get; set; }
        public string Vulnerability { get; set; }
        public string Immunity { get; set; }
        public string ImmunityAgaintsStatus { get; set; }
        public string EnemyAction { get; set; }
    }

    public class DbEnemyAction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int EnemyId { get; set; }

        public string ActionName { get; set; }
        public int Reach { get; set; }
        public int Bonus { get; set; }
        public string DamageType { get; set; }
        public int DamageCount { get; set; }
        public string DamageDice { get; set; }
        public int BonusDamage { get; set; }
        // public string EnemyActionEffect { get; set; }
    }

    public class DbSpell
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string spellName { get; set; }
        public int spellLevel { get; set; }
        public string spellCastingTime { get; set; }
        public bool spellConcentration { get; set; }
        public string spellSchool { get; set; }
        public string spellDuration { get; set; }
        public string spellAreaEffect { get; set; }
        public int spellRadius { get; set; }
        public string spellTargetType { get; set; }
        public int spellNumberOfTargets { get; set; }
        public string spellDamageType { get; set; }
        public int spellRollAmount { get; set; }
        public string spellRollDice { get; set; }
        public string spellSavingThrow { get; set; }
        public string spellSuccessfulThrow { get; set; }
        public string spellClasses { get; set; }
    }
}