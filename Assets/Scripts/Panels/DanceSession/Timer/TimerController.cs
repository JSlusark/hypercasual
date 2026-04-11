using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TimerView _timerView;
    public event Action OnTimerEnd;

    TimerModel _timerModel;

    private float timerModel = 20f;

    public void Awake()
    {
        _timerModel = new TimerModel(20f, true);
        _timerView.Show(_timerModel.Timer);
    }


    private void Update()
    {
        if (!_timerModel.IsRunning) return;
        UpdateTimer();
        CheckTimer();
    }

    private void UpdateTimer()
    {
        _timerModel.UpdateTimer(-Time.deltaTime);
        _timerView.Show(_timerModel.Timer);
    }

    private void CheckTimer()
    {
        if (_timerModel.Timer <= 0)
        {
            _timerModel.Pause();
            OnTimerEnd?.Invoke();
        }
    }


    public IEnumerator Freeze()
    {
        _timerModel.Pause();
        yield return new WaitForSeconds(0.5f);
        _timerModel.Resume();
    }
}