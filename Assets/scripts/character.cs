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

    private int[] stats = new int[6];
    private int[] bonusStats = new int[6];
    private List<int> availableStats;
    private int[] assignedStats = new int[6];

    private int hp, strength, dexterity, constitution, intelligence, wisdom, charisma;
    private string characterClass, race;

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

    private string[] characterClasses = new string[]
    {
        "Barbar", "Bard", "Bojovník", "Èarodìj", "Èernoknìžník", "Druid", "Hranièáø", "Klerik", "Kouzelník", "Mnich", "Paladin", "Tulák"
    };

    private int[] classHPValues = new int[]
    {
        12, 8, 10, 6, 8, 8, 10, 8, 6, 8, 10, 8
    };

    private string[] races = new string[]
    {
        "Èlovìk", "Lesní elf", "Temný elf", "Vznešený elf", "Hobit Poøízek", "Hobit Tichošlápek", "Horský trpaslík", "Kopcový trpaslík", "Tiefling"
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
            availableStats.Add(i);
        }
        valuesForStatsText.text = $"{stats[0]} {stats[1]} {stats[2]} {stats[3]} {stats[4]} {stats[5]}";

        int bonusStrength = Mathf.FloorToInt((stats[0] / 2)) - 5;

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
        if (assignedStats[statIndex] != -1)
        {
            availableStats.Add(assignedStats[statIndex]);
        }

        int currentIndex = assignedStats[statIndex] == -1 ? 0 : availableStats.IndexOf(assignedStats[statIndex]);

        currentIndex = (currentIndex + direction + availableStats.Count) % availableStats.Count;

        assignedStats[statIndex] = availableStats[currentIndex];
        availableStats.RemoveAt(currentIndex);

        int baseValue = stats[assignedStats[statIndex]];

        if (race == "èlovìk")
        {
            baseValue += 1;
        }
        else if (race == "Lesní elf" || race == "Temný elf" || race == "Vznešený elf")
        {
            if (statIndex == 2) baseValue += 2;

            if (race == "Vznešený elf" && statIndex == 1) baseValue += 1;
            else if (race == "Lesní elf" && statIndex == 3) baseValue += 1;
        }
        else if (race == "Hobit Poøízek" || race == "Hobit Tichošlápek")
        {
            if (statIndex == 2) baseValue += 2;

            if (race == "Hobit Poøízek" && statIndex == 4) baseValue += 1;
            else if (race == "Hobit Tichošlápek" && statIndex == 5) baseValue += 1;
        }
        else if (race == "Horský trpaslík" || race == "Kopcový trpaslík")
        {
            if (statIndex == 4) baseValue += 2;

            if (race == "Horský trpaslík" && statIndex == 0) baseValue += 2;
            else if (race == "Kopcový trpaslík" && statIndex == 3) baseValue += 1;
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
                showBonusStrengthText.text = bonusStrengthText.text;
                break;
            case 1:
                bonusIntelligenceText.text = bonusValue.ToString();
                showBonusIntelligenceText.text = bonusIntelligenceText.text;
                break;
            case 2:
                bonusDexterityText.text = bonusValue.ToString();
                showBonusDexterityText.text = bonusDexterityText.text;
                break;
            case 3:
                bonusWisdomText.text = bonusValue.ToString();
                showWisdomText.text = bonusDexterityText.text;
                break;
            case 4:
                bonusConstitutionText.text = bonusValue.ToString();
                showBonusConstitutionText.text = constitutionText.text;
                break;
            case 5:
                bonusCharismaText.text = bonusValue.ToString();
                showCharismaText.text = bonusCharismaText.text;
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
        characterClass = characterClassText.text;
        race = raceText.text;
        hp = int.Parse(hpText.text);
    }

    public void EnterValuesStats()
    {
        strength = int.Parse(strengthText.text);
        dexterity = int.Parse(dexterityText.text);
        constitution = int.Parse(constitutionText.text);
        intelligence = int.Parse(intelligenceText.text);
        wisdom = int.Parse(wisdomText.text);
        charisma = int.Parse(charismaText.text);
    }

    public void showStats()
    {
    showHpText.text = hp.ToString();
    showStrengthText.text = strength.ToString();
    showIntelligenceText.text = intelligence.ToString();
    showDexterityText.text = dexterity.ToString();
    showWisdomText.text = wisdom.ToString();
    showConstitutionText.text = constitution.ToString();
    showCharismaText.text = charisma.ToString();

    acrobatics.text = bonusDexterityText.text;
    animalHandling.text = bonusWisdomText.text;
    arcana.text = bonusIntelligenceText.text;
    athletics.text = bonusStrengthText.text;
    deception.text = bonusCharismaText.text;
    history.text = bonusIntelligenceText.text;
    insight.text = bonusWisdomText.text;
    intimidation.text = bonusCharismaText.text;
    investigation.text = bonusIntelligenceText.text;
    medicine.text = bonusWisdomText.text;
    nature.text = bonusIntelligenceText.text;
    perception.text = bonusWisdomText.text;
    performance.text = showBonusCharismaText.text;
    persuasion.text = bonusCharismaText.text;
    religion.text = bonusIntelligenceText.text;
    sleightOfHand.text = bonusDexterityText.text;
    stealth.text = bonusDexterityText.text;    
    survival.text = bonusWisdomText.text;
    }
}
