using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class DropdownStatus : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button addButton;
    public Button removeButton;
    public TextMeshProUGUI listDisplay;
    public List<StatusType> statusType = new List<StatusType>();
    void Start()
    {
        string componentName = dropdown.GetType().Name;
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(System.Enum.GetNames(typeof(StatusType))));
        addButton.onClick.AddListener(AddItem);
        removeButton.onClick.AddListener(RemoveItem);
    }

    void AddItem()
    {
        int index = dropdown.value;
        StatusType selectedItem = (StatusType)index;
        if (!statusType.Contains(selectedItem))
        {
            statusType.Add(selectedItem);
            UpdateListDisplay();
        }
    }

    void RemoveItem()
    {
        int index = dropdown.value;
        StatusType selectedItem = (StatusType)index;
        if (statusType.Contains(selectedItem))
        {
            statusType.Remove(selectedItem);
            UpdateListDisplay();
        }
    }

    void UpdateListDisplay()
    {
        listDisplay.text = string.Join(", ", statusType);
    }
}