using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersDatabaseO", menuName = "DancerGame/CharactersDatabase")]
public class CharactersDatabase : ScriptableObject
{
    public List<CharacterData> characters;
}