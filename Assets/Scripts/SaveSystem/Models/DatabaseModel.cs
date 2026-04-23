using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using SaveSystem.Character;
using UnityEngine;

/*
 * Models in this folder are being used as wrappers, since they wrap their data and config value in one place
 * to be accessed more easily through the various components.
 * 
 * - Stores databasedata and databaseconfig pointers in one place
 * - Operates changes to the database saved data and instantiates save data if it does not exist
 * - CharacterCatalogue contains a dictionary<CharacterID, CharacterModel> 
 * - Dictionary was created not only to access characterModels easier but also to instantiate each characterModel as soon as database is instantiated
 * - CharacterModel is instatiated with its own config and data (coming from configlist and data list)
 * 
 */


public class DatabaseModel
{
    // Pointers to data and config values, they should only be assigned on construction
    public DatabaseConfig Config { get; } // using this instead of private readonly to avoid writing a getter
    public DatabaseData Data { get; }
    
    
    
    // Instantiated dictionary of Characters
    public Dictionary<CharacterID, CharacterModel> CharacterCatalogue { get; private set; }

    public DatabaseModel(DatabaseConfig config, DatabaseData data)
    {
        Config = config;
        Data = data;
        
        SetDatabase(config.CharacterDatabase, data.CharacterDatabase);
        SetActiveCharacter(Data.activeCharacterId); // it is never null and set as defualt alredy
    }

    // Creates a dictionary of instantiated characterModels
    private void SetDatabase(List<CharacterConfig> configList, List<CharacterData> dataList)
    {
        CharacterCatalogue = new Dictionary<CharacterID, CharacterModel>();
        foreach (var characterConfig in configList)
        {
            // Stores save data or the characterID that is in the config list
            CharacterData _data = dataList.Find(data => data.characterID == characterConfig.danceStyle);
            // If we have no save data for the characterID, we have to create one (this helps when adding new charcater configs during development)
            if (_data == null)
            {
                _data = new CharacterData { characterID = characterConfig.danceStyle }; // we just assign the ID as the rest should be 0 (unless special cases)
                dataList.Add(_data); // Add it to the main list so it gets saved later
            }
            // Instantiates a new character from characterData and characterConfig lists
            CharacterModel characterModel = new CharacterModel(characterConfig, _data);
            CharacterCatalogue.Add(characterConfig.danceStyle, characterModel);
        }
    }

    public void SetActiveCharacter(CharacterID id)
    {
        Data.activeCharacterId = id;
        // adding unlocks without considering player wallet
        CharacterModel activeCharacter = GetCharacter(id);
        activeCharacter.Unlock();
    }
    
    // returns data of a character model
    public CharacterModel GetCharacter(CharacterID id)
    {
        if (CharacterCatalogue.TryGetValue(id, out CharacterModel model))
            return model;
        
        Debug.LogError($"Character {id} not found in CharacterCatalogue!");
        return null;
    }

    public CharacterModel GetActiveCharacter()
    {
        CharacterID activeCharacterId = Data.activeCharacterId;
        if (CharacterCatalogue.TryGetValue(activeCharacterId, out CharacterModel model))
            return model;
        
        Debug.LogError($"Character {activeCharacterId} not found in CharacterCatalogue!");
        return null;
    }
}