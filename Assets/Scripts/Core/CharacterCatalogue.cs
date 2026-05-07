using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

public class CharacterCatalogue : Singleton<CharacterCatalogue>
{
    public Dictionary<CharacterID, Character> characterCatalogue { get; private set; }

    // References to Save
    public Character activeCharacter { get; private set; }

    protected override void Initialize()
    {
        Create();
        SetActiveCharacter(SaveSystem.Instance.SaveData.activeCharacterID);
    }

    private void Create()
    {
        var configList = ConfigManager.Instance.CharacterCatalogue;
        var dataList = SaveSystem.Instance.SaveData.characterCatalogue;

        // gets from config manager and saveDta
        characterCatalogue = new Dictionary<CharacterID, Character>();
        foreach (var characterConfig in configList)
        {
            // Finds characterData for matching character config: if it returns null it means that we have no save data of that character so we have to initialize the data. This also helps when adding more character configs later.
            CharacterData _characterData = dataList.Find(data => data.id == characterConfig.id);
            if (_characterData == null)
            {
                // Debug.Log($"Character {characterConfig.id} not found in CatalogueConfig!");
                _characterData = new CharacterData
                                 {
                                     id = characterConfig.id
                                 };           // we just assign the ID as the rest should be 0 (unless special cases)
                dataList.Add(_characterData); // Adds it to the saved list of characters 
            }

            // Instantiates a new character from characterData and characterConfig lists
            Character character = new Character(characterConfig, _characterData);
            // Adding default unlock for moshpit 
            if (characterConfig.id == CharacterID.Moshpit && !character.Data.isUnlocked) character.Unlock();
            characterCatalogue.Add(characterConfig.id, character);
        }
    }

    public bool IsActive(CharacterID id) => activeCharacter?.Data.id == id;

    public void SetActiveCharacter(CharacterID id)
    {
        Character character = GetCharacter(id);
        if (character.Data.isUnlocked)
        {
            activeCharacter = character;
            SaveSystem.Instance.SaveData.activeCharacterID = id;
        }
        else
        {
            var wallet = Wallet.Instance;
            if (wallet.RemoveCoins(character.Config.costToUnlock))
            {
                SaveSystem.Instance.SaveData.activeCharacterID = id;
                activeCharacter = character;
                activeCharacter.Unlock();
            }
            
        }
        
    }

    public Character GetCharacter(CharacterID id)
    {
        if (!characterCatalogue.TryGetValue(id, out Character characterModel))
        {
            // Debug.LogError($"Character {id} not found in _characterCatalogue!");
            return null;
        }

        return characterModel;
    }
}