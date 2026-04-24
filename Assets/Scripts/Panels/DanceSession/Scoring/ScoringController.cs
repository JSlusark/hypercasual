using System;
using UnityEngine;

public class ScoringController : MonoBehaviour
{
    [SerializeField] private LevelView levelView;
    [SerializeField] private ScoreBarView scoreBarView;
    private float _scoreBarFill;
   
    [SerializeField] private Score scoreConfig;
    private ScoringModel _scoringModel;

    
    CharacterModel _character;
    
    // [Header("Dynamic Values: can change from booster applied, character level and/or session level progress")]
    // I expect this data may come from Game Manager and stored in the corresponding Character Data
    // [SerializeField]
    // private float characterLevelValue = 0;
    //
    // [SerializeField] private int rounds = 0;
    // [SerializeField] private float startScoreValue = 0;

    void Awake()
    {
        
        DatabaseModel _data = GameManager.Instance.Database;
        _character = _data.GetActiveCharacter();
        
        _scoringModel = new ScoringModel(scoreConfig, _character);
        _scoreBarFill = _scoringModel.Points / _scoringModel.Target;
        levelView.Show(_scoringModel.Rounds);
    }


    public void Refresh()
    {
        _scoringModel.UpdateScore();
        _scoreBarFill = _scoringModel.Points / _scoringModel.Target;
        scoreBarView.Show(_scoreBarFill);
        levelView.Show(_scoringModel.Rounds);
    }

    private void OnDestroy()
    {
        Debug.Log($"Destroyed scoring controller {_scoringModel.GetTotalPoints()}");
    }
}