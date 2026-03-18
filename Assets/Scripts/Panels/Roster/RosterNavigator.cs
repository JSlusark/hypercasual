using System.Collections.Generic;

/*
 * Model for navigating through a list of items, used in RosterPanelController.
 * Wondering if I should move this to a more general location as it can definitely be used in other
 * context in the game.
 * If so miht call it ListNavigator or something like that.
 */

public class RosterNavigator<T>
{
    private readonly List<T> _items;
    private int _index;
    private int _selectedCharacterIndex;
    
    public RosterNavigator(List<T> items, int startIndex)
    {
        _items = items;
        _index = startIndex;
    }

    public T Select()
    {
        _selectedCharacterIndex = _index;
        return _items[_selectedCharacterIndex];
    }
    
    public T Next()
    {
        _index = (_index + 1) % _items.Count;
        return _items[_index];
    }

    public T Previous()
    {
        _index = (_index - 1 + _items.Count) % _items.Count;
        return _items[_index];
    }
    
    public int SelectedCharacterIndex => _selectedCharacterIndex;
}