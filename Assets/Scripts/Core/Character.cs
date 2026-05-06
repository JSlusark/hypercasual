using UnityEngine;
using DefaultNamespace.ScriptableObjects;

/*
 * Used for characters in the CharacterCatalogue dictionary
 * 
 */

public class Character
{
    public CharacterConfig Config { get; private set; }
    public CharacterData Data { get; private set; }

    public Character(CharacterConfig config, CharacterData data)
    {
        Config = config;
        Data = data;
    }

    public void Unlock()
    {
            Data.isUnlocked = true; 
            // Debug.Log($"{Config.name} has been unlocked!");
    }

    public void UpdateExperience(int rounds, float points)
    {
        // Data.level =  // need to calculate based on exp bar max
        Data.followers += (rounds * points);
        
    }
}