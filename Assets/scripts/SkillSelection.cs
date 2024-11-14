using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class SkillSelection : MonoBehaviour
{
    public TextMeshProUGUI characterClassText;
    public string characterClass;

    public List<Toggle> allSkills;
    public List<Toggle> SkillsBarbar;
    public List<Toggle> SkillsBard;
    public List<Toggle> SkillsBojovnik;
    public List<Toggle> SkillsCarodej;
    public List<Toggle> SkillsCernokneznik;
    public List<Toggle> SkillsDruid;
    public List<Toggle> SkillsHranicar;
    public List<Toggle> SkillsKlerik;
    public List<Toggle> SkillsKouzelnik;
    public List<Toggle> SkillsMnich;
    public List<Toggle> SkillsPaladin;
    public List<Toggle> SkillsTulak;

    public int maxSkillSelection;

    private int selectedSkillsCount = 0;

    public TextMeshProUGUI SkillsLeftText;

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
    private void Update()
    {
        //OnClassSelectionChanged();
    }

    public void OnClassSelectionChanged()
    {
        characterClass = characterClassText.text;

        /*if (characterClass == "Barbar")
        {
            maxSkillSelection = 2;
            EnableBarbarSkills(true);
        }
        else if()
        {
            EnableBarbarSkills(false);
        }*/

        switch (characterClass)
        {
            case "Barbar": chooseSkills(SkillsBarbar, 2);
                break;

            case "Bard":
                chooseSkills(SkillsBard, 3);
                break;

            case "Bojovník":
                chooseSkills(SkillsBojovnik, 2);
                break;

            case "Èarodìj":
                chooseSkills(SkillsCarodej, 2);
                break;

            case "Èernoknìžník":
                chooseSkills(SkillsCernokneznik, 2);
                break;

            case "Druid":
                chooseSkills(SkillsDruid, 2);
                break;

            case "Hranicar":
                chooseSkills(SkillsHranicar, 3);
                break;

            case "Klerik":
                chooseSkills(SkillsKlerik, 2);
                break;

            case "Kouzelník":
                chooseSkills(SkillsKouzelnik, 2);
                break;

            case "Mnich":
                chooseSkills(SkillsMnich, 2);
                break;

            case "paladin":
                chooseSkills(SkillsPaladin, 2);
                break;

            case "Tulák":
                chooseSkills(SkillsTulak, 4);
                break;
        }
    }

    void EnableBarbarSkills(bool isEnabled)
    {
        selectedSkillsCount = 0;

        foreach (Toggle toggle in allSkills)
        {
            if (SkillsBarbar.Contains(toggle))
            {
                toggle.interactable = isEnabled;
                toggle.isOn = false;
                //Debug.Log("Setting " + toggle.name + " interactable: " + isEnabled);
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
            }
        }
        else
        {
            selectedSkillsCount--;
        }
    }

    private List<string> chooseSkills(List<Toggle> toggles, int skillsLeft)
    {
        List<string> result = new List<string>();
        return result;
    }
}
