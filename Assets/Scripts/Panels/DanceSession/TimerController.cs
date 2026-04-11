using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float _timer;
    [SerializeField] private bool _isRunning;


    public void Initialize(float timer)
    {
        _isRunning = true;
        _timer = timer;
        // text.text = Mathf.CeilToInt(_timer).ToString();
        // Debug.Log($"Timer start??{_timer}");
        text.text = _timer.ToString();
        
    }


    private void Update()
    {
        if (!_isRunning) return;
        UpdateTimer();
        CheckTimer();
    }


    public IEnumerator Freeze()
    {
        _isRunning = false;
        yield return new WaitForSeconds(0.5f);
        _isRunning = true;
    }



    public event Action OnTimerEnd;

    private void CheckTimer()
    {
        if (_timer <= 0)
        {
            _isRunning = false;
            OnTimerEnd?.Invoke();
        }
    }

    private void UpdateTimer()
    {
        _timer -= Time.deltaTime;
        // text.text = Mathf.CeilToInt(_timer).ToString();
        text.text = _timer.ToString();

        // Debug.Log($"Timer start{_timer}");
    }
}