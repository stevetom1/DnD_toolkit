using UnityEngine;
using UnityEngine.UI;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;
    private static GameObject addPlayerButton, addEnemyButton, moveButton; // Shared buttons
    private static GameObject buttonPanel;
    private static bool buttonsVisible = false;
    private float buttonSpacing = 10f;

    private static HexTile currentlySelectedHex = null;

    public GameObject addPlayerPrefab, addEnemyPrefab, movePrefab;

    public int corX;
    public int corY;

    public void SetupHexTile(GameObject addPlayerPrefab, GameObject addEnemyPrefab, GameObject movePrefab)
    {
        this.addPlayerPrefab = addPlayerPrefab;
        this.addEnemyPrefab = addEnemyPrefab;
        this.movePrefab = movePrefab;
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnHexClick);

        if (buttonPanel == null)
            CreateButtonPanel();

        if (addPlayerButton == null || addEnemyButton == null || moveButton == null)
            CreateActionButtons();
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
        rectTransform.sizeDelta = new Vector2(1000, 800);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;

        Image bgImage = buttonPanel.AddComponent<Image>();
        bgImage.color = new Color(0f, 0f, 0f, 0.6f);

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
            buttonPanel.SetActive(true);

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
}
