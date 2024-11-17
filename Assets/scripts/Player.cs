using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player : MonoBehaviour
{
    public string characterClass;
    public string race;
    public int hp;

    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;

    public int bonusStrength;
    public int bonusDexterity;
    public int bonusConstitution;
    public int bonusIntelligence;
    public int bonusWisdom;
    public int bonusCharisma;

    public void SaveToFile()
    {
        string json = JsonUtility.ToJson(this, true);
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");

        File.WriteAllText(path, json);
        Debug.Log("Player data saved to: " + path);
    }

    public void LoadFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
            Debug.Log("Player data loaded from: " + path);
        }
        else
        {
            Debug.LogError("Save file not found at: " + path);
        }
    }

    public void EnterValuesClass(string characterClass, string race, int hp)
    {
        this.characterClass = characterClass;
        this.race = race;
        this.hp = hp;
    }

    public void EnterValuesStats(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        this.strength = strength;
        this.dexterity = dexterity;
        this.constitution = constitution;
        this.intelligence = intelligence;
        this.wisdom = wisdom;
        this.charisma = charisma;
    }
}
