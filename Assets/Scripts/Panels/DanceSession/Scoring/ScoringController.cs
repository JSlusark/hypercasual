using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoringController : MonoBehaviour
{
    [FormerlySerializedAs("levelView")] [SerializeField]
    private RoundView roundView;

    [SerializeField] private ScoreBarView scoreBarView;
    private float _scoreBarFill;

    [SerializeField] private Score scoreConfig;
    private ScoringModel _scoringModel;
    // public event Action onRoundChange;
    private bool _setCompleted;

    
    // AUDIO: added them momentarily here, will have to go to an audio manager at the end
    [Header("DanceMove sound")] [SerializeField]
    private AudioSource audioSource; // drag the component here
    [SerializeField] private AudioClip audioTriumphant;


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
        roundView.Show(_scoringModel.Rounds);
    }

    private void OnEnable()
    {
        _scoringModel.OnRoundChange += HandleRoundView;
    }

    private void OnDisable()
    {
        _scoringModel.OnRoundChange -= HandleRoundView;
    }

    private void HandleRoundView(int round)
    {
        roundView.UpdateRound(_scoringModel.Rounds);
        audioSource.PlayOneShot(audioTriumphant);
        
        // if round > Beginner emit event for the view to change bg
    }


    public void UpdateScore() // updated from controller only at completed set
    {
        _scoringModel.UpdateScore();
        _scoreBarFill = _scoringModel.Points / _scoringModel.Target;
        scoreBarView.Show(_scoreBarFill);
    }

    private void OnDestroy()
    {
        Debug.Log($"Destroyed scoring controller {_scoringModel.GetTotalPoints()}");
    }
}