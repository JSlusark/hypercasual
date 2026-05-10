using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : Manager<GameManager>
{
    SaveSystem _saveSystem;
    
    protected override void OnAwake()
    {
        _saveSystem = SaveSystem.Instance;
    }

    ///    <summary>
    /// Unity built in methods for to handle when app is paused or quit
    /// - By default, on mobile Pause is always triggered before Quit (so saving needs to be a synchronous operation)
    /// - This is not the case on Desktop
    ///    </summary>
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused) _saveSystem.Save();
    }

    private void OnApplicationQuit()
    {
        // _timerCancellation.Cancel(); // timerSave needs to be interrupted or it will still be active on quit
        _saveSystem.Save();
    }
}