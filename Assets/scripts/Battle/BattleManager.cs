using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartBattle(HexTile playerTile, HexTile enemyTile, string attackType)
    {
        GameObject playerObj = playerTile.characterInstanceOnThisTile;
        GameObject enemyObj = enemyTile.enemyObject;

        if (playerObj == null || enemyObj == null)
        {
            Debug.LogWarning("Missing player or enemy GameObject.");
            return;
        }

        Player player = playerObj.GetComponent<Player>();
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (player == null || enemy == null)
        {
            Debug.LogError("Missing Player or Enemy script.");
            return;
        }

        int damage = 0;// = player.Attack;
        if (attackType == "Spell") damage += 2; // Example logic
        if (attackType == "Ranged") damage -= 1;

        enemy.Health -= damage;
        Debug.Log($"{attackType} attack: {player.name} deals {damage} to {enemy.EnemyName}");

        if (enemy.Health <= 0)
        {
            Debug.Log($"{enemy.EnemyName} is defeated!");
            GameObject.Destroy(enemyObj);
            enemyTile.enemyObject = null;
            enemyTile.enemyOnTile = null;
            enemyTile.hasEnemy = false;
        }
        else
        {
            // Optional: retaliate logic
        }
    }
}