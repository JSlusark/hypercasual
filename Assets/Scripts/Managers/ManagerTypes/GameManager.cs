using UnityEngine;


public class GameManager : Manager<GameManager>
{
    [Header("Game Systems")]
    [SerializeField] private SavegameManager _savegameManager;
    [SerializeField] private ConfigManager _configManager;

    [Header("Game Models")]
    public DatabaseModel Database;



    protected override void Awake() // Uses the Instance of the GameManager so that it persists across scenes 
    {
        base.Awake();

        Database = new DatabaseModel(_configManager.databaseConfig, _savegameManager.SaveData.databaseData);
    }
}