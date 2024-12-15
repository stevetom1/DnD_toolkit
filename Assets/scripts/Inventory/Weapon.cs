using UnityEngine;

[CreateAssetMenu(fileName = "new weapon", menuName = "inventory/weapon")]
public class Weapon : SOItems
{
    public DamageType damageType;
    public DamageRoll damageRoll;
    public WeaponType weaponType;

    public int Reach;
    public int maxRange;
    public int Range;

    public DamageRoll twoHandDamageRoll;

    public bool twoHanded;
    public bool light;
    public bool isLoadable;
    public bool isVersatile;
    public bool heavy;
    public bool Finesse;
}
