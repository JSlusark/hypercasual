using UnityEngine;
using UnityEngine.UI; // Required for the Image component
using System.Collections;

public class Timer : MonoBehaviour
{
    // Change from TextMeshPro to Image
    [SerializeField] Image timerBar;

    public float timeAvailable = 20f; // Total time
    private float timer;
    private bool pauseTimer = false;

    // Settings for the shrinking effect
    private float blinkDuration = 0.2f;

    Color originalColor = Color.white;
    Color blinkColor = new Color32(150, 255, 191, 255);
    Color warningColor = Color.red;
    Color successColor = new Color32(58, 255, 135, 255);

    void Start()
    {
        Time.timeScale = 1f; // Added thsi because of pause time in gameover - Forces the game to unpause and fixes bug (there could ne a better place to put this line)

        // Ensures the bar starts full
        timer = timeAvailable;
        timerBar.fillAmount = 1f; // IMPORTANT: Set this to 1f (full), not 'timeAvailable' (20)
    }

    void Update()
    {
        if (!pauseTimer)
            UpdateTimer();
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;
        // Debug Log to check values
        Debug.Log($"Timer: {timer} | Fill: {timer / timeAvailable}");

        // --- THE MATH FOR THE BAR ---
        // Calculates the percentage (0.0 to 1.0)
        // If timer is 10 and max is 20, result is 0.5 (half full)
        float fillPercentage = timer / timeAvailable;

        // Applies to the UI Image
        timerBar.fillAmount = fillPercentage;

        if (timer <= 5f)
            timerBar.color = warningColor;
    }

    // leaving this in case i want to use it in future
    /*
     public void ResetTimer()
      {
          timer = timeAvailable;
          timerBar.fillAmount = 1f; // Make bar full again
          timerBar.color = originalColor;
      }

//      Blinks bar and then resets timer - could use for special reset effects
      private IEnumerator BlinkAndResetRoutine()
      {
          pauseTimer = true;
          for (int i = 0; i < 2; i++)
          {
              timerBar.color = blinkColor;
              yield return new WaitForSeconds(blinkDuration);
              timerBar.color = successColor;
              yield return new WaitForSeconds(blinkDuration);
          }
          ResetTimer(); // Use the standard reset
          pauseTimer = false;
      } */

    // Getters
    public float GetTimeLeft()
    {
        return timer;
    }

    private void stoptimer()
    {


    }



}
