using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class HexGridGenerator : MonoBehaviour
{
    public GameObject hexPrefab;
    public RectTransform panel;
    public GameObject addPlayerButtonPrefab, addEnemyButtonPrefab, moveButtonPrefab;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float hexSize = 50f;
    public float spacing = 5f;

    private float hexWidth;
    private float hexHeight;

    [SerializeField] private int corX;
    [SerializeField] private int corY;

    void Start()
    {
        if (moveButtonPrefab == null || addPlayerButtonPrefab == null || addEnemyButtonPrefab == null)
        {
            Debug.LogError("One or more button prefabs are not assigned in HexGridGenerator.");
            return;
        }

        hexWidth = hexSize * 2 + spacing;
        hexHeight = Mathf.Sqrt(3) * hexSize + spacing;
        GenerateHexGrid();
    }

    void GenerateHexGrid()
    {
        float gridWidthOffset = (gridWidth - 1) * hexWidth * 0.75f / 2;
        float gridHeightOffset = (gridHeight - 1) * hexHeight / 2;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 position = CalculateHexPosition(x, y);
                position.x -= gridWidthOffset;
                position.y += gridHeightOffset;

                GameObject hex = Instantiate(hexPrefab, panel);
                hex.GetComponent<RectTransform>().anchoredPosition = position;

                HexTile hexTile = hex.GetComponent<HexTile>();
                hexTile.SetCoordinates(x, y);
                hexTile.SetupHexTile(addPlayerButtonPrefab, addEnemyButtonPrefab, moveButtonPrefab);
            }
        }
    }



    Vector2 CalculateHexPosition(int x, int y)
    {
        float xPos = x * hexWidth * 0.75f;
        float yPos = y * hexHeight + (x % 2 == 0 ? 0 : hexHeight / 2);
        return new Vector2(xPos, -yPos);
    }
}
