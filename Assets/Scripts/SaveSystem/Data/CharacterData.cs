using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

/*
 * Contains dynamic mutable data for a character
 * in the character list
 */

[Serializable] // tells Unity it can convert this class to JSON
public class CharacterData
{
    [Header("Character Data")]
    
    public CharacterID characterID; 
    
    [Header("Character Data")]
    public int experienceLevel; 
    public float followers; 
    public bool isUnlocked;
}
