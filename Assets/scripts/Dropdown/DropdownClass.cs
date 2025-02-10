using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class DropdownClass : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button addButton;
    public Button removeButton;
    public TextMeshProUGUI listDisplay;
    public List<CharacterClass> characterClass = new List<CharacterClass>();
    void Start()
    {
        string componentName = dropdown.GetType().Name;
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(System.Enum.GetNames(typeof(CharacterClass))));
        addButton.onClick.AddListener(AddItem);
        removeButton.onClick.AddListener(RemoveItem);
    }

    void AddItem()
    {
        int index = dropdown.value;
        CharacterClass selectedItem = (CharacterClass)index;
        if (!characterClass.Contains(selectedItem))
        {
            characterClass.Add(selectedItem);
            UpdateListDisplay();
        }
    }

    void RemoveItem()
    {
        int index = dropdown.value;
        CharacterClass selectedItem = (CharacterClass)index;
        if (characterClass.Contains(selectedItem))
        {
            characterClass.Remove(selectedItem);
            UpdateListDisplay();
        }
    }

    void UpdateListDisplay()
    {
        listDisplay.text = string.Join(", ", characterClass);
    }
}

public enum CharacterClass { Barbarian, Bard, Cleric, Druid, Fighter, Monk, Paladin, Ranger, Rogue, Sorcerer, Warlock, Wizard }
