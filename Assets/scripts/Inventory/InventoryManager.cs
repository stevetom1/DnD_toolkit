using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public SlotItem[] slotItems;
    public EquipedSlot[] equipedSlots;
    public EquipedSlot cloak, necklace, ring, armor, rightHand, leftHand, belt, boots;
    public Player player;

    public  HoldWeapon enumRightHand;
   public  HoldWeapon enumLeftHand;

    private void Start()
    {
        player = GameObject.Find("Character").GetComponent<Player>();
        player.AddToInventory();
    }

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
        if (item != null)
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
                    EquipWeapon(item);
                    break;
                case SlotType.LeftHand:
                    EquipShield(item);
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

    private void EquipWeapon(SOItems item)
    {
        //nastavení co je drženo

        if (!rightHand.isUsed)
        {
            enumRightHand = HoldWeapon.None;
        } else if (((Weapon)rightHand.item).twoHanded) enumRightHand = HoldWeapon.TwoHanded;
        else if (((Weapon)rightHand.item).isVersatile) enumRightHand = HoldWeapon.Versatile;
        else if (((Weapon)rightHand.item).light) enumRightHand = HoldWeapon.Light;
        else enumRightHand = HoldWeapon.Others;

        if (!leftHand.isUsed)
        {
            enumLeftHand = HoldWeapon.None;
        }
        else if (leftHand.item is Weapon)
        {
            if (((Weapon)leftHand.item).twoHanded) enumLeftHand = HoldWeapon.TwoHanded;
            else if (((Weapon)leftHand.item).isVersatile) enumLeftHand = HoldWeapon.Versatile;
            else if (((Weapon)leftHand.item).light) enumLeftHand = HoldWeapon.Light;
            else enumLeftHand = HoldWeapon.Others;
        }
        else enumLeftHand = HoldWeapon.Shield;



        //obouruèní
        if (((Weapon)item).twoHanded)
        {
            if (enumLeftHand == HoldWeapon.TwoHanded || enumLeftHand == HoldWeapon.Versatile) leftHand.RemoveItem();
            leftHand.EquipItem(item);
            rightHand.EquipItem(item);

        //jedna a pùl ruèní
        } else if (((Weapon)item).isVersatile)
        {
            if (enumLeftHand == HoldWeapon.TwoHanded || enumLeftHand == HoldWeapon.Versatile)
            {
                leftHand.RemoveItem();
                leftHand.EquipItem(item);
            }
            else if (enumLeftHand == HoldWeapon.Light || enumLeftHand == HoldWeapon.None) leftHand.EquipItem(item);
            rightHand.EquipItem(item);
        }
        // lehká
        else if (((Weapon)item).light)
        {
            if (enumLeftHand == HoldWeapon.TwoHanded || enumLeftHand == HoldWeapon.Versatile)
            {
                leftHand.RemoveItem();
                rightHand.EquipItem(item);
            }
            else if (enumRightHand == HoldWeapon.Light && enumLeftHand == HoldWeapon.None) leftHand.EquipItem(item);
            else rightHand.EquipItem(item);
        }
        //štít
        else if (((Armor)item).armorType == ArmorType.Shield)
        {
            if (enumLeftHand == HoldWeapon.TwoHanded || enumLeftHand == HoldWeapon.Versatile) rightHand.RemoveItem();
            leftHand.EquipItem(item);
        }
        //ostatní
        else
        {
            if (enumLeftHand == HoldWeapon.TwoHanded || enumLeftHand == HoldWeapon.Versatile || enumLeftHand == HoldWeapon.Light) leftHand.RemoveItem();
            rightHand.EquipItem(item);
        }
    }

    private void EquipShield(SOItems item)
    {
        if (leftHand.item is Weapon)
        {
            if ((((Weapon)leftHand.item).twoHanded) || (((Weapon)leftHand.item).isVersatile))
            {
                rightHand.RemoveItem();
            }
        }
        leftHand.EquipItem(item);
    }
}

public enum HoldWeapon { Light, TwoHanded, Versatile, Shield, Others,None }