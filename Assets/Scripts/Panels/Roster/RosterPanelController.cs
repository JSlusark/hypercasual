using System;
using Unity.VisualScripting;
using UnityEngine;


/*
 *
 * RosterPanelController instantiates the RosterPanel prefab, manages DanceSession components of the prefab and listens to events
 *
 */

public class RosterPanelController : PanelController
{
    private DancerCard dancerCard;
    private RosterButtons[] buttons;
    private RosterNavigator<CharacterData> _navigator;
    private int _cardIndex = 0;
    
    public override void Show()
    {
        base.Show();
        _navigator = new RosterNavigator<CharacterData>(GameManager.Instance.CharactersDatabase.characters, _cardIndex);
        dancerCard = PanelInstance.GetComponentInChildren<DancerCard>();
        buttons = PanelInstance.GetComponentsInChildren<RosterButtons>();

        foreach (var button in buttons)
            button.OnRequestTrigger += HandleButtonRequest;
        
        dancerCard.ShowCharacter(_navigator.Select());
    }

    public override void Hide()
    {
        foreach (var button in buttons)
            button.OnRequestTrigger -= HandleButtonRequest;
        _cardIndex = _navigator.SelectedCharacterIndex; // saves index of the selected character to show as card when  reloading the roster DanceSession
        base.Hide();
    }

    private void HandleButtonRequest(RosterButtons.Request request) // moved list navigation logic in its own model class
    {
        switch (request)
        {
            case RosterButtons.Request.ShowPrevious:
                dancerCard.ShowCharacter(_navigator.Previous());
                break;
            case RosterButtons.Request.ShowNext:
                dancerCard.ShowCharacter(_navigator.Next());
                break;
            case RosterButtons.Request.SelectCharacter:
                GameManager.Instance.SetSelectedCharacter(_navigator.Select());
                break;
        }
    }
}