using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : Manager<GameManager>
{
    [Header("Save Systems")] private SaveSystem _saveSystem;

    [Header("Configurations - Scriptable Objects")] [SerializeField]
    private DatabaseConfig characterListSo;

    [Header("Main Game Models")] public DatabaseModel Database;


    protected override void Awake() // Uses the Instance of the GameManager so that it persists across scenes 
    {
        base.Awake();
        StartSaveSystem();
        InitModels();
        if (Database == null)
        {
            Debug.LogError("Database is null!");
            
        }
    }


    private void StartSaveSystem()
    {
        _saveSystem = new SaveSystem();
    }

    private void InitModels()
    {
        Database = new DatabaseModel(characterListSo, _saveSystem.SaveData.databaseData);
    }


    ///    <summary>
    /// Unity built in methods for to handle when app is paused or quit
    ///- By default, on mobile Pause is always triggered before Quit (so saving needs to be a synchronous operation)
    ///     - This is not the case on Desktop
    ///    </summary>
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused) _saveSystem.Save();
    }

    private void OnApplicationQuit()
    {
        // _timerCancellation.Cancel(); // timerSave needs to be interrupted or it will still be active on quit
        _saveSystem.Save();
    }
}