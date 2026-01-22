using System;
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
        prevHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreUI.text = prevHighScore.ToString();

        if (character != null && DataLayer.Instance != null && DataLayer.Instance.selectedCharacter.characterSprite != null)
            character.sprite = DataLayer.Instance.selectedCharacter.characterSprite;

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
        Debug.Log($"UPDATE SCORE -  {point}");
        ScoreBar.UpdateLength(point);
    }
}