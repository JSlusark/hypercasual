using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

/*
 * Contains static immutable data for a character
 * in the character list
 * 
 */


[CreateAssetMenu(fileName = "CharacterList", menuName = "ScriptableObject/CharacterList")]
public class DatabaseConfig : ScriptableObject
{
    public List<CharacterConfig> CharacterDatabase;

}