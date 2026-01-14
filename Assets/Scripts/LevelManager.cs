using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl; // Important for UI
using Unity.VisualScripting;
using System.Collections;
using UnityEditorInternal;


/*
    Level: keeps track of what is required at every level.
*/

public class LevelManager : MonoBehaviour
{
    private float levelScore = 0f; // starting point of likes, is always 0 unless some special bonus carried over from previous level or booster
    private float levelTarget = 100f; // this should become higher at every level
    private int videoCompleted = 0;

    private bool levelWon = false;
    // private bool pausePlay = false;
    // float transitionTime = 0.5f;
    Color originalColor;
    Color warningColor = Color.red;
    Color successColor = Color.green;

    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private LikesBarUI likesBar;
    [SerializeField] private Timer levelTimer;   // 👈 ADD THIS

    void Start()
    {
        Debug.Log("Level Started! Target Likes: " + levelTarget);
        likesBar.SetStart(levelScore, levelTarget);
        // levelTimer.ResetTimer();
    }

    void Update()
    {
        if (likesBar.score >= levelTarget && levelTimer.GetTimeLeft() > 0)
        {
            levelWon = true;
            //pause for top level transition, i might just use it when doin a big
            // transition with animation when reached x amount of levels


            //Might use the flag change only in conditionals and encapsulate the rest outside somewhere else
            videoCompleted++;
            Debug.Log("📼 Reel published on time with " + levelTimer.GetTimeLeft() + "seconds left and " + likesBar.score + " likes total!");
            levelNumber.text = videoCompleted.ToString();
            // likesBar.SetStart(0f, levelTarget);
            likesBar.ResetScore();
            levelTimer.ResetTimer(levelWon);
            Debug.Log("🎬 New reel started. In this " + videoCompleted + "th reel, you have " + levelTimer.timeAvailable + " seconds to get " + levelTarget + " likes!");
            Debug.Log("Timer of new level: " + levelTimer.GetTimeLeft());

        }
        else if (likesBar.score < levelTarget && levelTimer.GetTimeLeft() <= 0)
        {
            Debug.Log("⏰ Time's up! You had " + (levelTarget - likesBar.score) + " likes left to complete another reel!");
            likesBar.ResetScore();
            Debug.Log("Total reels made: " + videoCompleted);

            // Optionally, you can implement level reset or game over logic here
        }
    }

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