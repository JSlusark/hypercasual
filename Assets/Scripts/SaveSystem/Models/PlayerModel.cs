using SaveSystem;
using SaveSystem.Character;
using UnityEngine;


/*
 *
 * Holds all values related to an object that has either/or:
 * - a save state (data): they are overwritten on save via its methods
 * - a config file: should be readonly
 *
 * It serves as a way to see the properties of an object as  whole
 */


public class PlayerModel
{
    private readonly PlayerData _data;
    

    public PlayerModel(PlayerData data)
    {
        _data = data;
        // GetActiveCharacter();
    }
    
    public string Name
    {
        get => _data.name;
        set
        {
            _data.name = value;
            SavegameManager.Instance.Save();
        }
    }
}