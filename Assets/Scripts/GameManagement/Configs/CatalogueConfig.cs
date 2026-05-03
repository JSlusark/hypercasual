using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "CharacterList", menuName = "ScriptableObject/CharacterList")]
public class CatalogueConfig : ScriptableObject
{
    [SerializeField] public List<CharacterConfig> catalogueConfig;

}