using System;
using DefaultNamespace;
using DefaultNamespace.ScriptableObjects;
using TMPro;
using UnityEngine;

public class PlaySessionController : PanelController
{
    [SerializeField] private ArrowManager _arrowManager;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private TimerController _timerController;
    [SerializeField] private BackgroundView _backgroundView;
    [SerializeField] private CoinSpawnerController _coinSpawnerController;
    [SerializeField] private CharacterView _characterView; // Might be just a Character Controller later


    // AUDIO: added them momentarily here, will have to go to an audio manager at the end
    [Header("DanceMove sound")] [SerializeField]
    private AudioSource scoreAudioSource; // drag the component here

    [SerializeField] private AudioClip audioMoveFail;
    [SerializeField] private AudioClip audioMoveSuccess;


    private Character _character;

    protected override void OnAwake()
    {
        MenuBarManager.Instance.Hide();
    }

    private void Start()
    {
        // _characterView.SetSprite(_activeCharacter.Config.idleSprite);
        _backgroundView.Show(_activeCharacter.Config
                                             .reelBackground[0]); // in the future the index is meant to dynamically change depending from the round level
    }

    /*Events*/
    protected override void SubscribeToEvents(bool isSubscribed)
    {
        if (isSubscribed)
        {
            _timerController.OnTimerEnd += HandleTimerEnd;
            _arrowManager.OnArrowAction += HandleDanceMove;
        }
        else
        {
            _timerController.OnTimerEnd -= HandleTimerEnd;
            _arrowManager.OnArrowAction -= HandleDanceMove;
        }
    }


    // perhaps create a character controller that handles the chatacter view instead
    private void HandleDanceMove(SwipeID move, bool isArrowScored, bool isSetComplete)
    {
        Sprite idleSprite = _activeCharacter.Config.idleSprite;
        Sprite moveSprite = _activeCharacter.Config.onFailSprite;
        AudioClip audioFeedback = audioMoveFail;
        if (isArrowScored) // the default gets overwritten with the success moves
        {
            audioFeedback = audioMoveSuccess;
            if (isSetComplete) scoreController.UpdateScore(); // adds score only at completed set
            switch (move)
            {
                case SwipeID.Up:
                    moveSprite = _activeCharacter.Config.danceMoveSpriteUp;
                    break;
                case SwipeID.Right:
                    moveSprite = _activeCharacter.Config.danceMoveSpriteRight;
                    break;
                case SwipeID.Down:
                    moveSprite = _activeCharacter.Config.danceMoveSpriteDown;
                    break;
                case SwipeID.Left:
                    moveSprite = _activeCharacter.Config.danceMoveSpriteLeft;
                    break;
            }
        }

        scoreAudioSource.PlayOneShot(audioFeedback);


        // pass freeze config for seconds
         _characterView.ShowDanceClip(moveSprite);
        _timerController.Stop();
    }

    private void HandleTimerEnd()
    {
        
        var button = System.Array.Find(PanelEmitterButtons, b => b.panelID == PanelID.PlaySummary);
        button.OnClick();
    }
}