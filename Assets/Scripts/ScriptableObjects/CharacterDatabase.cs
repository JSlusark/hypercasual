using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "DancerGame/CharacterDatabase")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> characters;
}