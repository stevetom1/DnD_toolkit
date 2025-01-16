using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
{
    public TextMeshProUGUI valuesForStatsText;
    public Button rollStatsButton;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI intelligenceText;
    public TextMeshProUGUI dexterityText;
    public TextMeshProUGUI wisdomText;
    public TextMeshProUGUI constitutionText;
    public TextMeshProUGUI charismaText;

    public TextMeshProUGUI bonusStrengthText;
    public TextMeshProUGUI bonusIntelligenceText;
    public TextMeshProUGUI bonusDexterityText;
    public TextMeshProUGUI bonusWisdomText;
    public TextMeshProUGUI bonusConstitutionText;
    public TextMeshProUGUI bonusCharismaText;

    public Button strengthLeftArrow;
    public Button strengthRightArrow;
    public Button intelligenceLeftArrow;
    public Button intelligenceRightArrow;
    public Button dexterityLeftArrow;
    public Button dexterityRightArrow;
    public Button wisdomLeftArrow;
    public Button wisdomRightArrow;
    public Button constitutionLeftArrow;
    public Button constitutionRightArrow;
    public Button charismaLeftArrow;
    public Button charismaRightArrow;

    public TextMeshProUGUI characterClassText;
    public Button classLeftArrow;
    public Button classRightArrow;

    public TextMeshProUGUI raceText;
    public Button raceLeftArrow;
    public Button raceRightArrow;

    public Button resetButton;
    public Button nextButton;

    private int[] stats = new int[6];
    private int[] bonusStats = new int[6];
    private List<int> availableStats;
    private int[] assignedStats = new int[6];

    private int hp, strength, dexterity, constitution, intelligence, wisdom, charisma;
    private string characterClass, race;

    public TextMeshProUGUI showName;
    public TMP_InputField nameInput;


    public TextMeshProUGUI showClass;
    public TextMeshProUGUI showRace;

    public TextMeshProUGUI showHpText;
    public TextMeshProUGUI showStrengthText;
    public TextMeshProUGUI showIntelligenceText;
    public TextMeshProUGUI showDexterityText;
    public TextMeshProUGUI showWisdomText;
    public TextMeshProUGUI showConstitutionText;
    public TextMeshProUGUI showCharismaText;

    public TextMeshProUGUI showBonusStrengthText;
    public TextMeshProUGUI showBonusIntelligenceText;
    public TextMeshProUGUI showBonusDexterityText;
    public TextMeshProUGUI showBonusWisdomText;
    public TextMeshProUGUI showBonusConstitutionText;
    public TextMeshProUGUI showBonusCharismaText;

    public TextMeshProUGUI acrobatics;
    public TextMeshProUGUI animalHandling;
    public TextMeshProUGUI arcana;
    public TextMeshProUGUI athletics;
    public TextMeshProUGUI deception;
    public TextMeshProUGUI history;
    public TextMeshProUGUI insight;
    public TextMeshProUGUI intimidation;
    public TextMeshProUGUI investigation;
    public TextMeshProUGUI medicine;
    public TextMeshProUGUI nature;
    public TextMeshProUGUI perception;
    public TextMeshProUGUI performance;
    public TextMeshProUGUI persuasion;
    public TextMeshProUGUI religion;
    public TextMeshProUGUI sleightOfHand;
    public TextMeshProUGUI stealth;
    public TextMeshProUGUI survival;

    private Player player;


    private string[] characterClasses = new string[]
    {
        "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard"
    };

    private int[] classHPValues = new int[]
    {
        12, 8, 8, 8, 10, 8, 10, 10, 8, 6, 8, 6
    };

    private string[] races = new string[]
    {
        "Human", "Wood elf", "Drow", "High elf", "Stout Halfling", "Lightfoot Halfling", "Mountain Dwarf", "Hill Dwarf", "Tiefling"
    };

    private int currentClassIndex = 0;
    private int currentRaceIndex = 0;

    void Start()
    {
        rollStatsButton.onClick.AddListener(RollStats);

        strengthLeftArrow.onClick.AddListener(() => CycleStat(ref strengthText, 0, -1));
        strengthRightArrow.onClick.AddListener(() => CycleStat(ref strengthText, 0, 1));

        intelligenceLeftArrow.onClick.AddListener(() => CycleStat(ref intelligenceText, 1, -1));
        intelligenceRightArrow.onClick.AddListener(() => CycleStat(ref intelligenceText, 1, 1));

        dexterityLeftArrow.onClick.AddListener(() => CycleStat(ref dexterityText, 2, -1));
        dexterityRightArrow.onClick.AddListener(() => CycleStat(ref dexterityText, 2, 1));

        wisdomLeftArrow.onClick.AddListener(() => CycleStat(ref wisdomText, 3, -1));
        wisdomRightArrow.onClick.AddListener(() => CycleStat(ref wisdomText, 3, 1));

        constitutionLeftArrow.onClick.AddListener(() => CycleStat(ref constitutionText, 4, -1));
        constitutionRightArrow.onClick.AddListener(() => CycleStat(ref constitutionText, 4, 1));

        charismaLeftArrow.onClick.AddListener(() => CycleStat(ref charismaText, 5, -1));
        charismaRightArrow.onClick.AddListener(() => CycleStat(ref charismaText, 5, 1));

        classLeftArrow.onClick.AddListener(() => CycleClass(-1));
        classRightArrow.onClick.AddListener(() => CycleClass(1));

        raceLeftArrow.onClick.AddListener(() => CycleRace(-1));
        raceRightArrow.onClick.AddListener(() => CycleRace(1));

        ResetAssignedStats();

        UpdateHP();

        player = FindObjectOfType<Player>();
        resetButton.onClick.AddListener(ResetStats);
        UpdateNextButtonInteractable();
    }

    private void RollStats()
    {
        availableStats = new List<int>();

        for (int i = 0; i < stats.Length; i++)
        {
            int[] rolls = new int[4];
            for (int j = 0; j < 4; j++)
            {
                rolls[j] = Random.Range(1, 7);
            }

            System.Array.Sort(rolls);
            int statValue = rolls[1] + rolls[2] + rolls[3];

            stats[i] = statValue;
            availableStats.Add(stats[i]);
        }

        availableStats.Sort();
        availableStats.Reverse();
        DisplayAvailableStats();

        ResetAssignedStats();

        strengthText.text = "0";
        intelligenceText.text = "0";
        dexterityText.text = "0";
        wisdomText.text = "0";
        constitutionText.text = "0";
        charismaText.text = "0";
    }

    private void ResetAssignedStats()
    {
        for (int i = 0; i < assignedStats.Length; i++)
        {
            assignedStats[i] = -1;
        }
    }

    private void CycleStat(ref TextMeshProUGUI statText, int statIndex, int direction)
    {
        if (direction == 1)
        {
            if (assignedStats[statIndex] != -1)
            {
                availableStats.Add(assignedStats[statIndex]);
            }
            assignedStats[statIndex] = availableStats[0];
            availableStats.RemoveAt(0);
        }

        if (direction == -1)
        {
            if (assignedStats[statIndex] != -1)
            {
                availableStats.Insert(0, assignedStats[statIndex]);
            }
            assignedStats[statIndex] = availableStats[availableStats.Count - 1];
            availableStats.RemoveAt(availableStats.Count - 1);
        }

        DisplayAvailableStats();



        int baseValue = assignedStats[statIndex];


        if (race == "Human")
        {
            baseValue += 1;
        }
        else if (race == "Wood elf" || race == "Drow" || race == "High elf")
        {
            if (statIndex == 2) baseValue += 2;

            if (race == "High elf" && statIndex == 1) baseValue += 1;
            else if (race == "Wood elf" && statIndex == 3) baseValue += 1;
            else if (race == "Drow" && statIndex == 5) baseValue += 1;
        }
        else if (race == "Stout Halfling" || race == "Lightfoot Halfling")
        {
            if (statIndex == 2) baseValue += 2;

            if (race == "Stout Halfling" && statIndex == 4) baseValue += 1;
            else if (race == "Lightfoot Halfling" && statIndex == 5) baseValue += 1;
        }
        else if (race == "Mountain Dwarf" || race == "Hill Dwarf")
        {
            if (statIndex == 4) baseValue += 2;

            if (race == "Mountain Dwarf" && statIndex == 0) baseValue += 2;
            else if (race == "Hill Dwarf" && statIndex == 3) baseValue += 1;
        }
        else if (race == "Tiefling")
        {
            if (statIndex == 5) baseValue += 2;
            else if (statIndex == 1) baseValue += 1;
        }

        statText.text = baseValue.ToString();

        int bonusValue = Mathf.FloorToInt((baseValue - 10) / 2f);

        switch (statIndex)
        {
            case 0:
                bonusStrengthText.text = bonusValue.ToString();
                player.bonusStrength = bonusValue;
                break;
            case 1:
                bonusIntelligenceText.text = bonusValue.ToString();
                player.bonusIntelligence = bonusValue;
                break;
            case 2:
                bonusDexterityText.text = bonusValue.ToString();
                player.bonusDexterity = bonusValue;
                break;
            case 3:
                bonusWisdomText.text = bonusValue.ToString();
                player.bonusWisdom = bonusValue;
                break;
            case 4:
                bonusConstitutionText.text = bonusValue.ToString();
                player.bonusConstitution = bonusValue;
                break;
            case 5:
                bonusCharismaText.text = bonusValue.ToString();
                player.bonusCharisma = bonusValue;
                break;
        }
    }






    private void CycleClass(int direction)
    {
        currentClassIndex = (currentClassIndex + direction + characterClasses.Length) % characterClasses.Length;
        characterClassText.text = characterClasses[currentClassIndex];

        UpdateHP();
    }

    private void CycleRace(int direction)
    {
        currentRaceIndex = (currentRaceIndex + direction + races.Length) % races.Length;
        raceText.text = races[currentRaceIndex];
    }

    private void UpdateHP()
    {
        int selectedClassHP = classHPValues[currentClassIndex];
        hpText.text = selectedClassHP.ToString();
    }

    public void EnterValuesClass()
    {
        name = nameInput.text;
        characterClass = characterClassText.text;
        race = raceText.text;
        hp = int.Parse(hpText.text);


        player.EnterValuesClass(name, characterClass, race, hp);
    }

    public void EnterValuesStats()
    {
        strength = int.Parse(strengthText.text);
        dexterity = int.Parse(dexterityText.text);
        constitution = int.Parse(constitutionText.text);
        intelligence = int.Parse(intelligenceText.text);
        wisdom = int.Parse(wisdomText.text);
        charisma = int.Parse(charismaText.text);

        player.hp += player.bonusConstitution;


        player.EnterValuesStats(strength, dexterity, constitution, intelligence, wisdom, charisma);
    }

    public void ShowStats()
    {
        showName.text = player.name; 
        showClass.text = player.characterClass;
        showRace.text = player.race;

        showHpText.text = player.hp.ToString();
        showStrengthText.text = player.strength.ToString();
        showIntelligenceText.text = player.intelligence.ToString();
        showDexterityText.text = player.dexterity.ToString();
        showWisdomText.text = player.wisdom.ToString();
        showConstitutionText.text = player.constitution.ToString();
        showCharismaText.text = player.charisma.ToString();

        showBonusStrengthText.text = player.bonusStrength.ToString();
        showBonusIntelligenceText.text = player.bonusIntelligence.ToString();
        showBonusDexterityText.text = player.bonusDexterity.ToString();
        showBonusWisdomText.text = player.bonusWisdom.ToString();
        showBonusConstitutionText.text = player.bonusConstitution.ToString();
        showBonusCharismaText.text = player.bonusCharisma.ToString();

        acrobatics.text = player.acrobatics.ToString();
        animalHandling.text = player.animalHandling.ToString();
        arcana.text = player.arcana.ToString();
        athletics.text = player.athletics.ToString();
        deception.text = player.deception.ToString();
        history.text = player.history.ToString();
        insight.text = player.insight.ToString();
        intimidation.text = player.intimidation.ToString();
        investigation.text = player.investigation.ToString();
        medicine.text = player.medicine.ToString();
        nature.text = player.nature.ToString();
        perception.text = player.perception.ToString();
        performance.text = player.performance.ToString();
        persuasion.text = player.persuasion.ToString();
        religion.text = player.religion.ToString();
        sleightOfHand.text = player.sleightOfHand.ToString();
        stealth.text = player.stealth.ToString();
        survival.text = player.survival.ToString();
    }

    private void DisplayAvailableStats()
    {
        string text = "";
        for (int i = 0; i < availableStats.Count; i++)
        {
            text = text + availableStats[i] + " ";
        }

        valuesForStatsText.text = text;
    }

    public void ResetStats()
    {
        for (int i = 0; i < assignedStats.Length; i++)
        {
            if (assignedStats[i] != -1)
            {
                availableStats.Add(assignedStats[i]);
                assignedStats[i] = -1;
            }
        }

        availableStats.Sort();
        availableStats.Reverse();
        DisplayAvailableStats();

        strengthText.text = "0";
        intelligenceText.text = "0";
        dexterityText.text = "0";
        wisdomText.text = "0";
        constitutionText.text = "0";
        charismaText.text = "0";

        bonusStrengthText.text = "0";
        bonusIntelligenceText.text = "0";
        bonusDexterityText.text = "0";
        bonusWisdomText.text = "0";
        bonusConstitutionText.text = "0";
        bonusCharismaText.text = "0";
    }

    private void UpdateNextButtonInteractable()
    {
        bool allStatsAssigned = true;
        for (int i = 0; i < assignedStats.Length; i++)
        {
            if (assignedStats[i] == -1)
            {
                allStatsAssigned = false;
                break;
            }
        }

        nextButton.interactable = allStatsAssigned;
    }

    private void Update()
    {
        UpdateNextButtonInteractable();
        ShowStats();
    }
}
