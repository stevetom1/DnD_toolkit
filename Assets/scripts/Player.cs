using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public string name;

    public string characterClass;
    public string race;
    public int hp;
    public int remainingHp;
    public int defense;
    public int iniciative;
    public int speed;

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

    public int acrobatics;
    public int animalHandling;
    public int arcana;
    public int athletics;
    public int deception;
    public int history;
    public int insight;
    public int intimidation;
    public int investigation;
    public int medicine;
    public int nature;
    public int perception;
    public int performance;
    public int persuasion;
    public int religion;
    public int sleightOfHand;
    public int stealth;
    public int survival;

    public int proficiencyBonus = 2;
    public List<string> proficienciesSkillsList;

    public List<SOItems> inventory;
    public InventoryManager inventoryManager;

    public void SaveToFile()
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Player name is empty! Cannot save data.");
            return;
        }
        string fileName = name.Replace(" ", "_") + ".json";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, json);
        Debug.Log($"Player data saved to: {path}");
    }

    public void LoadFromFile(string playerName)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Player name is empty! Cannot load data.");
            return;
        }

        string fileName = playerName.Replace(" ", "_") + ".json";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
            Debug.Log($"Player data loaded from: {path}");
        }
        else
        {
            Debug.LogError($"Save file for '{playerName}' not found at: {path}");
        }
    }

    public void EnterValuesClass(string name,string characterClass, string race, int hp)
    {
        this.name = name;
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

    public void SetSkills()
    {
        acrobatics = bonusDexterity + (proficienciesSkillsList.Contains("Acrobatics") ? proficiencyBonus : 0);
        animalHandling = bonusWisdom + (proficienciesSkillsList.Contains("AnimalHandling") ? proficiencyBonus : 0);
        arcana = bonusIntelligence + (proficienciesSkillsList.Contains("Arcana") ? proficiencyBonus : 0);
        athletics = bonusStrength + (proficienciesSkillsList.Contains("Athletics") ? proficiencyBonus : 0);
        deception = bonusCharisma + (proficienciesSkillsList.Contains("Deception") ? proficiencyBonus : 0);
        history = bonusIntelligence + (proficienciesSkillsList.Contains("History") ? proficiencyBonus : 0);
        insight = bonusWisdom + (proficienciesSkillsList.Contains("Insight") ? proficiencyBonus : 0);
        intimidation = bonusCharisma + (proficienciesSkillsList.Contains("Intimidation") ? proficiencyBonus : 0);
        investigation = bonusIntelligence + (proficienciesSkillsList.Contains("Investigation") ? proficiencyBonus : 0);
        medicine = bonusWisdom + (proficienciesSkillsList.Contains("Medicine") ? proficiencyBonus : 0);
        nature = bonusIntelligence + (proficienciesSkillsList.Contains("Nature") ? proficiencyBonus : 0);
        perception = bonusWisdom + (proficienciesSkillsList.Contains("Perception") ? proficiencyBonus : 0);
        performance = bonusCharisma + (proficienciesSkillsList.Contains("Performance") ? proficiencyBonus : 0);
        persuasion = bonusCharisma + (proficienciesSkillsList.Contains("Persuation") ? proficiencyBonus : 0);
        religion = bonusIntelligence + (proficienciesSkillsList.Contains("Religion") ? proficiencyBonus : 0);
        sleightOfHand = bonusDexterity + (proficienciesSkillsList.Contains("SleightOfHand") ? proficiencyBonus : 0);
        stealth = bonusDexterity + (proficienciesSkillsList.Contains("Stealth") ? proficiencyBonus : 0);
        survival = bonusWisdom + (proficienciesSkillsList.Contains("Survival") ? proficiencyBonus : 0);
    }
    
    public void AddToInventory()
    {
        inventoryManager = GameObject.Find("InvenroryPanel").GetComponent<InventoryManager>();
        foreach (var item in inventory)
        {
            inventoryManager.AddItem(item);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
