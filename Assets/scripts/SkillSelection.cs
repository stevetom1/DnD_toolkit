using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SkillSelection : MonoBehaviour
{
    public TextMeshProUGUI characterClassText;
    public string characterClass;

    public List<Toggle> allSkills;
    public List<Toggle> barbarSkills;

    public int maxSkillSelection = 2;

    private int selectedSkillsCount = 0;

    void Start()
    {
        foreach (Toggle toggle in allSkills)
        {
            toggle.interactable = false;
            toggle.isOn = false;
            toggle.onValueChanged.AddListener(delegate { OnSkillToggleChanged(toggle); });
        }

        OnClassSelectionChanged();
    }

    public void OnClassSelectionChanged()
    {
        characterClass = characterClassText.text;

        if (characterClass == "Barbar")
        {
            Debug.Log("Barbar selected. Enabling Barbar skills.");
            EnableBarbarSkills(true);
        }
        else
        {
            Debug.Log("Non-Barbar class selected. Disabling all skills.");
            EnableBarbarSkills(false);
        }
    }

    void EnableBarbarSkills(bool isEnabled)
    {
        selectedSkillsCount = 0;

        foreach (Toggle toggle in allSkills)
        {
            if (barbarSkills.Contains(toggle))
            {
                toggle.interactable = isEnabled;
                toggle.isOn = false;
                Debug.Log("Setting " + toggle.name + " interactable: " + isEnabled);
            }
            else
            {
                toggle.interactable = false;
                toggle.isOn = false;
            }
        }
    }

    void OnSkillToggleChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            selectedSkillsCount++;
            if (selectedSkillsCount > maxSkillSelection)
            {
                changedToggle.isOn = false;
                selectedSkillsCount--;
                Debug.Log("Skill limit exceeded. Deselecting " + changedToggle.name);
            }
        }
        else
        {
            selectedSkillsCount--;
        }
    }
}
