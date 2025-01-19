using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class EnemyAction : MonoBehaviour
{
    [field: SerializeField] public string ActionName { get; set; }
    public int Reach { get; set; }
    public int Bonus { get; set; }
    public DamageType DamageType { get; set; }
    [field: SerializeField] public int DamageCount { get; set; }
    [field: SerializeField] public int DamageDice { get; set; }
    [field: SerializeField] public int BonusDamage { get; set; }
    public List<EnemyActionEffect> EnemyActionEffect { get; set; }

    public List<EnemyAction> actionsList; 
    public TextMeshProUGUI actionsText;

    public TMP_InputField actionNameInput;
    public TMP_InputField reachInput;
    public TMP_InputField bonusInput;
    public TMP_Dropdown damageTypeDropdown;
    public TMP_InputField damageCountInput;
    public TMP_Dropdown damageDiceDropdown;
    public TMP_InputField bonusDamageInput;


    public void SetEnemyAction()
    {
        EnemyAction enemyAction = gameObject.AddComponent<EnemyAction>();
        enemyAction.ActionName= actionNameInput.text;
        enemyAction.Reach = int.Parse(reachInput.text);
        enemyAction.Bonus = int.Parse(bonusInput.text);
        enemyAction.DamageType = (DamageType)damageTypeDropdown.value;
        enemyAction.DamageCount = int.Parse(damageCountInput.text);
        enemyAction.DamageDice = damageDiceDropdown.value;
        enemyAction.BonusDamage = int.Parse(bonusDamageInput.text);
        actionsList.Add(enemyAction);
        UpdateListDisplay();
    }


    void UpdateListDisplay()
    {
        string actionTextDisplay = "";
        foreach (EnemyAction item in actionsList)
        {
            actionTextDisplay += item.ActionName + ", ";
        }
        actionsText.text = actionTextDisplay;
    }
}

public class EnemyActionEffect
{

}
