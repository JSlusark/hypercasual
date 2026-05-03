using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

public class CharacterCatalogue : Singleton<CharacterCatalogue>
{
    public Dictionary<CharacterID, CharacterModel> characterCatalogue { get; private set; }
    public CharacterModel activeCharacter { get; private set; }

    // IReadOnlyList only locks assignment, still need private set to not allow mutations outside
    public List<CharacterConfig> configList { get; private set; }

    protected override void Initialize()
    { 
        // base.Initialize();
        Create();
        SetActiveCharacter(SaveSystem.Instance.SaveData.activeCharacterID);
    }
    
    private void Create()
    {
        configList = ConfigManager.Instance.CharacterCatalogue.catalogueConfig;
        var dataList = SaveSystem.Instance.SaveData.catalogueData;

        // gets from config manager and saveDta
        characterCatalogue = new Dictionary<CharacterID, CharacterModel>();
        foreach (var characterConfig in configList)
        {
            // Finds characterData for matching character config: if it returns null it means that we have no save data of that character so we have to initialize the data. This also helps when adding more character configs later.
            CharacterData _characterData = dataList.Find(data => data.id == characterConfig.id);
            if (_characterData == null)
            {
                // Debug.Log($"Character {characterConfig.id} not found in CatalogueConfig!");
                _characterData = new CharacterData {id = characterConfig.id};  // we just assign the ID as the rest should be 0 (unless special cases)
                dataList.Add(_characterData); // Adds it to the saved list of characters 
            }

            // Instantiates a new character from characterData and characterConfig lists
            CharacterModel characterModel = new CharacterModel(characterConfig, _characterData);
            // Adding default unlock for moshpit 
            if (characterConfig.id == CharacterID.Moshpit && !characterModel.Data.isUnlocked) characterModel.Unlock();
            characterCatalogue.Add(characterConfig.id, characterModel);
        }
    }
    
    public bool IsActive(CharacterID id) => activeCharacter?.Data.id == id;

    public void SetActiveCharacter(CharacterID id)
    {
        activeCharacter = GetCharacter(id);
        SaveSystem.Instance.SaveData.activeCharacterID = id;
        activeCharacter.Unlock(); // momentarily implemented - will change with wallet addition
    }

    public CharacterModel GetCharacter(CharacterID id)
    {
        if (!characterCatalogue.TryGetValue(id, out CharacterModel characterModel))
        {
            Debug.LogError($"Character {id} not found in CharacterCatalogue!");
            return null;
        }
        
        return characterModel;
    }
}