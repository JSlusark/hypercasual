using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : Manager<GameManager>
{
    SaveSystem _saveSystem;
    // [SerializeField] private SwipeManager _swipeManager;
    // [SerializeField] private PanelManager _panelManager;
    // [SerializeField] private ConfigManager _configManager;


    protected override void Awake()  
    {
        base.Awake();
        /*
         * The ideal would be having just gamemanager as a monobehaviour
         * and the other "submanagers" as pure c# singletons, as it can make it easier
         * to convert the logic to another engine.
         *
         * For now, Awake order is set in Edit>ProjectSettings>Script Execution Order
         */
        
        _saveSystem = SaveSystem.Instance;
    }


    ///    <summary>
    /// Unity built in methods for to handle when app is paused or quit
    ///- By default, on mobile Pause is always triggered before Quit (so saving needs to be a synchronous operation)
    ///     - This is not the case on Desktop
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