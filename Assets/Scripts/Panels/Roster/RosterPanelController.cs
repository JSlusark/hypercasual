using System;
using Unity.VisualScripting;
using UnityEngine;


/*
 *
 * RosterPanelController instantiates the RosterPanel prefab, manages View components of the prefab and listens to events
 *
 */

public class RosterPanelController : PanelController
{

    [Header("View Scripts from the prefab")] [SerializeField]
    private DancerCard dancerCard;

    [SerializeField] private RosterButtons[] buttons;
    [SerializeField] private int i;


    // overriding show as we can get the view components and sub to events from the controller only after the prefab is instantiated
    public override void Show() 
    {
        base.Show();
        
        dancerCard = this.PanelInstance.GetComponentInChildren<DancerCard>();
        buttons = this.PanelInstance.GetComponentsInChildren<RosterButtons>();

        foreach (var button in buttons)
            button.OnRequestTrigger += HandleButtonRequest;
        dancerCard.ShowCharacter(GameManager.Instance.CharacterDatabase.characters[i]);
    }

    // overriding hide so taht we can unsub from events only when the prefab is active and avoid null reference errors
    public override void Hide()
    {
        foreach (var button in buttons)
            button.OnRequestTrigger -= HandleButtonRequest;
        base.Hide();
    }

    private void HandleButtonRequest(RosterButtons.Request request)
    {
        var characters = GameManager.Instance.CharacterDatabase.characters;
        switch (request)
        {
            case RosterButtons.Request.ShowPrevious:
                i = (i - 1 + characters.Count) % characters.Count;
                break;
            case RosterButtons.Request.ShowNext:
                i = (i + 1) % characters.Count;
                break;
            // character tracked from gameManager so that it is used correctly in other scenes
            case RosterButtons.Request.SelectCharacter:
                GameManager.Instance.SetSelectedCharacter(characters[i]);
                break;
        }

        dancerCard.ShowCharacter(characters[i]);
    }
}