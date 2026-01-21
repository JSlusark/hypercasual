using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image timerBar;

    [Tooltip("Total time available for gameplay (in seconds)")]
    public float timeMax;
    private float timeRemaining;

    Color originalColor = new Color32(36, 255, 0, 255);
    Color warningColor = new Color32(255, 0, 0, 255);

    void Start()
    {
        Time.timeScale = 1f; // Added thsi because of pause time in gameover - Forces the game to unpause and fixes bug (there could ne a better place to put this line)
        timeRemaining = timeMax;
        timerBar.fillAmount = 1f; //  1f used to see the timeMax filled time bar at start
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
        // Debug.Log($"Timer: {timer} | Fill: {timer / timeMax}");
        float fillRemaining = timeRemaining / timeMax; // Calculates the percentage (0.0 to 1.0). If timer is 10 and max is 20, result is 0.5 (half full)
        timerBar.fillAmount = fillRemaining; // Applies to the UI Image

        if (timeRemaining <= 5f)
            timerBar.color = warningColor;
    }


    public bool IsTimeup()
    {
        return timeRemaining < 0f;
    }

}
