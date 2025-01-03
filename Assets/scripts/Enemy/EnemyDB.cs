using SQLite;
using UnityEngine;

public class Enemy
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public string Size { get; set; }

}

public class EnemyDB : MonoBehaviour
{
    void Start()
    {
        // 1. Create a connection to the database.
        // The special ":memory:" in-memory database and
        var db = new SQLiteConnection($"{Application.persistentDataPath}/MyDb.db");

        // 2. Once you have defined your entity, you can automatically
        // generate tables in your database by calling CreateTable
        db.CreateTable<Enemy>();

        // 3. You can insert rows in the database using Insert
        // The Insert call fills Id, which is marked with [AutoIncremented]
        var newEnemy = new Enemy
        {
            Name = "Ghoul",
            MaxHealth = 22,

        };
        db.Insert(newEnemy);
        Debug.Log($"Enemy new ID: {newEnemy.Id}");

        var query = db.Table<Enemy>().Where(p => p.Name.StartsWith("g"));
        foreach (Enemy enemy in query)
        {
            Debug.Log($"Found enemy named {enemy.Name} with ID {enemy.Id}");
        }

        // 4.b You can also make queries at a low-level using the Query method
        var enemies = db.Query<Enemy>("SELECT * FROM Enemy WHERE Id = ?", 1);
        foreach (Enemy enemy in enemies)
        {
            Debug.Log($"Enemies with ID 1 is called {enemy.Name}");
        }

        // 5. You can perform low-level updates to the database using the Execute
        // method, for example for running PRAGMAs or VACUUM
        db.Execute("VACUUM");
    }
}
