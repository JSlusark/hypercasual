using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    public bool levelComplete = false;
    public float pointGain;
    public float pointLoss;

    private int completedLevels;
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

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        int index = DataLayer.Instance.GetCharacterIndex; // cache selected character index
        selectedCharacter = DataLayer.Instance.characterList[index];

        Debug.Log($"Player selected {selectedCharacter.danceStyleName} with high score {selectedCharacter.highScore}");
        completedLevels = 0;
        character.sprite = DataLayer.Instance.characterList[index].baseSprite;
        ScoreBar.SetStart();
    }

    private void Update()
    {
        if (ScoreBar.MaxScoreReached() && !timer.IsTimeup())
        {
            completedLevels++;
            levelNumber.text = completedLevels.ToString();
            ScoreBar.SetStart();
        }
        else if (!ScoreBar.MaxScoreReached() && timer.IsTimeup())
        {
            DataLayer.Instance.SaveCharacterScore(completedLevels, ref message);
            highScoreMessageUI.text = message;
            highScoreUI.text = selectedCharacter.highScore.ToString();
            EndLevel();
        }
    }


    public void RetryGame()
    {
        // Time.timeScale = 1f; // important, since EndLevel() pauses time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
