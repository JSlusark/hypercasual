using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class DanceSessionPanelController : PanelController
{
    [SerializeField] private ArrowManager _arrowManager;
    [SerializeField] private ScoreSystemController _scoreSystemController;
    [SerializeField] private TimerController _timerController;

    // private CharacterView _characterSprite; // Might be just a Character Controller later

    public override void Show()
    {
        base.Show();
        GetPanelComponents();
        SubscribeToEvents(true);
    }

    public override void Hide()
    {
        SubscribeToEvents(false);
        base.Hide();
    }

    void GetPanelComponents()
    {
        _arrowManager = PanelInstance.GetComponentInChildren<ArrowManager>();
        _scoreSystemController = PanelInstance.GetComponentInChildren<ScoreSystemController>();
        _timerController = PanelInstance.GetComponentInChildren<TimerController>();
        // _characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();

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
        if (isScored) _scoreSystemController.Refresh();
        StartCoroutine(_timerController.Freeze());
    }

    private void HandleTimerEnd()
    {
        var button = System.Array.Find(PanelEmitterButtons, b => b.panelID == PanelID.DanceSummary);
        button.OnClick();
    }
}