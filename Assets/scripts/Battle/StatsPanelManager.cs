using UnityEngine;
using TMPro;

public class StatsPanelManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI strText;
    public TextMeshProUGUI dexText;
    public TextMeshProUGUI conText;
    public TextMeshProUGUI intText;
    public TextMeshProUGUI wisText;
    public TextMeshProUGUI charText;

    public void DisplayPlayerStats(Player player)
    {
        nameText.text = player.name;
        hpText.text = player.hp.ToString();
        defText.text = player.defense.ToString();
        spdText.text = player.speed.ToString();
        strText.text = player.strength.ToString();
        dexText.text = player.dexterity.ToString();
        conText.text = player.constitution.ToString();
        intText.text = player.intelligence.ToString();
        wisText.text = player.wisdom.ToString();
        charText.text = player.charisma.ToString();
    }

    public void DisplayEnemyStats(Enemy enemy)
    {
        nameText.text = enemy.EnemyName;
        hpText.text = enemy.MaxHp.ToString();
        defText.text = enemy.Defense.ToString();
        spdText.text = enemy.Speed.ToString();
        strText.text = enemy.Strength.ToString();
        dexText.text = enemy.Dexterity.ToString();
        conText.text = enemy.Constitution.ToString();
        intText.text = enemy.Intelligence.ToString();
        wisText.text = enemy.Wisdom.ToString();
        charText.text = enemy.Charisma.ToString();
    }

    public void ClearStats()
    {
        nameText.text = "NAME";
        hpText.text = "0";
        defText.text = "0";
        spdText.text = "0";
        strText.text = "0";
        dexText.text = "0";
        conText.text = "0";
        intText.text = "0";
        wisText.text = "0";
        charText.text = "0";
    }
}