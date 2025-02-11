using UnityEngine;
using UnityEngine.UI;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;
    private GameObject moveButton, attackButton, defendButton;
    private bool buttonsVisible = false;
    private float buttonSpacing = 10f;

    public void SetupHexTile(GameObject movePrefab, GameObject attackPrefab, GameObject defendPrefab)
    {
        if (movePrefab == null || attackPrefab == null || defendPrefab == null)
        {
            Debug.LogError("One or more button prefabs are not assigned in HexTile.");
            return;
        }

        if (GetComponent<Image>() == null)
        {
            Image image = gameObject.AddComponent<Image>();
            image.color = Color.white;
        }

        moveButton = Instantiate(movePrefab, transform.parent.parent);
        attackButton = Instantiate(attackPrefab, transform.parent.parent);
        defendButton = Instantiate(defendPrefab, transform.parent.parent);

        moveButton.SetActive(false);
        attackButton.SetActive(false);
        defendButton.SetActive(false);

        moveButton.GetComponent<Button>().onClick.AddListener(() => MoveAction());
        attackButton.GetComponent<Button>().onClick.AddListener(() => AttackAction());
        defendButton.GetComponent<Button>().onClick.AddListener(() => DefendAction());
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

    void ShowActionButtons()
    {
        GetComponent<Image>().color = highlightColor;

        Vector2 position = GetComponent<RectTransform>().anchoredPosition;

        float offsetX = 150f;
        float offsetY = 40f;

        moveButton.SetActive(true);
        attackButton.SetActive(true);
        defendButton.SetActive(true);

        moveButton.transform.SetAsLastSibling();
        attackButton.transform.SetAsLastSibling();
        defendButton.transform.SetAsLastSibling();

        moveButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY);
        attackButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - (buttonSpacing + 40f));
        defendButton.GetComponent<RectTransform>().anchoredPosition = position + new Vector2(offsetX, offsetY - 2 * (buttonSpacing + 40f));

        buttonsVisible = true;
    }

    void HideActionButtons()
    {
        moveButton.SetActive(false);
        attackButton.SetActive(false);
        defendButton.SetActive(false);
        buttonsVisible = false;
    }

    void MoveAction()
    {
        Debug.Log("Move action triggered!");
    }

    void AttackAction()
    {
        Debug.Log("Attack action triggered!");
    }

    void DefendAction()
    {
        Debug.Log("Defend action triggered!");
    }
}
