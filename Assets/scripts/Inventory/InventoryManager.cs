using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public SlotItem[] slotItems;
    public EquipedSlot[] equipedSlots;
    public EquipedSlot cloak, necklace, ring, armor, rightHand, leftHand, belt, boots;

    public void DeselectAllSlots()
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            slotItems[i].selectedShader.SetActive(false);
            slotItems[i].isItemSelected = false;
        }

        for (int i = 0; i < equipedSlots.Length; i++)
        {
            equipedSlots[i].selectedShader.SetActive(false);
            equipedSlots[i].isItemSelected = false;
        }
    }

    public void AddItem(SOItems item)
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            if (!slotItems[i].isUsed)
            {
                slotItems[i].AddItem(item);
                return;
            }
        }
    }

    public void EquipItem(SOItems item)
    {
        switch (item.slotType)
        {
            case SlotType.Cloak:
                cloak.EquipItem(item);
                break;
            case SlotType.Necklace:
                necklace.EquipItem(item);
                break;
            case SlotType.Ring:
                ring.EquipItem(item);
                break;
            case SlotType.Armor:
                armor.EquipItem(item);
                break;
            case SlotType.RightHand:
                rightHand.EquipItem(item);
                break;
            case SlotType.LeftHand:
                leftHand.EquipItem(item);
                break;
            case SlotType.Belt:
                belt.EquipItem(item);
                break;
            case SlotType.Boots:
                boots.EquipItem(item);
                break;
        }
    }
}
