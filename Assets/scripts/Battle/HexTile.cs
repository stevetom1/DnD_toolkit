using UnityEngine;
using UnityEngine.UI;

public class HexTile : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnHexClick);
    }

    public void SetupHexTile()
    {
        if (GetComponent<Image>() == null)
        {
            Image image = gameObject.AddComponent<Image>();
            image.color = Color.white;
        }
    }

    void OnHexClick()
    {
        ResetOtherHexes();

        var clickedHexImage = GetComponent<Image>();
        clickedHexImage.color = highlightColor;
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
            }
        }
    }
}
