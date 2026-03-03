using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSessionController : MonoBehaviour
{
    [Header("GameSession Data")]
    public float pointGain;
    public float pointLoss;
    private int completedRounds = 0;
    private CharacterData character;
    private string message;
    public GameObject gameOverPanel;


    [Header("UI Components")]
    // [SerializeField] private SpriteRenderer characterUI;
    [SerializeField] private Image  characterUI;
    [SerializeField] private ScoreBar ScoreBar;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreMessageUI;




    public static LevelSessionController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            // Debug.LogWarning($"[LevelSessionController] Duplicate detected! Destroying this instance: {GetInstanceID()}");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        character = GameManager.Instance.Character;
        message = "Try again to beat your High Score";
        // Debug.Log($"[LevelSessionController] Instance set to: {GetInstanceID()}");
    }

    private void Start()
    {
        // Debug.Log($"Player selected {character.danceStyle} with high score {character.highScore}");
        characterUI.sprite = character.baseSprite;
        ScoreBar.SetStart();
    }

    private void Update()
    {
        if (ScoreBar.MaxScoreReached() && !timer.IsTimeup())
        {
            completedRounds++;
            levelNumber.text = completedRounds.ToString();
            ScoreBar.SetStart();
        }
        else if (!ScoreBar.MaxScoreReached() && timer.IsTimeup())
        {
            if (completedRounds > character.highScore)
            {
                message = "New High Score Achieved!";
                character.SetNewHighScore(completedRounds);
            }
            highScoreMessageUI.text = message;
            highScoreUI.text = character.highScore.ToString();
            EndLevel();
        }
    }

    public void EndLevel()
    {
        // Pauses the game
        // GameManager.Instance.SaveCharacter();
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //automatically destroys LevelSessionController instance and creates a new one

    }

    public void BackToCharacterSelection()
    {
        Time.timeScale = 0f;
        Debug.Log("Loading Character Selection Scene");
        SceneManager.LoadScene("CharacterSelection");
    }

    public void ChooseAnotherCharacter()
    {
        Debug.Log("Loading Character Selection Scene");
        SceneManager.LoadScene("CharacterSelection");
    }


    public void UpdateScore(bool hasScored)
    {
        float point = hasScored ? pointGain : pointLoss;
        ScoreBar.UpdateLength(point);
    }

}
