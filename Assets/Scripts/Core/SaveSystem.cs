using System.Collections.Generic;
using System.IO;
using DefaultNamespace.ScriptableObjects;
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
public class SaveSystem : Singleton<SaveSystem>
{
    private string _savePath;
    public SaveData SaveData { get; private set; }
    // private CancellationTokenSource _timerCancellation;

    protected override void Initialize()
    {
        // Debug.Log("Starting SaveSystem");
        _savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        Load();
    }


    public void Load()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            SaveData = JsonUtility.FromJson<SaveData>(json);
            if (IsInvalid(SaveData))
            {
                // Debug.LogWarning("SaveData invalid or corrupted");
                SaveData = GetNewSaveData();
                Save();
            }

            // Debug.Log("Loaded SaveData from Save System");
        }
        else
        {
            // Debug.Log("Creating new save file from Save System, creating new data");
            SaveData = GetNewSaveData();
            Save();
        }
    }

    private bool IsInvalid(SaveData data)
    {
        if (data               == null) return true;
        if (data.characterCatalogue == null) return true;
        if (!System.Enum.IsDefined(typeof(CharacterID), data.activeCharacterID)) return true;
        return false;
    }

    private SaveData GetNewSaveData()
    {
        return new SaveData()
               {
                   activeCharacterID = CharacterID.Moshpit,
                   characterCatalogue = new List<CharacterData>(),
                   wallet = new WalletData { coins = 100, maxCoinsReached = false},
               };
    }


    public void Save()
    {
        string json = JsonUtility.ToJson(SaveData, true);
        // Writing as temp file first and copying to filepath as a fallback for cases where the app is killed mid-write 
        string tempPath = _savePath + ".tmp";
        File.WriteAllText(tempPath, json);
        File.Copy(tempPath, _savePath, overwrite: true);
        File.Delete(tempPath);
        // Debug.Log("Saved SaveData");
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
