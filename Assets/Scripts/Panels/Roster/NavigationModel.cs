using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using SaveSystem.Character;
using UnityEngine;
using UnityEngine.TextCore.Text;

/*
 * 
 * Navigates through the DatabaseModel's config.database (which is always populated)
 * When constructed, it starts from the index of DatabaseModel's data.activeCharacter 
 *
 */

public class NavigationModel
{
    private int _index;
    private List<CharacterConfig> _configs;

    public NavigationModel(DatabaseModel database)
    {
        _configs = database.Config.CharacterDatabase;
        _index = _configs.FindIndex(characterConfig => characterConfig.danceStyle == database.Data.activeCharacterId);
        // Debug.Log($"Active Character: {_configs[_index].danceStyle}");
    }

    public CharacterID Next()
    {
        _index = (_index + 1) % _configs.Count;
        // Debug.Log($"Preview Next: {_configs[_index].danceStyle}");

        return _configs[_index].danceStyle;
    }

    public CharacterID Previous()
    {
        _index = (_index - 1 + _configs.Count) % _configs.Count;
        // Debug.Log($"Preview Prev: {_configs[_index].danceStyle}");

        return _configs[_index].danceStyle;
    }
}