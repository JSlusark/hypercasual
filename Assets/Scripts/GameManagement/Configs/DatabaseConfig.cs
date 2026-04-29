using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterList", menuName = "ScriptableObject/CharacterList")]
public class DatabaseConfig : ScriptableObject
{
    public List<CharacterConfig> CharacterDatabase;

}