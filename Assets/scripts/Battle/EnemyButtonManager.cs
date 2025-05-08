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
        GameObject enemyObj = Instantiate(enemyPrefab, currentTile.transform.position, Quaternion.identity);
        currentTile.SetEnemy(enemy, enemyObj);
    }
}
