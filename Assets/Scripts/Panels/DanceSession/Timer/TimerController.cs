using System;
using System.Collections;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TimerBarView _timerBarView;
    
    [Header("Dynamic Values: can change from booster activation and character level")]

    TimerModel _timerModel;
    public event Action OnTimerEnd;


    public void Awake()
    {
        _timerModel = new TimerModel(20f, true);
        _timerBarView.UpdateFill(_timerModel.Timer, _timerModel.MaxTimer);
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
        _timerBarView.UpdateFill(_timerModel.Timer, _timerModel.MaxTimer);
    }

    private void CheckTimer()
    {
        if (_timerModel.Timer <= 0)
        {
            _timerModel.Pause();
            OnTimerEnd?.Invoke();
        }
    }


    public void Stop()
    {
        StartCoroutine(Freeze());
    }

    private IEnumerator Freeze()
    {
        _timerModel.Pause();
        yield return new WaitForSeconds(0.5f);
        _timerModel.Resume();
    }
}