using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;
    private static GameObject addPlayerButton, addEnemyButton, moveButton;
    private static GameObject buttonPanel;
    private static Transform buttonContainer;
    private static bool buttonsVisible = false;
    private float buttonSpacing = 10f;

    private static HexTile currentlySelectedHex = null;

    public GameObject characterInstanceOnThisTile;
    private string saveDirectory;
    private static GameObject buttonPrefab;

    public int corX;
    public int corY;

    public GameObject addPlayerPrefab, addEnemyPrefab, movePrefab;

    public Enemy enemyOnTile;
    public GameObject enemyObject;
    public bool hasEnemy = false;

    private EnemyButtonManager enemyButtonManager;
    private StatsPanelManager statsPanelManager;

    private static bool isMoveMode = false;
    private static HexTile originTile = null;
    private static List<HexTile> highlightedTiles = new List<HexTile>();
    public static List<HexTile> allTiles = new List<HexTile>();

    public int moveRange = 0;

    public void SetupHexTile(GameObject addPlayerPrefab, GameObject addEnemyPrefab, GameObject movePrefab)
    {
        this.addPlayerPrefab = addPlayerPrefab;
        this.addEnemyPrefab = addEnemyPrefab;
        this.movePrefab = movePrefab;
    }

    public void SetCoordinates(int x, int y)
    {
        corX = x;
        corY = y;
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnHexClick);

        saveDirectory = Application.persistentDataPath;

        if (buttonPanel == null)
            CreateButtonPanel();

        if (addPlayerButton == null || addEnemyButton == null || moveButton == null)
            CreateActionButtons();

        if (buttonPrefab == null)
            buttonPrefab = Resources.Load<GameObject>("CharacterButton");

        statsPanelManager = FindObjectOfType<StatsPanelManager>();

        allTiles.Add(this);
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
        rectTransform.sizeDelta = new Vector2(300, 500);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;

        Image bgImage = buttonPanel.AddComponent<Image>();
        bgImage.color = new Color(0f, 0f, 0f, 0.6f);

        VerticalLayoutGroup layout = buttonPanel.AddComponent<VerticalLayoutGroup>();
        layout.childAlignment = TextAnchor.UpperCenter;
        layout.spacing = 10f;
        layout.padding = new RectOffset(10, 10, 10, 10);

        ContentSizeFitter fitter = buttonPanel.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        buttonContainer = buttonPanel.transform;
        buttonPanel.SetActive(false);
    }

    void CreateActionButtons()
    {
        Transform parent = transform.parent?.parent ?? transform.parent;

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
        if (isMoveMode)
        {
            if (highlightedTiles.Contains(this))
            {
                MoveCharacterToThisTile();
            }
            else
            {
                CancelMove();
            }

            return;
        }

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

            EnemyButtonManager.Instance.SetCurrentTile(this);
        }

        if (statsPanelManager != null)
        {
            if (characterInstanceOnThisTile != null)
            {
                Player player = characterInstanceOnThisTile.GetComponent<Player>();
                if (player != null)
                    statsPanelManager.DisplayPlayerStats(player);
            }
            else if (hasEnemy && enemyOnTile)
            {
                statsPanelManager.DisplayEnemyStats(enemyOnTile);
            }
            else
            {
                statsPanelManager.ClearStats();
            }
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
        if (hasEnemy)
        {
            Debug.LogWarning("Cannot add player: Tile already contains an enemy.");
            return;
        }

        if (buttonPanel != null)
        {
            GenerateCharacterButtons();
            buttonPanel.SetActive(true);
        }

        HideActionButtonsFromAll();
    }

    void AddEnemyAction()
    {
        if (characterInstanceOnThisTile != null)
        {
            Debug.LogWarning("Cannot add enemy: Tile already contains a player.");
            return;
        }

        if (EnemyButtonManager.Instance == null)
        {
            Debug.LogError("EnemyButtonManager instance not found");
            return;
        }

        EnemyButtonManager.Instance.ShowEnemySelection(currentlySelectedHex, buttonContainer, buttonPanel);
        HideActionButtonsFromAll();
    }

    public void SetEnemy(Enemy enemy, GameObject enemyObj)
    {
        this.enemyOnTile = enemy;
        this.enemyObject = enemyObj;
        this.hasEnemy = true;
    }

    void MoveAction()
    {
        if (characterInstanceOnThisTile == null && !hasEnemy) {
            Debug.Log(characterInstanceOnThisTile);
                return;
        }
        Debug.Log("Is player or enemy");
        isMoveMode = true;
        originTile = this;

        moveRange = 0;

        if (characterInstanceOnThisTile != null)
        {
            var player = characterInstanceOnThisTile.GetComponent<Player>();
            if (player != null)
            {
                moveRange = player.speed;
                Debug.Log(moveRange);
            }
        }
        else if (enemyOnTile != null)
        {
            moveRange = enemyOnTile.Speed;
        }

        HighlightReachableTiles(moveRange);
        HideActionButtonsFromAll();
    }

    void HighlightReachableTiles(int range)
    {
        highlightedTiles.Clear();

        foreach (HexTile tile in allTiles)
        {
            int distance = Mathf.Abs(tile.corX - corX) + Mathf.Abs(tile.corY - corY);
            if (distance <= range && tile != this && !tile.IsOccupied())
            {
                tile.GetComponent<Image>().color = Color.cyan;
                highlightedTiles.Add(tile);
            }
        }
    }

    void MoveCharacterToThisTile()
    {
        if (originTile == null) return;

        if (originTile.characterInstanceOnThisTile != null)
        {
            characterInstanceOnThisTile = originTile.characterInstanceOnThisTile;
            characterInstanceOnThisTile.transform.SetParent(transform);
            characterInstanceOnThisTile.transform.localPosition = Vector3.zero;
            originTile.characterInstanceOnThisTile = null;

            var player = characterInstanceOnThisTile.GetComponent<Player>();
            if (player != null)
            {
                player.hexX = corX;
                player.hexY = corY;
            }
        }
        else if (originTile.enemyObject != null)
        {
            enemyObject = originTile.enemyObject;
            enemyObject.transform.SetParent(transform);
            enemyObject.transform.localPosition = Vector3.zero;
            enemyOnTile = originTile.enemyOnTile;
            hasEnemy = true;

            originTile.enemyObject = null;
            originTile.enemyOnTile = null;
            originTile.hasEnemy = false;

            if (enemyOnTile != null)
            {
                enemyOnTile.hexX = corX;
                enemyOnTile.hexY = corY;
            }
        }

        CancelMove();
    }

    void CancelMove()
    {
        isMoveMode = false;
        originTile = null;

        foreach (HexTile tile in highlightedTiles)
        {
            tile.GetComponent<Image>().color = Color.white;
        }

        highlightedTiles.Clear();
    }

    void GenerateCharacterButtons()
    {
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        string[] files = Directory.GetFiles(saveDirectory, "*.json");

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            var text = newButton.GetComponentInChildren<Text>();
            var tmp = newButton.GetComponentInChildren<TextMeshProUGUI>();

            if (text != null)
                text.text = fileName;
            else if (tmp != null)
                tmp.text = fileName;

            Button btn = newButton.GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(() => OnCharacterSelected(filePath));
        }
    }

    void OnCharacterSelected(string filePath)
    {
        if (currentlySelectedHex == null)
        {
            Debug.LogError("No hex tile selected");
            return;
        }

        if (currentlySelectedHex.hasEnemy)
        {
            Debug.LogWarning("Cannot place player: Tile already has an enemy.");
            buttonPanel.SetActive(false);
            return;
        }

        if (!File.Exists(filePath))
        {
            Debug.LogError($"[HexTile] File not found: {filePath}");
            return;
        }

        string json = File.ReadAllText(filePath);
        GameObject prefab = Resources.Load<GameObject>("CharacterPrefab");

        if (prefab == null)
        {
            Debug.LogError("CharacterPrefab not found in Resources folder");
            return;
        }

        if (currentlySelectedHex.characterInstanceOnThisTile != null)
            Destroy(currentlySelectedHex.characterInstanceOnThisTile);

        currentlySelectedHex.characterInstanceOnThisTile = Instantiate(prefab, currentlySelectedHex.transform);
        currentlySelectedHex.characterInstanceOnThisTile.transform.localPosition = Vector3.zero;

        Player player = currentlySelectedHex.characterInstanceOnThisTile.GetComponent<Player>();
        if (player != null)
        {
            JsonUtility.FromJsonOverwrite(json, player);
            player.hexX = currentlySelectedHex.corX;
            player.hexY = currentlySelectedHex.corY;

            Transform visualGO = currentlySelectedHex.transform.Find("GameObject");
            if (visualGO != null)
            {
                visualGO.gameObject.SetActive(true);
                TextMeshProUGUI nameText = visualGO.GetComponentInChildren<TextMeshProUGUI>();
                if (nameText != null)
                    nameText.text = player.name;
            }
        }
        else
        {
            Debug.LogError("Player component not found on character prefab");
        }

        buttonPanel.SetActive(false);

        if (statsPanelManager != null)
        {
            if (characterInstanceOnThisTile != null)
            {
                if (player != null)
                    statsPanelManager.DisplayPlayerStats(player);
            }
            else if (hasEnemy && enemyOnTile != null)
            {
                statsPanelManager.DisplayEnemyStats(enemyOnTile);
            }
            else
            {
                statsPanelManager.ClearStats();
            }
        }

        statsPanelManager.DisplayPlayerStats(player);
    }

    public bool IsOccupied()
    {
        return characterInstanceOnThisTile != null || hasEnemy;
    }
}


