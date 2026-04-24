using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

/*
 * - Stores saveData and config pointers for CharacterDatabase
 * - Overwrites the database saveData and instantiates save data if it does not exist
 * - CharacterDictionary contains a dictionary<CharacterID, CharacterModel> 
 * - Dictionary was created not only to access characterModels easier but also to instantiate each characterModel as soon as database is instantiated
 * - CharacterModel is instatiated inside the dictionary and has refs to its own config and data (we keep config in configlist and saveData in dataList)
 * 
 */


public class DatabaseModel
{
    // Pointers to data and config values, they should only be assigned on construction
    public DatabaseConfig Config { get; } // using this instead of private readonly to avoid writing a getter
    public DatabaseData Data { get; }
    
    // Dictionary of instantiated CharacterModels
    public Dictionary<CharacterID, CharacterModel> CharacterCatalogue { get; private set; }

    public DatabaseModel(DatabaseConfig config, DatabaseData data)
    {
        Config = config;
        Data = data;
        
        SetDatabase(config.CharacterDatabase, data.CharacterDatabase);
        SetActiveCharacter(Data.activeCharacterId); // it is never null and set as defualt alredy
    }

    // Creates a dictionary of instantiated characterModels, matches characterConfig field with its corresponding characterData
    private void SetDatabase(List<CharacterConfig> configList, List<CharacterData> dataList)
    {
        CharacterCatalogue = new Dictionary<CharacterID, CharacterModel>();
        foreach (var characterConfig in configList)
        {
            // Finds data for matching character config: if it returns null it means that we have no save data of that character so we have to initialize the data. This also helps when adding more character configs later.
            CharacterData _data = dataList.Find(data => data.characterID == characterConfig.id);
            if (_data == null)
            {
                Debug.Log($"Character {characterConfig.id} not found in CharacterCatalogue!");
                _data = new CharacterData { characterID = characterConfig.id }; // we just assign the ID as the rest should be 0 (unless special cases)
                dataList.Add(_data); // Adds it to the saved list of characters 
            }
            // Instantiates a new character from characterData and characterConfig lists
            CharacterModel characterModel = new CharacterModel(characterConfig, _data);
            CharacterCatalogue.Add(characterConfig.id, characterModel);
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