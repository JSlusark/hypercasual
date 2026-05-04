using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

/*
 * NOTE: remember to always initialize reference types or they will be null. Value types if not initialize will default to 0 or false.
 */

[Serializable] // tells Unity it can convert this class to JSON
public class CharacterData
{
    [Header("Character Data")]
    public CharacterID id; // is overwritten with the CharacterConfig characterID when CatalogueModel creates its CatalogueConfig Dictionary and constructs each character model in a loop based on databaseconfig 
    
    [Header("Character Data")]
    public int experienceLevel; 
    public float followers; 
    public bool isUnlocked;
}
