using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

public class ConfigManager : Manager<ConfigManager>
{
    [SerializeField] private CatalogueConfig characterCatalogue;


    public CatalogueConfig CharacterCatalogue => characterCatalogue;



}