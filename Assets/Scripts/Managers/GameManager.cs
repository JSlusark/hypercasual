using SaveSystem.Character;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Later make it inherit from Singleton as TouchManager
 * as T will ensure it's not confused with other singleton type children
 *
 */

public class GameManager : MonoBehaviour
{
    // [SerializeField] private CharacterDatabaseConfig Database;
    // [FormerlySerializedAs("defaultCharacterData")] [SerializeField]
    // private CharacterConfig defaultCharacter;


    // I EITHER INSTANTIATE ONCE HERE OR THEY SHOULD BE SINGLETONS
    public PlayerModel Player;
    public CharacterModel ActiveCharacter;
    public DatabaseModel Database;


    public static GameManager Instance { get; private set; } // Singleton instance shared globally
    // public CharacterConfig SelectedCharacter { get; private set; }


    private void Awake() // Uses the Instance of the GameManager so that it persists across scenes 
    {
        if (Instance != null)
        {
            Debug.Log($"[GameManager] Duplicate found, destroying. Existing ID: {Instance.GetInstanceID()}, This ID: {GetInstanceID()}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SavegameManager _savegameManager = SavegameManager.Instance;
        ConfigManager _configManager = ConfigManager.Instance;
        Database = new DatabaseModel(_configManager.databaseConfig, _savegameManager.SaveData.databaseData);
    }
}