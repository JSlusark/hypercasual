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

    [SerializeField] private CharacterView _characterView; // Might be just a Character Controller later


    // AUDIO: added them momentarily here, will have to go to an audio manager at the end
    [Header("DanceMove sound")] [SerializeField]
    private AudioSource audioSource; // drag the component here
    [SerializeField] private AudioClip audioMoveFail;
    [SerializeField] private AudioClip audioMoveSuccess;

    
    private Character _character;

    public override void Show()
    {
        base.Show();
        SetPanelComponents();
        SubscribeToEvents(true);


        _character = _characterCatalogue.activeCharacter;
        _characterView.SetSprite(_character.Config.idleSprite);
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
        /*
         * Subscribes to events from other controllers
         * While its controllers subscribe to events from their corresponding models
         */
        if (isSubscribed)
        {
            _timerController.OnTimerEnd += HandleTimerEnd;
            _arrowManager.OnArrowAction += HandleDanceMove;
            // scoringController.onRoundChange += HandleRoundChange;
        }
        else
        {
            _timerController.OnTimerEnd -= HandleTimerEnd;
            _arrowManager.OnArrowAction -= HandleDanceMove;
            // scoringController.onRoundChange -= HandleRoundChange;
        }
    }


    // private void HandleRoundChange()
    // {
    //     Sprite idleSprite = character.IdleSprite;
    //     Sprite transitionSprite = character.OnSetComplete;
    //     _characterView.ShowDanceMove(transitionSprite, idleSprite);
    //     
    // }

    // perhaps create a character controller that handles the chatacter view instead
    private void HandleDanceMove(SwipeID move, bool isArrowScored, bool isSetComplete)
    {
        Sprite idleSprite = _character.Config.idleSprite;
        Sprite moveSprite = _character.Config.onFailSprite;
        AudioClip audioFeedback = audioMoveFail;
        if (isArrowScored) // the default gets overwritten with the success moves
        {
            audioFeedback = audioMoveSuccess;
            if (isSetComplete) scoringController.UpdateScore(); // adds score only at completed set
            switch (move)
            {
                case SwipeID.Up:
                    moveSprite = _character.Config.danceMoveSpriteUp;
                    break;
                case SwipeID.Right:
                    moveSprite = _character.Config.danceMoveSpriteRight;
                    break;
                case SwipeID.Down:
                    moveSprite = _character.Config.danceMoveSpriteDown;
                    break;
                case SwipeID.Left:
                    moveSprite = _character.Config.danceMoveSpriteLeft;
                    break;
            }
        }

        audioSource.PlayOneShot(audioFeedback);


        // pass freeze config for seconds
        _characterView.ShowDanceMove(moveSprite, idleSprite);
        _timerController.Stop();
    }

    private void HandleTimerEnd()
    {
        var button = System.Array.Find(PanelEmitterButtons, b => b.panelID == PanelID.DanceSummary);
        button.OnClick();
    }
}