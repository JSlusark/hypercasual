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
    private float levelScore = 0f; // starting point of likes, is always 0 unless some special bonus carried over from previous level or booster
    private float levelTarget = 100f; // this should become higher at every level
    private int videoCompleted = 0;

    // private bool levelWon = false;
    // private bool pausePlay = false;
    // float transitionTime = 0.5f;
    Color originalColor;

    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private VideoBarUI VideoBar;
    [SerializeField] private Timer timer;   // 👈 ADD THIS
    public GameObject gameOverPanel; // Drag your Panel here in Inspector


    void Start()
    {
        Debug.Log("Level Started! Target Likes: " + levelTarget);
        VideoBar.SetStart(levelScore, levelTarget);
    }

    public void GameOver()
    {
        // 1. Pause the game (optional, stops physics/movement)
        Time.timeScale = 0f;

        // 2. Show the Game Over screen
        gameOverPanel.SetActive(true);
    }

    public void RetryGame()
    {
        // needs to unpause the game before reloading
        // Time.timeScale = 1f;

        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (VideoBar.score >= levelTarget && timer.GetTimeLeft() > 0)
        {
            videoCompleted++;
            levelNumber.text = videoCompleted.ToString();
            VideoBar.ResetScore();
        }
        else if (VideoBar.score < levelTarget && timer.GetTimeLeft() <= 0/*  or 3 mistakes done */)
        {
            VideoBar.ResetScore();
            GameOver();
            /*
            Instead of retry, show game calculations here:
            videos made, followers/exp calculation (based on videos and streaks) and coins earned.
            Then bring user to main menu.
             */
        }
    }

    // pause/play logic when player completes a level and dance animation plays (timer needs to stop during this moment and resume after)
    // private IEnumerator pauseForTransition()
    // {
    //     // yield return new WaitForSeconds(waitTime);
    //     // Reset level logic here
    //     pausePlay = true;
    //     // transition starts here
    //     yield return new WaitForSeconds(transitionTime);// Transition stays still for  some time
    //     // return to previous state
    // }

}