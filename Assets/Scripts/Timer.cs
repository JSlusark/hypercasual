using System.Runtime.Serialization;
using UnityEngine;
using TMPro;
using System.Collections;


public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    /*
     Field: aka variable, a storage location for data that sits in memory  - sraric?
     Property: a smat wrapper arpund a field that provides controlled access to it, uses get and set methods
      you also create thsi when handling data taht does not belong to your class directly
     */
    public float timeAvailable = 20; // seconds
    private float timer;
    private bool pauseTimer = false;
    private float blinkDuration = 0.2f;

    private float originalFontSize = 36f;
    private float blinkFontSize = 37f;


    Color originalColor = Color.white;
    Color blinkColor = new Color32(150, 255, 191, 255);
    Color warningColor = Color.red;
    Color successColor = new Color32(58, 255, 135, 255);
    Color failColor = Color.gray;

    // main methods
    void Start()
    {
        timer = timeAvailable;
        timerText.text = $"{00:00}:{timeAvailable:00}";
    }

    void Update()
    {
        if (!pauseTimer) // also pause commands collection and pay dance animation
            UpdateTimer();
    }

    // updates timer
    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        if (seconds <= 0)
            timerText.text = $"{00:00}:{00:00}";
        else
            timerText.text = $"{minutes:00}:{seconds:00}";

        if (seconds <= 5f)
        {
            // Debug.Log("⏰ Timer is running low: " + seconds + " seconds left!");
            timerText.color = warningColor;
        }
    }

    // resets timer
    public void ResetTimer()
    {
        // if (levelWon)
        // StartCoroutine(BlinkAndResetRoutine()); // should move this on a high level later
        ResetTimerValues();
    }

    // reset timer helpers
    private void ResetTimerValues()
    {
        timer = timeAvailable;
        if (timerText.color != originalColor)
            timerText.color = originalColor;

        // Format to show 00:60 (or whatever timeAvailable is)
        timerText.text = $"00:{timeAvailable:00}";
    }

    private IEnumerator BlinkAndResetRoutine()
    {
        pauseTimer = true;
        for (int i = 0; i < 2; i++)
        {
            timerText.color = blinkColor;
            timerText.fontSize = blinkFontSize;
            yield return new WaitForSeconds(blinkDuration);
            timerText.color = successColor;
            timerText.fontSize = originalFontSize;
            yield return new WaitForSeconds(blinkDuration);
        }
        ResetTimerValues();
        pauseTimer = false;
    }

    // Getters
    public float GetTimeLeft()
    {
        return timer;
    }

    private void stoptimer()
    {


    }



}
