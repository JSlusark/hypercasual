using UnityEngine;

public class ScoreSystemController : MonoBehaviour
{
    
    [SerializeField] private LevelView levelView;
    [SerializeField] private ScoreView scoreView;
    

    private ScoreSystemModel _scoreSystemModel;
    // start level and score can change if booster applied -  I expect this data may be store and come from Game Manager
    // Added for now to test values only
    private float _characterLevel = 0;
    private int _startLevel = 0;
    private float _startScore = 0;


    void Awake()
    {
        _scoreSystemModel = new ScoreSystemModel(_characterLevel, _startLevel, _startScore);
        scoreView.Show(_scoreSystemModel.Score);
        levelView.Show(_scoreSystemModel.Level);
    }


    public void Refresh()
    {
        _scoreSystemModel.Update();
        scoreView.Show(_scoreSystemModel.Score);
        levelView.Show(_scoreSystemModel.Level);
    }
}