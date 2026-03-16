using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * MenuBar Manager is responsible for handling the menu bar buttons and their interactions
 * with the screen manager.
 * It was added to avoid crowding screen manager with button logic and array management.
 * MenuBarManager is for menuButtons what ScreenManager is for screens (loaded as prefabs)
 * Therefore might add prefab rendering logic for buttons later
 */


public class MenuBarManager : MonoBehaviour
{
    [SerializeField] private MenuBarView[] buttons;
    
     /*Signal emitted to ScreenManager when a button is clicked
    carries the ScreenName of the requested screen for the manager to handle*/
    public event Action<ScreenManager.ScreenName> OnScreenRequested;

    public void OnEnable()
    {
        foreach (var button in buttons)
            button.OnButtonClick += HandleButtonClick;
    }

    public void OnDisable()
    {
        foreach (var button in buttons)
            button.OnButtonClick -= HandleButtonClick;
    }

    private void HandleButtonClick(ScreenManager.ScreenName screenName)
    {
        SwitchButtonView(screenName);
        OnScreenRequested?.Invoke(screenName); // forwards up to ScreenManager
    }

    public void SwitchButtonView(ScreenManager.ScreenName screenName)
    {
        foreach (var button in buttons)
            button.SetState(screenName);
    }
}