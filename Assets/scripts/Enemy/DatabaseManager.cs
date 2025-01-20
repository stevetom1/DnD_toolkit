using SQLite;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;

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
            db.CreateTable<Enemy>();
            db.CreateTable<EnemyAction>();
            Debug.Log("Database tables created successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to initialize database: {ex.Message}");
        }
    }

    public void SaveEnemy(Enemy enemy)
    {
        if (db == null)
        {
            Debug.LogError("Database connection is not initialized.");
            return;
        }
        try
        {
            db.Insert(enemy);
            /*foreach (var action in enemy.EnemyAction)
            {
                action.EnemyId = enemy.Id;
                db.Insert(action);
            }*/
            Debug.Log($"Enemy '{enemy.EnemyName}' saved successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save enemy: {ex.Message}");
        }
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
            /*foreach (var monster in monsters)
            {
                monster.EnemyAction = db.Table<EnemyAction>()
                    .Where(a => a.EnemyId == monster.Id).ToList();
            }*/
            return monsters;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to fetch monsters: {ex.Message}");
            return null;
        }
    }
}

