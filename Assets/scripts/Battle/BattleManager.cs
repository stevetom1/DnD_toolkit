using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public GameObject battlePanelPrefab;
    private GameObject battlePanelInstance;

    public Button meleeButton;
    public Button rangedButton;
    public Button spellsButton;

    private HexTile attackerTile;
    private HexTile targetTile;

    private List<HexTile> validTargets = new List<HexTile>();
    private bool isAwaitingTargetClick = false;

    public static BattleManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void InitiateBattle(HexTile selectedTile)
    {
        attackerTile = selectedTile;
        attackerTile.SetAttackMode(true);
        FindTargetsInRange(attackerTile);

        if (validTargets.Count == 0)
        {
            Debug.Log("No valid targets in range.");
            attackerTile.SetAttackMode(false);
            return;
        }

        HighlightTargets(true);
        isAwaitingTargetClick = true;
        Debug.Log("Click on a highlighted tile to attack.");
    }

    public void OnTileClicked(HexTile clickedTile)
    {
        if (!isAwaitingTargetClick) return;

        if (validTargets.Contains(clickedTile))
        {
            targetTile = clickedTile;
            isAwaitingTargetClick = false;

            HighlightTargets(false);
            ShowBattlePanel();
            UpdateButtonStates();
        }
        else
        {
            Debug.Log("Clicked tile is not a valid target. Cancelling.");
            HighlightTargets(false);
            isAwaitingTargetClick = false;
            attackerTile.SetAttackMode(false);
        }
    }

    void ShowBattlePanel()
    {
        if (battlePanelInstance == null)
        {
            battlePanelInstance = Instantiate(battlePanelPrefab, FindObjectOfType<Canvas>().transform);

            meleeButton = battlePanelInstance.transform.Find("MeleeButton").GetComponent<Button>();
            rangedButton = battlePanelInstance.transform.Find("RangedButton").GetComponent<Button>();
            spellsButton = battlePanelInstance.transform.Find("SpellsButton").GetComponent<Button>();

            meleeButton.onClick.AddListener(() => ExecuteAttack("Melee"));
            rangedButton.onClick.AddListener(() => ExecuteAttack("Ranged"));
            spellsButton.onClick.AddListener(() => ExecuteAttack("Spells"));
        }

        battlePanelInstance.SetActive(true);
    }

    void UpdateButtonStates()
    {
        int distance = CalculateHexDistance(attackerTile, targetTile);

        meleeButton.interactable = distance == 1;
        rangedButton.interactable = distance > 1;
        spellsButton.interactable = true;
    }

    void ExecuteAttack(string type)
    {
        Debug.Log($"Executing {type} attack from ({attackerTile.corX},{attackerTile.corY}) to ({targetTile.corX},{targetTile.corY})");
        battlePanelInstance.SetActive(false);
        attackerTile.SetAttackMode(false);

        if (targetTile.hasEnemy) 
        { 
            targetTile.enemyOnTile.Health--;
            Debug.Log("enemy health: " + targetTile.enemyOnTile.Health);
        }


        if (targetTile.characterInstanceOnThisTile)
        {
            var player = targetTile.characterInstanceOnThisTile.GetComponent<Player>();
            player.hp--;
            Debug.Log("player health: " + player.hp);
        }
    }

    void FindTargetsInRange(HexTile fromTile)
    {
        validTargets.Clear();

        foreach (HexTile tile in HexTile.allTiles)
        {
            if (tile == fromTile) continue;

            int distance = CalculateHexDistance(fromTile, tile);

            if (HasCharacter(tile) && distance <= 5)
            {
                validTargets.Add(tile);
            }
        }
    }

    bool HasCharacter(HexTile tile)
    {
        return tile.hasEnemy || tile.characterInstanceOnThisTile != null;
    }

    void HighlightTargets(bool state)
    {
        foreach (HexTile tile in validTargets)
        {
            tile.HighlightAsTarget(state);
        }
    }

    int CalculateHexDistance(HexTile a, HexTile b)
    {
        Vector3Int start = OddqToCube(new Vector2Int(a.corX, a.corY));
        Vector3Int end = OddqToCube(new Vector2Int(b.corX, b.corY));
        return Mathf.Max(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y), Mathf.Abs(start.z - end.z));
    }

    private Vector3Int OddqToCube(Vector2Int hex)
    {
        int q = hex.x;
        int r = hex.y - (hex.x - (hex.x >> 1)) / 2;
        return new Vector3Int(q, r, -q - r);
    }
}