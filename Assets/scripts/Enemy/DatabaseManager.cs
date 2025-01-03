// Definice trid
using SQLite;
using System.Collections.Generic;

public class Monster
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Health { get; set; }
    public string Type { get; set; }
    public List<Attack> Attacks { get; set; }
    public List<Spell> Spells { get; set; }
    public List<Ability> Abilities { get; set; }
}

public class Attack
{
    public int Id { get; set; }
    public int MonsterId { get; set; }
    public string AttackName { get; set; }
    public int Damage { get; set; }
}

public class Spell
{
    public int Id { get; set; }
    public int MonsterId { get; set; }
    public string SpellName { get; set; }
    public int ManaCost { get; set; }
    public string Effect { get; set; }
}

public class Ability
{
    public int Id { get; set; }
    public int MonsterId { get; set; }
    public string AbilityName { get; set; }
    public string Description { get; set; }
}

//Ulozeni a nacitani dat z databaze
public class DatabaseManager
{
    private SQLiteConnection db;

    public DatabaseManager(string dbPath)
    {
        db = new SQLiteConnection(dbPath);
        db.CreateTable<Monster>();
        db.CreateTable<Attack>();
        db.CreateTable<Spell>();
        db.CreateTable<Ability>();
    }

    public void SaveMonster(Monster monster)
    {
        db.Insert(monster);
        foreach (var attack in monster.Attacks)
        {
            attack.MonsterId = monster.Id;
            db.Insert(attack);
        }
        foreach (var spell in monster.Spells)
        {
            spell.MonsterId = monster.Id;
            db.Insert(spell);
        }
        foreach (var ability in monster.Abilities)
        {
            ability.MonsterId = monster.Id;
            db.Insert(ability);
        }
    }

    public List<Monster> GetAllMonsters()
    {
        var monsters = db.Table<Monster>().ToList();
        foreach (var monster in monsters)
        {
            monster.Attacks = db.Table<Attack>().Where(a => a.MonsterId == monster.Id).ToList();
            monster.Spells = db.Table<Spell>().Where(s => s.MonsterId == monster.Id).ToList();
            monster.Abilities = db.Table<Ability>().Where(a => a.MonsterId == monster.Id).ToList();
        }
        return monsters;
    }
}
