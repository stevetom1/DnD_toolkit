/*using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FightButtonHandler : MonoBehaviour
{
    [Header("UI References")]
    public GameObject fightOptionsPanel;
    public Button fightButton;
    public Button meleeButton;
    public Button rangedButton;
    public Button spellButton;

    private HexTile selectedCharacter;

    void Start()
    {
        fightButton.onClick.AddListener(OnFightButtonClicked);
        meleeButton.onClick.AddListener(OnMeleeClicked);
        rangedButton.onClick.AddListener(OnRangedClicked);
        spellButton.onClick.AddListener(OnSpellClicked);

        fightOptionsPanel.SetActive(false);
    }

    public void SetSelectedCharacter(HexTile character)
    {
        selectedCharacter = character;
    }

    void OnFightButtonClicked()
    {
        if (selectedCharacter == null)
        {
            Debug.LogWarning("No character selected for fight.");
            return;
        }

        fightOptionsPanel.SetActive(true);
        UpdateFightOptions();
    }

    void UpdateFightOptions()
    {
        bool isAdjacentToEnemy = CheckForAdjacentEnemy(selectedCharacter);

        meleeButton.interactable = isAdjacentToEnemy;
        rangedButton.interactable = true;
        spellButton.interactable = true;
    }

    bool CheckForAdjacentEnemy(HexTile character)
    {
        foreach (Vector2Int coords in GetHexNeighbors(character.coordinates))
        {
            if (HexTile.allTiles.TryGetValue(coords, out HexTile neighborTile))
            {
                if (neighborTile.hasEnemy)
                    return true;
            }
        }
        return false;
    }

    List<Vector2Int> GetHexNeighbors(Vector2Int coords)
    {
        return new List<Vector2Int>
        {
            coords + new Vector2Int(1, 0),
            coords + new Vector2Int(0, 1),
            coords + new Vector2Int(-1, 1),
            coords + new Vector2Int(-1, 0),
            coords + new Vector2Int(0, -1),
            coords + new Vector2Int(1, -1)
        };
    }

    void OnMeleeClicked()
    {
        HexTile enemyTile = FindAdjacentEnemy(selectedCharacter);
        if (enemyTile != null)
        {
            BattleManager.Instance.StartBattle(selectedCharacter, enemyTile, "Melee");
        }
        else
        {
            Debug.Log("No enemy adjacent for melee attack.");
        }

        fightOptionsPanel.SetActive(false);
    }

    void OnRangedClicked()
    {
        HexTile enemyTile = FindAnyEnemy(); // Simple placeholder: find the first enemy
        if (enemyTile != null)
        {
            BattleManager.Instance.StartBattle(selectedCharacter, enemyTile, "Ranged");
        }
        else
        {
            Debug.Log("No enemy found for ranged attack.");
        }

        fightOptionsPanel.SetActive(false);
    }

    void OnSpellClicked()
    {
        HexTile enemyTile = FindAnyEnemy();
        if (enemyTile != null)
        {
            BattleManager.Instance.StartBattle(selectedCharacter, enemyTile, "Spell");
        }
        else
        {
            Debug.Log("No enemy found for spell attack.");
        }

        fightOptionsPanel.SetActive(false);
    }

    HexTile FindAdjacentEnemy(HexTile character)
    {
        foreach (Vector2Int coords in GetHexNeighbors(character.Coordinates))
        {
            if (HexTile.allHexes.TryGetValue(coords, out HexTile neighborTile))
            {
                if (neighborTile.hasEnemy)
                    return neighborTile;
            }
        }
        return null;
    }

    HexTile FindAnyEnemy()
    {
        foreach (var tile in HexTile.allHexes.Values)
        {
            if (tile.HasEnemy)
                return tile;
        }
        return null;
    }
}*/