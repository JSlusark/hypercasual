using System;
using DefaultNamespace;
using DefaultNamespace.ScriptableObjects;
using TMPro;
using UnityEngine;

public class DanceSessionPanelController : PanelController
{
    private ArrowManager _arrowManager;
    private ScoringController scoringController;
    private TimerController _timerController;
    // private DatabaseModel _data;
    // private CharacterID _characterID;

    [SerializeField] private CharacterView _characterView; // Might be just a Character Controller later

    public override void Show()
    {
        base.Show();
        // _data = GameManager.Instance.Database;
        // _characterID = _data.Data.activeCharacterId;
        SetPanelComponents();
        SubscribeToEvents(true);
        
        _characterView.SetSprite(character.IdleSprite);
    }

    public override void Hide()
    {
        SubscribeToEvents(false);
        base.Hide();
    }

    void SetPanelComponents()
    {
        _arrowManager = PanelInstance.GetComponentInChildren<ArrowManager>();
        scoringController = PanelInstance.GetComponentInChildren<ScoringController>();
        _timerController = PanelInstance.GetComponentInChildren<TimerController>();
        _characterView = PanelInstance.GetComponentInChildren<CharacterView>();

        // Should be taken from character list model? or saveGamedata?
        // _characterView.SetSprite(GameManager.Instance.SelectedCharacter.idleSprite);
    }


    /*Events*/
    private void SubscribeToEvents(bool isSubscribed)
    {
        if (isSubscribed)
        {
            _timerController.OnTimerEnd += HandleTimerEnd;
            _arrowManager.OnArrowAction += HandleDanceMoveView;
            // _arrowManager.OnArrowSuccess += HandleDanceMoveView;
        }
        else
        {
            _timerController.OnTimerEnd -= HandleTimerEnd;
            _arrowManager.OnArrowAction -= HandleDanceMoveView;
        }
    }

    // private void HandleDanceMoveView(SwipeID swipeID)
    // {
    //     Sprite moveSprite;
    //     // if (swipeID == SwipeID.Up)
    //         // moveSprite = 
    //         
    //         
    // }

    private void HandleDanceMoveView(SwipeID move, bool isArrowScored, bool isSetComplete)
    {
        Sprite idleSprite = character.IdleSprite;
        Sprite moveSprite = character.OnFailSprite;
        if (isArrowScored) // the default gets overwritten with the success moves
        {
            scoringController.Refresh();
            // if (isSetComplete)
            //     moveSprite = character.OnSetComplete;
            // else
            // {
                switch (move)
                {
                    case SwipeID.Up:
                        moveSprite = character.DanceMoveSpriteUp;
                        break;
                    case SwipeID.Right:
                        moveSprite = character.DanceMoveSpriteRight;
                        break;
                    case SwipeID.Down:
                        moveSprite = character.DanceMoveSpriteDown;
                        break;
                    case SwipeID.Left:
                        moveSprite = character.DanceMoveSpriteLeft;
                        break;
                }
            // }
        }
        _characterView.ShowDanceMove(moveSprite, idleSprite);
        StartCoroutine(_timerController.Freeze());
        // StartCoroutine(_characterView.MoveAnimation());
    }

    private void HandleTimerEnd()
    {
        var button = System.Array.Find(PanelEmitterButtons, b => b.panelID == PanelID.DanceSummary);
        button.OnClick();
    }
}