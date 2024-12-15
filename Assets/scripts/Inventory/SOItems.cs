using Unity.VisualScripting;
using UnityEngine;

public abstract class SOItems : ScriptableObject
{
    public string itemName;
    public float itemPrice;
    public int itemAmount;
    public Sprite itemImage;

    public ItemType itemType;
    public SlotType slotType;
}

public enum SlotType { Cloak, Necklace, Ring, Armor, RightHand, LeftHand, Belt, Boots }
public enum ItemType { Weapon, Armor, Consumable, Trinket, Instrument, None }
public enum DamageType { Acid, Bludgeoning, Cold, Fire, Force, Lightning, Necrotic, Piercing, Poison, Psychic, Radiant, Slashing, Thunder }
public enum WeaponType { Melee, Ranged, Throwing }
public enum ArmorType { Light, Medium, Heavy, Shield }
[System.Serializable] public class DamageRoll
{
    public int dice;
    public int roll;
}