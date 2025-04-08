using UnityEngine;
using UnityEngine.UI;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;
    public GameObject addPlayerButton, addEnemyButton, moveButton;
    public GameObject buttonPanel;
    private bool buttonsVisible = false;
    private float buttonSpacing = 10f;

    public int corX;
    public int corY;

    public void SetupHexTile(GameObject addPlayerPrefab, GameObject addEnemyPrefab, GameObject movePrefab)
    {
        if (movePrefab == null || addPlayerPrefab == null || addEnemyPrefab == null)
        {
            Debug.LogError("One or more button prefabs are not assigned in HexTile.");
            return;
        }

        if (GetComponent<Image>() == null)
        {
            Image image = gameObject.AddComponent<Image>();
            image.color = Color.white;
        }

        addPlayerButton = Instantiate(addPlayerPrefab, transform.parent.parent);
        addEnemyButton = Instantiate(addEnemyPrefab, transform.parent.parent);
        moveButton = Instantiate(movePrefab, transform.parent.parent);

        addPlayerButton.SetActive(false);
        addEnemyButton.SetActive(false);
        moveButton.SetActive(false);

        addPlayerButton.GetComponent<Button>().onClick.AddListener(() => AddPlayerAction());
        addEnemyButton.GetComponent<Button>().onClick.AddListener(() => AddEnemyAction());
        moveButton.GetComponent<Button>().onClick.AddListener(() => MoveAction());
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnHexClick);
    }

    void OnHexClick()
    {
        ResetOtherHexes();

        if (buttonsVisible)
        {
            HideActionButtons();
        }
        else
        {
            ShowActionButtons();
        }
    }

    void ResetOtherHexes()
    {
        foreach (Transform sibling in transform.parent)
        {
            var hexTile = sibling.GetComponent<HexTile>();
            if (hexTile != null)
            {
                var image = sibling.GetComponent<Image>();
                image.color = Color.white;
                hexTile.HideActionButtons();
            }
        }
    }
    void HideActionButtons()
    {
        moveButton.SetActive(false);
        addPlayerButton.SetActive(false);
        addEnemyButton.SetActive(false);
        buttonsVisible = false;
    }

    void ShowActionButtons()
    {
        GetComponent<Image>().color = highlightColor;

        Vector2 position = GetComponent<RectTransform>().anchoredPosition;

        float offsetX = 5f;
        float offsetY = 40f;

        moveButton.SetActive(true);
        addPlayerButton.SetActive(true);
        addEnemyButton.SetActive(true);

        moveButton.transform.SetAsLastSibling();
        addPlayerButton.transform.SetAsLastSibling();
        addEnemyButton.transform.SetAsLastSibling();

        moveButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY);
        addPlayerButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - (buttonSpacing + 40f));
        addEnemyButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - 2 * (buttonSpacing + 40f));

        buttonsVisible = true;
    }

    void AddPlayerAction()
    {
        Debug.Log("Add player!");
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