using UnityEngine;
using UnityEngine.UI;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;
    private GameObject[] actionButtons;
    private bool buttonsVisible = false;

    public void SetupHexTile(GameObject buttonPrefab)
    {
        if (buttonPrefab == null)
        {
            Debug.LogError("Button prefab is not assigned in HexTile.");
            return;
        }

        if (GetComponent<Image>() == null)
        {
            Image image = gameObject.AddComponent<Image>();
            image.color = Color.white;
        }

        actionButtons = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform.parent);
            button.SetActive(false);
            actionButtons[i] = button;
        }
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
        var clickedHexImage = GetComponent<Image>();
        clickedHexImage.color = highlightColor;

        Vector2 position = GetComponent<RectTransform>().anchoredPosition;

        for (int i = 0; i < 3; i++)
        {
            actionButtons[i].SetActive(true);

            Vector2 buttonPosition = position + new Vector2(i * 60f - 60f, 0);
            actionButtons[i].GetComponent<RectTransform>().anchoredPosition = buttonPosition;
        }

        buttonsVisible = true;
    }

    void HideActionButtons()
    {
        foreach (var button in actionButtons)
        {
            button.SetActive(false);
        }

        buttonsVisible = false;
    }
}
