using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * NOTE: remember to always initialize reference types or they will be null. Value types if not initialize will default to 0 or false.
 */

[Serializable] // tells Unity it can convert this class to JSON
public class CharacterData
{
    //Character info
    public CharacterID id; // is overwritten with the CharacterConfig characterID when CatalogueModel creates its CatalogueConfig Dictionary and constructs each character model in a loop based on databaseconfig 
    public bool isUnlocked;
    
    //Character Progress
    public int level; 
    public float followers; 
    
    //Character Profile
    public int studioIndex; // in the future the index is meant to dynamically change depending from the experience level of the character
    
    
}
