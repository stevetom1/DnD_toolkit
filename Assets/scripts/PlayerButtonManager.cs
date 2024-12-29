using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class CharacterButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    private string saveDirectory;

    void Start()
    {
        saveDirectory = Application.persistentDataPath;
        GenerateButtons();
    }

    void GenerateButtons()
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
                Debug.LogError("Button prefab does not contain a Text or TextMeshProUGUI component!");
                continue;
            }

            var buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => LoadCharacter(filePath));
            }
            else
            {
                Debug.LogError("Button prefab does not contain a Button component!");
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
                Debug.LogError("Player script not found on Character GameObject!");
            }
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
    }
}
