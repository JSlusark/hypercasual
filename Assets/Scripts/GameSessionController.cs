using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSessionController : MonoBehaviour
{
    public float pointGain;
    public float pointLoss;

    private int completedRounds;
    public int prevHighScore;
    public int newHighScore;
    public int expPoints;

    [SerializeField] private SpriteRenderer character;
    public GameObject gameOverPanel;
    [SerializeField] private ScoreBar ScoreBar;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreMessageUI;

    private CharacterData selectedCharacter;


    private string message = "Try again to beat your High Score";



    public static GameSessionController Instance { get; private set; }
    private void Awake()
    {
        Debug.Log($"[GameSessionController] Awake called. Current Instance: {(Instance == null ? "null" : "exists")}, This: {GetInstanceID()}");

        if (Instance != null)
        {
            // Debug.LogWarning($"[GameSessionController] Duplicate detected! Destroying this instance: {GetInstanceID()}");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Debug.Log($"[GameSessionController] Instance set to: {GetInstanceID()}");
    }

    private void Start()
    {
        int index = GameManager.Instance.GetCharacterIndex; // cache selected character index
        selectedCharacter = GameManager.Instance.characterList[index];

        Debug.Log($"Player selected {selectedCharacter.danceStyleName} with high score {selectedCharacter.highScore}");
        completedRounds = 0;
        character.sprite = GameManager.Instance.characterList[index].baseSprite;
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
            GameManager.Instance.SaveCharacterScore(completedRounds, ref message);
            highScoreMessageUI.text = message;
            highScoreUI.text = selectedCharacter.highScore.ToString();
            EndLevel();
        }
    }


    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //automatically destroys GameSessionController instance and creates a new one
    }

    public void EndLevel()
    {
        // Pauses the game
        Time.timeScale = 0f;
        // Shows the Game Over screen
        gameOverPanel.SetActive(true);
    }

    public void UpdateScore(bool hasScored)
    {
        float point = hasScored ? pointGain : pointLoss;
        ScoreBar.UpdateLength(point);
    }

}
