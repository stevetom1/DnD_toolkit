using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipedSlot : MonoBehaviour, IPointerClickHandler
{
    //item data
    public Sprite itemImage;
    public bool isUsed;


    //slot data
    [SerializeField] private Image slot;
    [SerializeField] private TextMeshProUGUI slotText;
    [SerializeField] public GameObject selectedShader;

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
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
    }
}
