using UnityEngine;

public class ScoringController : MonoBehaviour
{
    [SerializeField] private LevelView levelView;
    [SerializeField] private ScoreBarView scoreBarView;
    private float _scoreBarFill;

    private ScoringModel _scoringModel;

    [Header("Dynamic Values: can change from booster applied, character level and/or session level progress")]
    // I expect this data may come from Game Manager and stored in the corresponding Character Data
    [SerializeField] private float characterLevelValue = 0;
    [SerializeField] private int startLevelValue = 0;
    [SerializeField] private float startScoreValue = 0;
    
    void Awake()
    {
        _scoringModel = new ScoringModel(characterLevelValue, startLevelValue, startScoreValue);
        _scoreBarFill = _scoringModel.LevelPoints /_scoringModel.LevelTarget;
        levelView.Show(_scoringModel.Level);
    }
    

    public void Refresh()
    {
        _scoringModel.Update();
        _scoreBarFill = _scoringModel.LevelPoints /_scoringModel.LevelTarget;
        scoreBarView.Show(_scoreBarFill);
        levelView.Show(_scoringModel.Level);
    }
}