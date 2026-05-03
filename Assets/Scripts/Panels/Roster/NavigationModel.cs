using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;

/*
 *
 * Navigates through the CatalogueModel's config.catalogue (which is always populated)
 * When constructed, it starts from the index of CatalogueModel's data.activeCharacter
 *
 */

public class NavigationModel
{
    private int _index;
    private List<CharacterConfig> _list;

    public NavigationModel(List<CharacterConfig> configList)
    {
        _list = configList;
        _index = _list.FindIndex(c => c.id == SaveSystem.Instance.SaveData.activeCharacterID);
        // Debug.Log($"Active Character: {_list[_index].id}");
    }

    public CharacterID Next()
    {
        _index = (_index + 1) % _list.Count;
        // Debug.Log($"Preview Next: {_list[_index].id}");

        return _list[_index].id;
    }

    public CharacterID Previous()
    {
        _index = (_index - 1 + _list.Count) % _list.Count;
        // Debug.Log($"Preview Prev: {_list[_index].id}");

        return _list[_index].id;
    }
}