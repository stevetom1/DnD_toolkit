using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player : MonoBehaviour
{
    public string name;
    public string characterClass;
    public string race;
    public int hp;

    public int level;
    public int xp;

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

    public int acrobaticst;
    public int animalHandling;
    public int arcana;
    public int athletics;
    public int deception;
    public int history;
    public int insight;
    public int intimidation;
    public int investigation;
    public int intmedicine;
    public int nature;
    public int perception;
    public int performance;
    public int persuasion;
    public int religion;
    public int sleightOfHand;
    public int stealth;
    public int survival;

    public void SaveToFile()
    {
        acrobaticst = bonusDexterity;
        animalHandling = bonusWisdom;
        arcana = bonusIntelligence;
        athletics = bonusStrength;
        deception = bonusCharisma;
        history = bonusIntelligence;
        insight = bonusWisdom;
        intimidation = bonusCharisma;
        investigation = bonusIntelligence;
        intmedicine = bonusWisdom;
        nature = bonusIntelligence;
        perception = bonusWisdom;
        performance = bonusCharisma;
        persuasion = bonusCharisma;
        religion = bonusIntelligence;
        sleightOfHand = bonusDexterity;
        stealth = bonusDexterity;
        survival = bonusWisdom;


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
