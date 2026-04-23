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
    private DatabaseModel _data;
    private CharacterID _characterID;

    [SerializeField] private CharacterView _characterSprite; // Might be just a Character Controller later

    public override void Show()
    {
        base.Show();
        // _data = GameManager.Instance.Database;
        // _characterID = _data.Data.activeCharacterId;
        SetPanelComponents();
        SubscribeToEvents(true);
        // _characterSprite.ShowIdle(_data.GetCharacter(_characterID));
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
        _characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();

        // Should be taken from character list model? or saveGamedata?
        // _characterSprite.ShowIdle(GameManager.Instance.SelectedCharacter.idleSprite);
    }


    /*Events*/
    private void SubscribeToEvents(bool isSubscribed)
    {
        if (isSubscribed)
        {
            _timerController.OnTimerEnd += HandleTimerEnd;
            _arrowManager.OnSequenceComplete += HandleScoreChange;
        }
        else
        {
            _timerController.OnTimerEnd -= HandleTimerEnd;
            _arrowManager.OnSequenceComplete -= HandleScoreChange;
        }
    }

    private void HandleScoreChange(bool isScored)
    {
        if (isScored) scoringController.Refresh();
        StartCoroutine(_timerController.Freeze());
    }

    private void HandleTimerEnd()
    {
        var button = System.Array.Find(PanelEmitterButtons, b => b.panelID == PanelID.DanceSummary);
        button.OnClick();
    }
}