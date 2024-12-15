using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipedSlot : MonoBehaviour, IPointerClickHandler
{
    //item data
    public SOItems item;
    public Sprite itemImage;
    public bool isUsed;


    //slot data
    [SerializeField] private Image imageSlot;
    [SerializeField] private TextMeshProUGUI slotText;
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
            //inventoryManager.EquipItem(item);
            //RemoveItem();
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            isItemSelected = true;
        }
    }

    public void EquipItem(SOItems item)
    {
        if (isUsed)
        {
            inventoryManager.AddItem(this.item);
        }
        this.item = item;
        itemImage = item.itemImage;
        isUsed = true;
        imageSlot.sprite = itemImage;
        slotText.enabled = false;
    }
    public void RemoveItem()
    {
        this.item = null;
        itemImage = emptySprite;
        isUsed = false;
        imageSlot.sprite = itemImage;
        slotText.enabled = true;
    }
}
