using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class DanceSessionPanelController : PanelController
{
    [SerializeField] private int _startlevel;
    [SerializeField] private float _availableTime;
    [SerializeField] private float _startScore;
    [SerializeField] private float _characterExperience;

    private DanceSessionModel _danceSessionModel;
    private TimerController _timerController;
    private ScoreView _scoreView;
    private CharacterView _characterSprite;
    private LevelView _levelView;
    private ArrowManager _arrowManager;


    private DanceSession _fields;


    public override void Show()
    {
        base.Show();
        _danceSessionModel = new DanceSessionModel(_startlevel, _availableTime, _startScore, _characterExperience);
        GetPanelComponents();
        SubscribeToEvents(true);
        LoadComponentViews();
        
    }
    public override void Hide()
    {
        SubscribeToEvents(false);
        base.Hide();
    }

    void GetPanelComponents()
    {
        _fields = PanelInstance.GetComponent<DanceSession>();
        _timerController = _fields.TimerController;
        _scoreView = _fields.ScoreView;
        _characterSprite = _fields.CharacterSprite;
        _levelView = _fields.LevelView;
        _arrowManager = _fields.ArrowManager;
    }

    void LoadComponentViews()
    {
        _characterSprite.ShowIdle(GameManager.Instance.SelectedCharacter.idleSprite);
        _timerController.Initialize(_danceSessionModel.TimeAvailable);
        _scoreView.Show(_danceSessionModel.Score);
        _levelView.Show(_danceSessionModel.Level);
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
    
    private void HandleScoreChange( bool isScored)
    {
        if (isScored)
        {
            _danceSessionModel.SetScore();
            _danceSessionModel.UpdateLevel();
            _scoreView.Show(_danceSessionModel.Score);
            _levelView.Show(_danceSessionModel.Level);
        }
        StartCoroutine(_timerController.Freeze());
    }
    
    private void HandleTimerEnd()
    {
        var button = System.Array.Find(PanelEmitterButtons, b => b.panelID == PanelID.DanceSummary);
        button.OnClick();
    }
}