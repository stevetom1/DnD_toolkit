/*using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class EnemyButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject enemyPrefab;

    private DatabaseManager db;
    private HexTile currentTile;
    private StatsPanelManager statsPanelManager;

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
        statsPanelManager = FindObjectOfType<StatsPanelManager>();
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

            HexTile selectedTile = tile;
            btn.onClick.AddListener(() =>
            {
                AddEnemyToTile(enemy, selectedTile);
                buttonPanel.SetActive(false);
            });
        }

        buttonPanel.SetActive(true);
    }

    private void AddEnemyToTile(Enemy enemyData, HexTile tile)
    {
        if (tile == null)
        {
            Debug.LogError("No tile selected to place the enemy.");
            return;
        }

        if (tile.characterInstanceOnThisTile != null)
        {
            Debug.LogWarning("Cannot place enemy: Player already exists on this tile.");
            return;
        }

        if (tile.enemyObject != null)
        {
            Destroy(tile.enemyObject);
        }

        GameObject enemyObj = Instantiate(enemyPrefab, tile.transform);
        Enemy enemyComponent = enemyObj.GetComponent<Enemy>();

        if (enemyComponent != null)
        {
            //ExportEnemiesToJson(enemyData);
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(enemyData), enemyComponent);
            enemyComponent.hexX = tile.corX;
            enemyComponent.hexY = tile.corY;

            

            Transform visualGO = tile.transform.Find("GameObject");
            if (visualGO != null)
            {
                visualGO.gameObject.SetActive(true);
                TextMeshProUGUI nameText = visualGO.GetComponentInChildren<TextMeshProUGUI>();
                if (nameText != null)
                    nameText.text = enemyData.EnemyName;
            }

            tile.SetEnemy(enemyComponent, enemyObj);
        }
        else
        {
            Debug.LogError("Enemy component missing on prefab!");
        }
        statsPanelManager.DisplayEnemyStats(enemyComponent);
        foreach (var actionAn in enemyData.EnemyAction) Debug.Log("enemy action: " + actionAn.ActionName);
    }


    public void SetCurrentTile(HexTile tile)
    {
        currentTile = tile;
    }

    public void ExportEnemiesToJson(Enemy enemy)
    {
        //List<Enemy> enemies = db.GetAllEnemies();
        if (enemy == null ||enemy.Count == 0)
        {
            Debug.LogWarning("No enemies found to export.");    
            return;
        }

        string json = JsonUtility.ToJson(enemy new EnemyListWrapper { enemies = enemies }, true);

        string path = Path.Combine(Application.persistentDataPath, "Enemies.json");
        File.WriteAllText(path, json);

        Debug.Log("Enemy stats exported to: " + path);
    }

    [System.Serializable]
    private class EnemyListWrapper
    {
        public List<Enemy> enemies;
    }

}*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EnemyButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject enemyPrefab;

    private DatabaseManager db;
    private HexTile currentTile;
    private StatsPanelManager statsPanelManager;

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
        statsPanelManager = FindObjectOfType<StatsPanelManager>();
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

            HexTile selectedTile = tile;
            btn.onClick.AddListener(() =>
            {
                AddEnemyToTile(enemy, selectedTile);
                buttonPanel.SetActive(false);
            });
        }

        buttonPanel.SetActive(true);
    }

    private void AddEnemyToTile(Enemy enemyData, HexTile tile)
    {
        if (tile == null)
        {
            Debug.LogError("No tile selected to place the enemy.");
            return;
        }

        if (tile.characterInstanceOnThisTile != null)
        {
            Debug.LogWarning("Cannot place enemy: Player already exists on this tile.");
            return;
        }

        if (tile.enemyObject != null)
        {
            Destroy(tile.enemyObject);
        }

        GameObject enemyObj = Instantiate(enemyPrefab, tile.transform);
        Enemy enemyComponent = enemyObj.GetComponent<Enemy>();

        if (enemyComponent != null)
        {
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(enemyData), enemyComponent);
            enemyComponent.hexX = tile.corX;
            enemyComponent.hexY = tile.corY;

            Transform visualGO = tile.transform.Find("GameObject");
            if (visualGO != null)
            {
                visualGO.gameObject.SetActive(true);
                TextMeshProUGUI nameText = visualGO.GetComponentInChildren<TextMeshProUGUI>();
                if (nameText != null)
                    nameText.text = enemyData.EnemyName;
            }

            tile.SetEnemy(enemyComponent, enemyObj);
        }
        else
        {
            Debug.LogError("Enemy component missing on prefab!");
        }
        enemyComponent.Health = enemyComponent.MaxHp;
        statsPanelManager.DisplayEnemyStats(enemyComponent);
    }


    public void SetCurrentTile(HexTile tile)
    {
        currentTile = tile;
    }
}