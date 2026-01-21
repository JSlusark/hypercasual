using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl; // Important for UI
using Unity.VisualScripting;
using System.Collections;
using UnityEditorInternal;
using UnityEngine.SceneManagement; // Needed for restarting


/*
    Level: keeps track of what is required at every level.
*/

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }

    // level data
    public bool levelComplete = false;
    public float pointGain; // to be decreased as level difficulty goes up
    public float pointLoss;

    // global game data
    private int completedLevels;
    public int prevHighScore;
    public int newHighScore;
    public int expPoints;

    // UI components
    Color originalColor;
    public GameObject gameOverPanel;
    [SerializeField] private ScoreBar ScoreBar;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreMessageUI;




    private void Awake() // Awake is use to initialize any variables or game state before Start()
    {
        if (Instance != null && Instance != this) // Ensures only one instance exists by killing duplicates
            Destroy(this);
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Optional: Persist through scenes
        }
    }

    private void OnDestroy()
    {
        // important for scene singletons, clears the static reference to avoid issues
        // when other scenes are trying to access it
        if (Instance == this)
            Instance = null;
    }


    /// ___ Level Management ___ ///
    void Start()
    {
        prevHighScore = PlayerPrefs.GetInt("HighScore", 0); // loads saved high score
        highScoreUI.text = prevHighScore.ToString();

        // Debug.Log($"High Score: {highScore}");
        ScoreBar.SetStart();
    }

    void Update()
    {
        if (ScoreBar.MaxScoreReached() && !timer.IsTimeup())
        {
            completedLevels++;
            levelNumber.text = completedLevels.ToString();
            ScoreBar.SetStart();
        }
        else if (!ScoreBar.MaxScoreReached() && timer.IsTimeup())
        {
            string message;
            if (completedLevels > prevHighScore) // to be changed with scoring system later
            {
                // Debug.Log("New High Score!");
                newHighScore = completedLevels;
                message = "You got a new High Score!";
                // simple test with unity's pre-built persistence layer
                PlayerPrefs.SetInt("HighScore", newHighScore);
                PlayerPrefs.Save();
            }
            else
            {
                newHighScore = prevHighScore;
                message = "Try again to beat your High Score";
            }

            highScoreMessageUI.text = message;
            highScoreUI.text = newHighScore.ToString();
            EndLevel();
        }
    }

    public void RetryGame()
    {
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndLevel()
    {
        // Pauses the game
        Time.timeScale = 0f;
        // Shows the Game Over screen
        gameOverPanel.SetActive(true);
    }

    /// ___ Score Management ___ ///
    public void UpdateScore(bool hasScored)
    {
        float point = hasScored ? pointGain : pointLoss;
        ScoreBar.UpdateLength(point);
    }



}