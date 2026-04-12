using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class DanceSessionPanelController : PanelController
{
    private ArrowManager _arrowManager;
    private ScoringController scoringController;
    private TimerController _timerController;

    [SerializeField] private CharacterView _characterSprite; // Might be just a Character Controller later

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
        scoringController = PanelInstance.GetComponentInChildren<ScoringController>();
        _timerController = PanelInstance.GetComponentInChildren<TimerController>();
        _characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();

        _characterSprite.ShowIdle(GameManager.Instance.SelectedCharacter.idleSprite);
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