using System;
using DefaultNamespace.ScriptableObjects;

/*
 * Contains dynamic mutable data for a character
 * in the character list
 */

[Serializable] // tells Unity it can convert this class to JSON
public class PlayerData
{
    public string name = "Your manager name here";
    public int coins = 100;
    public CharacterID activeCharacterId;
    
    
}