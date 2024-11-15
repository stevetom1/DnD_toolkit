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
    public int skillCount;

    public TextMeshProUGUI skillsLeftText;

    public Button nextButtonInteractable;

    void Start()
    {
        foreach (Toggle toggle in allSkills)
        {
            toggle.interactable = false;
            toggle.isOn = false;
            toggle.onValueChanged.AddListener(delegate { OnSkillToggleChanged(toggle); });
        }
    }
    private void Update()
    {
        UpdateNextButtonInteractable();
    }

    public void OnClassSelectionChanged()
    {
        characterClass = characterClassText.text;

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

    void OnSkillToggleChanged(Toggle changedToggle)
    {
        if (!changedToggle.isOn)
        {
            skillCount++;
        }
        else
        {
            skillCount--;
            if (skillCount < 0)
            {
                changedToggle.isOn = false;
            }
        }
        skillsLeftText.text = skillCount.ToString();
    }

    private void chooseSkills(List<Toggle> toggles, int skillsLeft)
    {
        foreach (Toggle toggle in allSkills)
        {
            if (toggles.Contains(toggle))
            {
                toggle.interactable = true;
                toggle.isOn = false;
            }
            else
            {
                toggle.interactable = false;
                toggle.isOn = false;
            }
        }

        skillsLeftText.text = skillsLeft.ToString();
        skillCount = skillsLeft;
    }

    public void Button()
    {
        OnClassSelectionChanged();
    }

    private void UpdateNextButtonInteractable()
    {
        if(skillCount == 0)
        {
            nextButtonInteractable.interactable = true;
        }
        else
        {
            nextButtonInteractable.interactable = false;
        }
    }
}
