using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySessionController : MonoBehaviour
{
    
    // [SerializeField] private GameObject gameplayRoot;
    // [SerializeField] private GameObject lowerBar;

    
    [Header("Level Session Data")]
    private PlaySessionData _sessionData;
    
    [Header("PlaySession Data")]
    [SerializeField] private float pointGain;
    [SerializeField] private float pointLoss;
    // [SerializeField] private int completedRounds = 0;
    [SerializeField] private CharacterData characterData;
    // [SerializeField] private string message;
    [SerializeField] private GameObject gameOverPanel;


    [Header("UI Components")]
    // [SerializeField] private SpriteRenderer characterUI;
    [SerializeField] private Image  characterUI;
    [SerializeField] private ScoreBar ScoreBar;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    // [SerializeField] private TextMeshProUGUI highScoreMessageUI;




    public static PlaySessionController Instance { get; private set; }
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            // Debug.LogWarning($"[PlaySessionController] Duplicate detected! Destroying this instance: {GetInstanceID()}");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        characterData = GameManager.Instance.Character;
        // message = "Try again to beat your High Score";
        // Debug.Log($"[PlaySessionController] Instance set to: {GetInstanceID()}");
    }

    private void Start()
    {
        _sessionData = new PlaySessionData(pointGain, pointLoss, characterData.highScore);
        // Debug.Log($"Player selected {character.danceStyle} with high score {character.highScore}");
        characterUI.sprite = characterData.baseSprite;
        ScoreBar.SetStart();
    }

    private void Update()
    {
        if (ScoreBar.MaxScoreReached() && !timer.IsTimeup())
        {
            _sessionData.CompleteRound();
            levelNumber.text = _sessionData.CompletedRounds.ToString();
            ScoreBar.SetStart();
        }
        else if (!ScoreBar.MaxScoreReached() && timer.IsTimeup())
        {
            bool newHighScore = _sessionData.SetNewHighScore();
            if (newHighScore)
            {
                // message = "New High Score Achieved!";
                characterData.SetNewHighScore(_sessionData.HighScore);
            }
            // highScoreMessageUI.text = _sessionData.HighScore.ToString();
            highScoreUI.text = _sessionData.HighScore.ToString();
            ShowResultScreen();
        }
    }

    public void OnCommandTrigger(bool hasScored)
    {
        float point = _sessionData.MoveResult(hasScored);
        ScoreBar.UpdateLength(point); // TODO: don't like the logic here
    }

    public void ShowResultScreen()
    {
        // Pauses the game
        // GameManager.Instance.SaveCharacter();
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    
}
