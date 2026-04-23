using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DefaultNamespace.ScriptableObjects;
using SaveSystem;
using UnityEngine;

/*
 *
 * Not an actual manager per se as it just makes I/O operations to json save:
 * - Reads (Load) SaveData from json.
 * - Writes (Save) SaveData into json.
 * - Creates SaveData if json does not exist
 * 
 * SaveData is referenced in its corresponding Model class so it can be overwritten easily based on role
 * 
 */

public class SavegameManager : Manager<SavegameManager>
{
    
    private string _savePath;
    public SavegameData SaveData { get; private set; }
    private CancellationTokenSource _timerCancellation;
    
    protected override void Awake()
    {
        base.Awake();
        _savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        Load();
        // OnTimerSave();
    }

    public void Load()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            SaveData = JsonUtility.FromJson<SavegameData>(json);
            Debug.Log("SavegameData Loaded successfully.");
        }
        else
        {
            Debug.Log("No save found. Creating default save file.");
            SaveData = CreateDefaultSave();
            Save(); 
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(SaveData, true); 
        File.WriteAllText(_savePath, json);
        Debug.Log("SavegameData Saved!");
    }
    

    private SavegameData CreateDefaultSave()
    {
        var newSaveData = new SavegameData();
        return newSaveData;
    }
    
    /*
    Unity built in methods for save OnPause and onExit 
    - By default, on mobile Pause is always triggered before Quit (so saving needs to be a synchronous operation)
    - This is not the case on Desktop
    */
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused) Save(); 
    }

    private void OnApplicationQuit()
    {
        // _timerCancellation.Cancel(); // timerSave needs to be interrupted or it will still be active on quit
        Save(); 
    }
    /*
    For timer based save fallback (60 seconds), it has to be async or it would block other operations
    Commented for now as will better check later if mising something
    */
    // private async void OnTimerSave()
    // {
    //     _timerCancellation = new CancellationTokenSource();
    //     CancellationToken token = _timerCancellation.Token;
    //     
    //     while (!token.IsCancellationRequested)
    //     {
    //         await Task.Delay(60000, token);
    //         await SaveAsync();
    //     }
    // }
    //
    // private async Task SaveAsync()
    // {
    //     string json = JsonUtility.ToJson(SaveData, true);
    //     await File.WriteAllTextAsync(_savePath, json);
    //     Debug.Log("SavegameData Saved!");
    // }
}