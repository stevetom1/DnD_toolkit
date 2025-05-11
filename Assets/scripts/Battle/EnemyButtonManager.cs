using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EnemyButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject enemyPrefab; // Ensure this prefab has an Enemy component for stats

    private DatabaseManager db;
    private HexTile currentTile;

    public static EnemyButtonManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        db = FindObjectOfType<DatabaseManager>();
    }

    public void ShowEnemySelection(HexTile tile, Transform buttonContainer, GameObject buttonPanel)
    {
        currentTile = tile;

        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        List<Enemy> enemies = db.GetAllEnemies();
        foreach (var enemy in enemies)
        {
            GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
            TMP_Text label = btnObj.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = enemy.EnemyName;

            Button btn = btnObj.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                AddEnemyToTile(enemy);
                buttonPanel.SetActive(false);
            });
        }

        buttonPanel.SetActive(true);
    }

    private void AddEnemyToTile(Enemy enemy)
    {
        if (currentTile == null)
        {
            Debug.LogError("No tile selected to place the enemy.");
            return;
        }

        // Destroy any existing enemy on this tile
        if (currentTile.enemyObject != null)
        {
            GameObject.Destroy(currentTile.enemyObject);
        }

        // Instantiate on correct tile
        GameObject enemyObj = Instantiate(enemyPrefab, currentTile.transform);
        enemyObj.transform.localPosition = Vector3.zero;

        // Assign stats
        Enemy enemyComponent = enemyObj.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(enemy), enemyComponent);

            // Optional logging
            Debug.Log($"Spawned enemy '{enemy.EnemyName}' at hex ({currentTile.corX}, {currentTile.corY})");

            // Optional: Display enemy name on tile if UI is set up
            Transform visualGO = currentTile.transform.Find("GameObject");
            if (visualGO != null)
            {
                visualGO.gameObject.SetActive(true);
                TextMeshProUGUI nameText = visualGO.GetComponentInChildren<TextMeshProUGUI>();
                if (nameText != null)
                {
                    nameText.text = enemy.EnemyName;
                }
                else
                {
                    Debug.LogWarning("TextMeshProUGUI not found inside GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Child 'GameObject' not found on hex tile.");
            }
        }
        else
        {
            Debug.LogError("The enemy prefab does not have an Enemy component to receive stats.");
        }

        // Register enemy on tile
        currentTile.SetEnemy(enemy, enemyObj);
    }
}
