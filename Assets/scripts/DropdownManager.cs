using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropdownManager : MonoBehaviour
{
    public Dropdown dropdown;
    public Button addButton;
    public Button removeButton;
    public Text listDisplay;
    private List<ItemType> itemList = new List<ItemType>();
    void Start()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(System.Enum.GetNames(typeof(ItemType))));
        addButton.onClick.AddListener(RemoveItem);
    }

    void AddItem()
    {
        int index = dropdown.value;
        ItemType selectedItem = (ItemType)index;
        itemList.Add(selectedItem);
        UpdateListDisplay();
    }

    void RemoveItem()
    {
        int index = dropdown.value;
        ItemType selectedItem = (ItemType)index;
        if(itemList.Contains(selectedItem))
        {
            itemList.Remove(selectedItem);
            UpdateListDisplay();
        }
    }

    void UpdateListDisplay()
    {
        listDisplay.text = string.Join("\n", itemList);
    }
}
