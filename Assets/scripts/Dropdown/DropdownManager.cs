using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button addButton;
    public Button removeButton;
    public TextMeshProUGUI listDisplay;
    public List<DamageType> damageType = new List<DamageType>();
    void Start()
    {
        string componentName = dropdown.GetType().Name;
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(System.Enum.GetNames(typeof(DamageType))));
        addButton.onClick.AddListener(AddItem);
        removeButton.onClick.AddListener(RemoveItem);
    }

    void AddItem()
    {
        int index = dropdown.value;
        DamageType selectedItem = (DamageType)index;
        if (!damageType.Contains(selectedItem))
        {
            damageType.Add(selectedItem);
            UpdateListDisplay();
        }
    }

    void RemoveItem()
    {
        int index = dropdown.value;
        DamageType selectedItem = (DamageType)index;
        if(damageType.Contains(selectedItem))
        {
            damageType.Remove(selectedItem);
            UpdateListDisplay();
        }
    }

    void UpdateListDisplay()
    {
        listDisplay.text = string.Join(", ",damageType);
    }
}