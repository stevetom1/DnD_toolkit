using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject battlePanelPrefab;
    private GameObject battlePanelInstance;

    public Button meleeButton;
    public Button rangedButton;
    public Button spellsButton;

    private HexTile attackerTile;
    private HexTile targetTile;


    public void InitiateBattle(HexTile selectedTile)
    {
        attackerTile = selectedTile;
        targetTile = FindTargetInRange(attackerTile);

        if (targetTile == null)
        {
            Debug.Log("No valid enemy target in range.");
            return;
        }

        ShowBattlePanel();
        UpdateButtonStates();
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
        rangedButton.interactable = distance > 2;
        spellsButton.interactable = true;
    }

    int CalculateHexDistance(HexTile a, HexTile b)
    {
        int dx = b.corX - a.corX;
        int dy = b.corY - a.corY;
        return Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy), Mathf.Abs(dx + dy));
    }

    HexTile FindTargetInRange(HexTile fromTile)
    {
        foreach (HexTile tile in HexTile.allTiles)
        {
            if (tile == fromTile) continue;

            int distance = CalculateHexDistance(fromTile, tile);

            if ((tile.characterInstanceOnThisTile != null || tile.hasEnemy) && distance <= 5)
            {
                return tile;
            }
        }

        return null;
    }

    void ExecuteAttack(string type)
    {
        Debug.Log($"Executing {type} attack from ({attackerTile.corX},{attackerTile.corY}) to ({targetTile.corX},{targetTile.corY})");
        battlePanelInstance.SetActive(false);
    }
}