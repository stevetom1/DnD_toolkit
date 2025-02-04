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
        dbEnemy.MaxHp= enemy.MaxHp;
        dbEnemy.Defense = enemy.Defense;
        dbEnemy.Speed = enemy.Speed;
        dbEnemy.EnemySize = SizeEnumToString(enemy.EnemySize);
        dbEnemy.EnemyType = TypeEnumToString(enemy.EnemyType);
        dbEnemy.Experience = enemy.Experience;
        dbEnemy.NumberOfAttacks = enemy.NumberOfAttacks;
        dbEnemy.Strength= enemy.Strength;
        dbEnemy.Dexterity= enemy.Dexterity;
        dbEnemy.Constitution= enemy.Constitution;
        dbEnemy.Intelligence= enemy.Intelligence;
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
        dbEnemy.ImmunityAgaintsStatus = StatusListToString(enemy.ImmunityAgaintsStatus);
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
                dbEnemyAction.DamageType = DamageTypeEnumToString(action.DamageType);
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

    private string SizeEnumToString(Size enemySize)
    {
        return enemySize.ToString();
    }
    private string TypeEnumToString(Type enemyType)
    {
        return enemyType.ToString();
    }
    private string DamageTypeEnumToString(DamageType enemyType)
    {
        return enemyType.ToString();
    }

    private string ListToString(List<DamageType> list)
    {
        string result = "";
        for (int i = 0; i < list.Count; i++)
        {
            result = result + list[i].ToString() + ",";
        }
        return result;
    }

    private string StatusListToString(List<StatusType> list)
    {
        string result = "";
        for (int i = 0; i < list.Count; i++)
        {
            result = result + list[i].ToString() + ",";
        }
        return result;
    }
    private string ActionListToString(List<EnemyAction> list)
    {
        string result = "";
        for (int i = 0; i < list.Count; i++)
        {
            result = result + list[i].ToString() + ",";
        }
        return result;
    }
    private string EffectListToString(List<EnemyActionEffect> list)
    {
        string result = "";
        for (int i = 0; i < list.Count; i++)
        {
            result = result + list[i].ToString() + ",";
        }
        return result;
    }
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