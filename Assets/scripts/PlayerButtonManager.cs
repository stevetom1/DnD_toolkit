using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class CharacterButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    public GameObject viewCharacters1;
    public GameObject viewCharacters2;

    private string saveDirectory;


    public TextMeshProUGUI showName;
    public TextMeshProUGUI showClass;
    public TextMeshProUGUI showRace;

    public TextMeshProUGUI showHpText;
    public TextMeshProUGUI showRemainingHpText;
    public TextMeshProUGUI showIniciative;
    public TextMeshProUGUI showSpeed;
    public TextMeshProUGUI showDefense;

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


    void Start()
    {
        saveDirectory = Application.persistentDataPath;
        GenerateButtons();
        player = FindObjectOfType<Player>();
    }

    private void ShowStats()
    {
        showName.text = player.name;
        showClass.text = player.characterClass;
        showRace.text = player.race;

        showHpText.text = player.hp.ToString();

        showIniciative.text = player.iniciative.ToString();
        showSpeed.text = player.speed.ToString();
        showDefense.text = player.defense.ToString();
        showRemainingHpText.text = player.remainingHp.ToString();

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

    public void GenerateButtons()
    {
        if (buttonPrefab == null)
        {
            Debug.LogError("Button prefab is not assigned!");
            return;
        }

        if (buttonContainer == null)
        {
            Debug.LogError("Button container is not assigned!");
            return;
        }

        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        string[] files = Directory.GetFiles(saveDirectory, "*.json");

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Debug.Log("Creating button for: " + fileName);

            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.localScale = Vector3.one;
                rectTransform.anchoredPosition = Vector2.zero;
            }

            var buttonText = newButton.GetComponentInChildren<Text>();
            var buttonTMP = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = fileName;
            }
            else if (buttonTMP != null)
            {
                buttonTMP.text = fileName;
            }
            else
            {
                Debug.LogError("Button prefab does not contain a Text or TextMeshProUGUI component");
                continue;
            }

            var buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => LoadCharacter(filePath));
            }
            else
            {
                Debug.LogError("Button prefab does not contain a Button component");
            }
        }
    }



    void LoadCharacter(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Player player = GameObject.Find("Character")?.GetComponent<Player>();

            if (player != null)
            {
                JsonUtility.FromJsonOverwrite(json, player);
                Debug.Log($"Loaded character data from: {filePath}");
            }
            else
            {
                Debug.LogError("Player script not found on Character GameObject");
            }
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }

        viewCharacters1.SetActive(false);
        viewCharacters2.SetActive(true);
        ShowStats();
    }
}