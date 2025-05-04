using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;
    private static GameObject addPlayerButton, addEnemyButton, moveButton;
    private static GameObject buttonPanel;
    private static RectTransform buttonContainer;
    private static bool buttonsVisible = false;

    private static HexTile currentlySelectedHex = null;
    private static HexTile hexWaitingForCharacter = null;

    public GameObject addPlayerPrefab, addEnemyPrefab, movePrefab;
    public int corX;
    public int corY;

    private string saveDirectory;
    private GameObject characterInstanceOnThisTile;

    void Start()
    {
        saveDirectory = Application.persistentDataPath;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnHexClick);

        if (buttonPanel == null)
            CreateButtonPanel();

        if (addPlayerButton == null || addEnemyButton == null || moveButton == null)
            CreateActionButtons();
    }

    public void SetupHexTile(GameObject addPlayerPrefab, GameObject addEnemyPrefab, GameObject movePrefab)
    {
        this.addPlayerPrefab = addPlayerPrefab;
        this.addEnemyPrefab = addEnemyPrefab;
        this.movePrefab = movePrefab;
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
        rectTransform.sizeDelta = new Vector2(300, 400);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;

        Image bgImage = buttonPanel.AddComponent<Image>();
        bgImage.color = new Color(0f, 0f, 0f, 0.6f);

        // Scroll view for vertical buttons
        GameObject scrollObj = new GameObject("ScrollView");
        scrollObj.transform.SetParent(buttonPanel.transform, false);
        ScrollRect scrollRect = scrollObj.AddComponent<ScrollRect>();
        RectTransform scrollRT = scrollObj.GetComponent<RectTransform>();
        scrollRT.sizeDelta = new Vector2(280, 380);
        scrollRT.anchorMin = new Vector2(0.5f, 0.5f);
        scrollRT.anchorMax = new Vector2(0.5f, 0.5f);
        scrollRT.pivot = new Vector2(0.5f, 0.5f);
        scrollRT.anchoredPosition = Vector2.zero;

        GameObject containerObj = new GameObject("ButtonContainer");
        containerObj.transform.SetParent(scrollObj.transform, false);
        buttonContainer = containerObj.AddComponent<RectTransform>();
        VerticalLayoutGroup layout = containerObj.AddComponent<VerticalLayoutGroup>();
        layout.childControlHeight = true;
        layout.childControlWidth = true;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = true;
        layout.spacing = 5f;

        ContentSizeFitter fitter = containerObj.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scrollRect.content = buttonContainer;
        scrollRect.vertical = true;

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
            currentlySelectedHex.ResetColor();

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
        addPlayerButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - 50f);
        addEnemyButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - 100f);

        buttonsVisible = true;
    }

    void ResetColor()
    {
        GetComponent<Image>().color = Color.white;
    }

    void AddPlayerAction()
    {
        hexWaitingForCharacter = this;
        GenerateCharacterButtons();
        buttonPanel.SetActive(true);
        HideActionButtonsFromAll();
    }

    void AddEnemyAction()
    {
        Debug.Log("Add enemy!");
    }

    void MoveAction()
    {
        Debug.Log("Move!");
    }

    void GenerateCharacterButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        string[] files = Directory.GetFiles(saveDirectory, "*.json");

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            GameObject buttonObj = CreateUIButton(fileName, filePath);
            buttonObj.transform.SetParent(buttonContainer, false);
        }
    }

    GameObject CreateUIButton(string label, string filePath)
    {
        GameObject buttonObj = new GameObject(label);
        buttonObj.AddComponent<CanvasRenderer>();
        Button btn = buttonObj.AddComponent<Button>();
        Image img = buttonObj.AddComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0.8f);

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 24;

        RectTransform rt = buttonObj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(260, 40);

        RectTransform textRT = textObj.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;

        btn.onClick.AddListener(() => OnCharacterSelected(filePath));

        return buttonObj;
    }

    void OnCharacterSelected(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"[HexTile] File not found: {filePath}");
            return;
        }

        if (hexWaitingForCharacter == null)
        {
            Debug.LogError("No hex selected for character placement!");
            return;
        }

        string json = File.ReadAllText(filePath);
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        GameObject prefab = Resources.Load<GameObject>("CharacterPrefab");
        if (prefab == null)
        {
            Debug.LogError("CharacterPrefab not found in Resources!");
            return;
        }

        if (hexWaitingForCharacter.characterInstanceOnThisTile != null)
            Destroy(hexWaitingForCharacter.characterInstanceOnThisTile);

        GameObject instance = Instantiate(
            prefab,
            hexWaitingForCharacter.transform.position,
            Quaternion.identity,
            hexWaitingForCharacter.transform
        );

        Player player = instance.GetComponent<Player>();
        if (player != null)
        {
            JsonUtility.FromJsonOverwrite(json, player);
            Debug.Log($"[HexTile] Loaded character: {data.name}");
        }
        else
        {
            Debug.LogError("Player component not found on instantiated character!");
        }

        hexWaitingForCharacter.characterInstanceOnThisTile = instance;
        buttonPanel.SetActive(false);
    }

    [System.Serializable]
    public class CharacterData
    {
        public string name;
        public int level;
        public int health;
        public int maxHealth;
        public int mana;
        public int maxMana;
        public int strength;
        public int agility;
        public int intelligence;
        public int defense;
        public int speed;

        public string characterClass;
        public string race;
        public string weapon;
        public string armor;

        public List<string> abilities;
    }

}
