using UnityEngine;
    public class TimerModel
    {
        public float Timer { get; private set; }
        public float MaxTimer { get; private set; }
        public bool IsRunning { get; private set; }

        public TimerModel(float timer, bool isRunning)
        {
            MaxTimer = timer;
            Timer = timer;
            IsRunning = isRunning;
        }
        
        public void Pause() => IsRunning = false;
        public void Resume() => IsRunning = true;
        public void UpdateTimer(float value) => Timer = Timer + value;
    }