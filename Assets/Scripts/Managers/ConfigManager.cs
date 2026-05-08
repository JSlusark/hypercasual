using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

public class ConfigManager : Manager<ConfigManager>
{
    [SerializeField] public List<CharacterConfig> characterCatalogue;
    [SerializeField] public WalletConfig wallet;
    public List<CharacterConfig> CharacterCatalogue => characterCatalogue;

    protected override void OnAwake()
    {
    }
}