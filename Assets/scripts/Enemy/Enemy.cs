using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name;

    public int maxHp;
    public int Defense;
    public int Danger;

    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;

    public int bonusStrength;
    public int bonusDexterity;
    public int bonusConstitution;
    public int bonusIntelligence;
    public int bonusWisdom;
    public int bonusCharisma;

    public int experience;
    public int numberOfAttacks;
}

public enum Size { Fine, Diminutive, Tiny, Small, Medium, Large, Huge, Gargantuan, Colossal }
public enum Type { Aberration, Beast, Celestial, Construct, Dragon, Elemental, Fey, Fiend, Giant, Humanoid, Monstrosity, Ooze, Plant, Undead }
