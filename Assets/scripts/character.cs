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

    private Player player;


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

        player = FindObjectOfType<Player>();
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
        //System.Array.Sort(stats);
        //System.Array.Reverse(stats);
        availableStats.Sort();
        availableStats.Reverse();
        //valuesForStatsText.text = $"{stats[0]} {stats[1]} {stats[2]} {stats[3]} {stats[4]} {stats[5]}";
        DisplayAvailableStats();

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
        Debug.Log(statIndex);
        if (assignedStats[statIndex] != -1)
        {
            availableStats.Add(assignedStats[statIndex]);
            availableStats.Sort();
            availableStats.Reverse();
            DisplayAvailableStats();
        }

        int currentIndex = assignedStats[statIndex] == -1 ? 0 : availableStats.IndexOf(assignedStats[statIndex]);
        Debug.Log(currentIndex);


        currentIndex = (currentIndex + direction + availableStats.Count) % availableStats.Count;

        Debug.Log(currentIndex);

        assignedStats[statIndex] = availableStats[currentIndex];
        availableStats.RemoveAt(currentIndex);
        DisplayAvailableStats();



        //int baseValue = stats[assignedStats[statIndex]];
        int baseValue = assignedStats[statIndex];


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
        characterClass = characterClassText.text;
        race = raceText.text;
        hp = int.Parse(hpText.text);

        player.EnterValuesClass(characterClass, race, hp);

        Debug.Log(player.characterClass);
        Debug.Log(player.race);
        Debug.Log(player.hp);
    }

    public void EnterValuesStats()
    {
        strength = int.Parse(strengthText.text);
        dexterity = int.Parse(dexterityText.text);
        constitution = int.Parse(constitutionText.text);
        intelligence = int.Parse(intelligenceText.text);
        wisdom = int.Parse(wisdomText.text);
        charisma = int.Parse(charismaText.text);

        player.EnterValuesStats(strength, dexterity, constitution, intelligence, wisdom, charisma);

        Debug.Log(player.strength);
        Debug.Log(player.bonusStrength);
    }

    public void showStats()
    {
    showHpText.text = player.hp.ToString();
    showStrengthText.text = player.strength.ToString();
    showIntelligenceText.text = player.intelligence.ToString();
    showDexterityText.text = player.dexterity.ToString();
    showWisdomText.text = player.wisdom.ToString();
    showConstitutionText.text = player.constitution.ToString();
    showCharismaText.text = player.charisma.ToString();

    showBonusStrengthText.text = player.bonusStrength.ToString();
    showBonusIntelligenceText.text = player.bonusIntelligence.ToString();
    showBonusDexterityText.text = player.dexterity.ToString();
    showBonusWisdomText.text = player.wisdom.ToString();
    showBonusConstitutionText.text = player.constitution.ToString();
    showBonusCharismaText.text = player.charisma.ToString();

    acrobatics.text = player.bonusDexterity.ToString();
    animalHandling.text = player.bonusWisdom.ToString();
    arcana.text = player.bonusIntelligence.ToString();
    athletics.text = player.bonusStrength.ToString();
    deception.text = player.bonusCharisma.ToString();
    history.text = player.bonusIntelligence.ToString();
    insight.text = player.bonusWisdom.ToString();
    intimidation.text = player.bonusCharisma.ToString();
    investigation.text = player.bonusIntelligence.ToString();
    medicine.text = player.bonusWisdom.ToString();
    nature.text = player.bonusIntelligence.ToString();
    perception.text = player.bonusWisdom.ToString();
    performance.text = player.bonusCharisma.ToString();
    persuasion.text = player.bonusCharisma.ToString();
    religion.text = player.bonusIntelligence.ToString();
    sleightOfHand.text = player.bonusDexterity.ToString();
    stealth.text = player.bonusDexterity.ToString();    
    survival.text = player.bonusWisdom.ToString();
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

}
