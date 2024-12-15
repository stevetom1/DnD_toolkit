using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotItem : MonoBehaviour, IPointerClickHandler
{
    //item data
    private SOItems item;
    public Sprite itemImage;
    public bool isUsed;


    //slot data
    [SerializeField] private Image imageSlot;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] public GameObject selectedShader;
    [SerializeField] private Sprite emptySprite;
    public bool isItemSelected = false;


    public InventoryManager inventoryManager;
    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) 
        {
            OnLeftClick();
        }
    }

    private void OnLeftClick()
    {
        if (isItemSelected) 
        {
            inventoryManager.EquipItem(item);
            RemoveItem();
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            isItemSelected = true;
        }
    }

    public void AddItem(SOItems item)
    {
        this.item = item;
        itemImage = item.itemImage;
        isUsed = true;
        amountText.text = item.itemAmount.ToString();
        amountText.enabled = true;
        imageSlot.sprite = itemImage;
    }

    public void RemoveItem()
    {
        this.item = null;
        itemImage = emptySprite;
        isUsed = false;
        amountText.enabled = false;
        imageSlot.sprite = itemImage;
    }
}
