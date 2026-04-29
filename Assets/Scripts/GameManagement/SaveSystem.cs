using System.IO;
using System.Threading;
using UnityEngine;


/// <summary>
/// * Makes I/O operations to json save:
/// - Reads (Load) SaveData from json.
/// - Writes (Save) SaveData into json.
/// - Creates SaveData if json does not exist
///
/// It is meant to be instantiated and used only from gameManager, nothing else.
/// In a bigger project within a team it would be ideal to make its constructor internal and share the
/// same assembly definition with GameManager. 
/// </summary>

public class SaveSystem
{
    private readonly string _savePath;
    public SaveData SaveData { get; private set; }
    private CancellationTokenSource _timerCancellation;

    public SaveSystem()
    {
        Debug.Log("Starting SaveSystem");
        _savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        Load();
    }

    public void Load()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            SaveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("SaveData Loaded");
        }
        else
        {
            Debug.Log("No save found on device, creating new save file.");
            SaveData = new SaveData();
            Save();
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(SaveData, true);
        File.WriteAllText(_savePath, json);
        Debug.Log("SaveData Saved!");
    }


    // For timer based save fallback (60 seconds), it has to be async or it would block other operations
    // Commented for now as will better check later if mising something

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
    //     Debug.Log("SaveData Saved!");
    // }
}