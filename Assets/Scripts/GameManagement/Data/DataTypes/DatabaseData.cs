using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;

/*
 * NOTE: remember to always initialize reference types or they will be null. Value types if not initialize will default to 0 or false.
 */


[Serializable]
public class DatabaseData
{
    public CharacterID activeCharacterId; // <----------- might move this to another class like Player class or Settings class?
    public List<CharacterData> CharacterDatabase =  new List<CharacterData>(); // is loaded from the json, if empty it is filled when starting character model and setting up the dictionary

}