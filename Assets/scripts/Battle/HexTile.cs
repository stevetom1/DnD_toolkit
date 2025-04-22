using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;
    private static GameObject addPlayerButton, addEnemyButton, moveButton;
    private static GameObject buttonPanel;
    private static bool buttonsVisible = false;
    private float buttonSpacing = 10f;

    private static HexTile currentlySelectedHex = null;

    public GameObject addPlayerPrefab, addEnemyPrefab, movePrefab;

    public int corX;
    public int corY;

    private string saveDirectory;
    private GameObject buttonPrefab;

    public void SetupHexTile(GameObject addPlayerPrefab, GameObject addEnemyPrefab, GameObject movePrefab)
    {
        this.addPlayerPrefab = addPlayerPrefab;
        this.addEnemyPrefab = addEnemyPrefab;
        this.movePrefab = movePrefab;
    }

    void Start()
    {
        saveDirectory = Application.persistentDataPath;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnHexClick);

        if (buttonPanel == null)
            CreateButtonPanel();

        if (addPlayerButton == null || addEnemyButton == null || moveButton == null)
            CreateActionButtons();

        buttonPrefab = Resources.Load<GameObject>("CharacterButton");
        if (buttonPrefab == null)
        {
            Debug.LogError("CharacterButton prefab not found in Resources folder");
        }
    }

    void CreateButtonPanel()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        buttonPanel = new GameObject("ButtonPanel");
        buttonPanel.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = buttonPanel.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(500, 1600);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;

        Image bgImage = buttonPanel.AddComponent<Image>();
        bgImage.color = new Color(0f, 0f, 0f, 0.6f);

        VerticalLayoutGroup layoutGroup = buttonPanel.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = true;
        layoutGroup.childAlignment = TextAnchor.UpperCenter;
        layoutGroup.spacing = 10f;

        ContentSizeFitter fitter = buttonPanel.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        buttonPanel.SetActive(false);
    }

    void CreateActionButtons()
    {
        Transform parent = transform.parent.parent;

        addPlayerButton = Instantiate(addPlayerPrefab, parent);
        addEnemyButton = Instantiate(addEnemyPrefab, parent);
        moveButton = Instantiate(movePrefab, parent);

        addPlayerButton.SetActive(false);
        addEnemyButton.SetActive(false);
        moveButton.SetActive(false);

        addPlayerButton.GetComponent<Button>().onClick.RemoveAllListeners();
        addEnemyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        moveButton.GetComponent<Button>().onClick.RemoveAllListeners();

        addPlayerButton.GetComponent<Button>().onClick.AddListener(() => AddPlayerAction());
        addEnemyButton.GetComponent<Button>().onClick.AddListener(() => AddEnemyAction());
        moveButton.GetComponent<Button>().onClick.AddListener(() => MoveAction());
    }

    void OnHexClick()
    {
        if (buttonPanel != null)
            buttonPanel.SetActive(false);

        if (currentlySelectedHex != null && currentlySelectedHex != this)
        {
            currentlySelectedHex.ResetColor();
        }

        HideActionButtonsFromAll();

        if (buttonsVisible && currentlySelectedHex == this)
        {
            HideActionButtons();
            currentlySelectedHex = null;
        }
        else
        {
            ShowActionButtons();
            currentlySelectedHex = this;
        }
    }

    static void HideActionButtonsFromAll()
    {
        if (addPlayerButton != null) addPlayerButton.SetActive(false);
        if (addEnemyButton != null) addEnemyButton.SetActive(false);
        if (moveButton != null) moveButton.SetActive(false);
        buttonsVisible = false;
    }

    void HideActionButtons()
    {
        HideActionButtonsFromAll();
    }

    void ShowActionButtons()
    {
        GetComponent<Image>().color = highlightColor;

        Vector2 position = GetComponent<RectTransform>().anchoredPosition;
        float offsetX = 5f;
        float offsetY = 40f;

        addPlayerButton.SetActive(true);
        addEnemyButton.SetActive(true);
        moveButton.SetActive(true);

        moveButton.transform.SetAsLastSibling();
        addPlayerButton.transform.SetAsLastSibling();
        addEnemyButton.transform.SetAsLastSibling();

        moveButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY);
        addPlayerButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - (buttonSpacing + 40f));
        addEnemyButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - 2 * (buttonSpacing + 40f));

        buttonsVisible = true;
    }

    void ResetColor()
    {
        GetComponent<Image>().color = Color.white;
    }

    void AddPlayerAction()
    {
        Debug.Log("Add player!");
        if (buttonPanel != null)
        {
            GenerateCharacterButtons();
            buttonPanel.SetActive(true);
        }

        HideActionButtonsFromAll();
    }

    void AddEnemyAction()
    {
        Debug.Log("Add enemyy!");
    }

    void MoveAction()
    {
        Debug.Log("Move!");
    }

    private void GenerateCharacterButtons()
    {
        foreach (Transform child in buttonPanel.transform)
        {
            Destroy(child.gameObject);
        }

        string[] files = Directory.GetFiles(saveDirectory, "*.json");

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            GameObject newButton = Instantiate(buttonPrefab, buttonPanel.transform);

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
                Debug.LogWarning("Button missing Text/TextMeshProUGUI");
            }

            var buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnCharacterSelected(filePath));
            }
        }
    }

    private void OnCharacterSelected(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Player player = GameObject.Find("Character")?.GetComponent<Player>();

            if (player != null)
            {
                JsonUtility.FromJsonOverwrite(json, player);
                Debug.Log($"[HexTile] Loaded character from: {filePath}");
            }
            else
            {
                Debug.LogError("Player component not found on GameObject 'Character'");
            }
        }
        else
        {
            Debug.LogError($"[HexTile] File not found: {filePath}");
        }

        buttonPanel.SetActive(false);
    }
}