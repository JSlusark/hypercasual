using System.Runtime.Serialization;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    public float timeAvailable = 20; // seconds
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = timeAvailable;
        timerText.text = $"{00:00}:{timeAvailable:00}";
    }
    public void ResetTimer()
    {
        timer = timeAvailable; // seconds
        timerText.text = $"{00:00}:{timeAvailable:00}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        if (seconds <= 0)
            timerText.text = $"{00:00}:{00:00}";
        else
        {
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

    }

    // Getters
    public float GetTimeLeft()
    {
        return timer;
    }

}
