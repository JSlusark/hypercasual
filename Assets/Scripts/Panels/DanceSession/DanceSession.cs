using UnityEngine;
using UnityEngine.Serialization;

public class DanceSession : MonoBehaviour
{
    [SerializeField] private CharacterView _characterSprite;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private ArrowManager _arrowManager;
    [SerializeField] private TimerController _timerController;
    [SerializeField] private ScoreView scoreView;


    public CharacterView CharacterSprite => _characterSprite;
    public LevelView LevelView => _levelView;
    public ArrowManager ArrowManager => _arrowManager;
    public TimerController TimerController => _timerController;
    public ScoreView ScoreView => scoreView;
}