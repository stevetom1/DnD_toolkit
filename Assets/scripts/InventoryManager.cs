using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public SlotItem[] slotItems;
    public EquipedSlot cloak, necklace, ring, armor, rightHand, leftHand, belt, boots;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            slotItems[i].selectedShader.SetActive(false);
        }
    }
}
