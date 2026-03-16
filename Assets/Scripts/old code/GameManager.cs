using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("Game Data")]
    [SerializeField] private CharacterData[] characterList; // set in the inspector
    private string saveFilePath;
    private SaveData saveData; // loads once and saves across scenes, makes sense to keep it stored here
    private CharacterData character;
    private int index = 0; // default index if load fails


    public int Index => index;
    public CharacterData Character => character;
    public CharacterData[] CharacterList => characterList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // Debug.Log($"[GameManager] Created instance: {GetInstanceID()}"); // helps to debug if we have multiple instances


        saveFilePath = Application.persistentDataPath + "/savegame.json";
        CreateSaveData();
        LoadSaveData();
    }


    private void LoadSaveData() // loads only the active character index so that we can load a single character data instead of the whole list
    {
        string json = File.ReadAllText(saveFilePath); // opens and reads the save file
        saveData = JsonUtility.FromJson<SaveData>(json); // deserializes the json data into the SaveData object

        index = 0;
        if (saveData != null && saveData.index >= 0 && saveData.index < characterList.Length)
            index = saveData.index;

        LoadCharacter(index);
        // Debug.Log($"Active character:  index: {index} danceStyle: {character.danceStyle} high score: {character.highScore}");
    }


    private void CreateSaveData() // creates a deep copy of the character list to save in memory as a json file
    {
        if (File.Exists(saveFilePath)) return;

        saveData = new SaveData();
        saveData.index = 0;
        saveData.characterList = new CharacterSaveData[characterList.Length];

        // Deep copy each CharacterData object
        for (int i = 0; i < characterList.Length; i++)
        {
            saveData.characterList[i] = new CharacterSaveData()
            {
                danceStyle = characterList[i].danceStyle,
                isUnlocked = characterList[i].isUnlocked,
                highScore = 0
            };
            // Debug.Log($"Initialized save data: {i}.{characterList[i].danceStyle}");
        }

        SaveToFile();
        Debug.Log($"[GameManager] Save file created at {saveFilePath}");
    }


    public void SaveCharacter()
    {
        if (saveData == null || character == null)
        {
            Debug.LogWarning("[GameManager] Cannot save: saveData or character is null");
            return;
        }

        if (index < 0 || index >= saveData.characterList.Length)
        {
            Debug.LogWarning($"[GameManager] Invalid index {index} for saving");
            return;
        }

        saveData.index = index;
        saveData.characterList[index].highScore = character.highScore;
        saveData.characterList[index].isUnlocked = character.isUnlocked;
        saveData.characterList[index].danceStyle = character.danceStyle;

        // Debug.Log($"[GameManager] Saved: {saveData.characterList[index].danceStyle} with score {saveData.characterList[index].highScore} ");
        SaveToFile();
    }


    public void LoadCharacter(int i)
    {
        if (i < 0 || i >= characterList.Length) return;

        index = i;
        character = characterList[i];
        if (saveData != null && saveData.characterList != null)
        {
            character.danceStyle = saveData.characterList[i].danceStyle; // not really needed but for consistency
            character.highScore = saveData.characterList[i].highScore;
            character.isUnlocked = saveData.characterList[i].isUnlocked;
        }
        // Debug.Log($"Character data loaded");
    }

    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
    }


    /*
        auto-save functions can be added  if needed
        private void OnApplicationPause(bool pauseStatus)
        private void OnApplicationQuit()
    */


}

[System.Serializable]
public class SaveData // The global data level
{
    public int index; // active character index
    public CharacterSaveData[] characterList;
}

[System.Serializable]
public class CharacterSaveData // the per-character data
{
    public string danceStyle;
    public bool isUnlocked;
    public int highScore;
}

