using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;

/*
 * Contains static immutable data for a character
 * in the character list
 * 
 */


[Serializable]
public class DatabaseData 
{
    public CharacterID activeCharacterId;
    public List<CharacterData> CharacterDatabase =  new List<CharacterData>();

}