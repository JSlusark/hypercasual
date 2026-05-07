using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoringController : MonoBehaviour
{
    [FormerlySerializedAs("levelView")] [SerializeField]
    private RoundView roundView;

    [SerializeField] private ScoreBarView scoreBarView;
    private float _scoreBarFill;

    [SerializeField] private DanceSession danceSessionConfig;
    private ScoringModel _scoringModel;
    private bool _setCompleted;

    
    // AUDIO: added momentarily here, will have to go to an audio manager at the end
    [Header("DanceMove sound")] [SerializeField]
    private AudioSource audioSource; 


    Character _character;

    void Awake()
    {
        _character = CharacterCatalogue.Instance.activeCharacter;
        _scoringModel = new ScoringModel(danceSessionConfig, _character);
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
        audioSource.PlayOneShot(_character.Config.onSetSuccess);
    }


    public void UpdateScore() // updated from controller only at completed set
    {
        _scoringModel.UpdateScore();
        _scoreBarFill = _scoringModel.Points / _scoringModel.Target;
        scoreBarView.Show(_scoreBarFill);
    }

    private void OnDestroy()
    {
        // Debug.Log($"Destroyed scoring controller {_scoringModel.GetTotalPoints()}");
    }
}