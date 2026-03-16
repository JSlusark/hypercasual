using UnityEngine;
using System.Collections.Generic;
using System; // used for event Action delegate

public class ScreenManager : MonoBehaviour
{
    public enum ScreenName
    {
        CharacterSelection,
        CharacterProfile,
        Play,
        PlaySession,
        PlaySessionSummary,
        Shop,
        Missions
    }

    public enum ScreenLayer // ref for what layer the screen belongs to
    {
        MainMenu,
        CharacterSelectionLayer,
        CharacterProfileLayer,
        PlayLayer,
        ShopLayer,
        MissionsLayer
    }

    [System.Serializable]
    public class Screen
    {
        public ScreenName name;
        public ScreenLayer assignedlayer;
        public GameObject screenPrefab;
        public ScreenController controller; // screen controller (derives from ScreenController as base class)
        public bool active; // used only for reference for now
    }

    public List<Screen> screenMenu; // list populated in the inspector
    private Screen _currentScreen;  // active screen, needs to be assigned on awake from screenMenu

    [SerializeField] private MenuBarManager menuBarManager;
    
    void Awake()
    {
        _currentScreen = GetScreen(ScreenName.Play);
        SetScreenState(_currentScreen, true);
        menuBarManager.SwitchButtonView(ScreenName.Play);
    }

    private void OnEnable()
    {
        menuBarManager.OnScreenRequested += HandleScreenView;
    }

    private void OnDisable()
    {
        menuBarManager.OnScreenRequested -= HandleScreenView;
    }

    private void HandleScreenView(ScreenName screenName)
    {
        Debug.Log(screenName);
        Screen newScreen = GetScreen(screenName); // has to take the prefab from screenMenu list 
        SwitchScreenState(newScreen);
    }
    

    public void SwitchScreenState(Screen newScreen)
    {
        if (_currentScreen == null) // top level check to avoid re-checkin in used methods - double false ?
        {
            Debug.Log($"[ERROR]: current screen or new screen is set to null, make sure to fill fields in the inspector. \n Current screen: {_currentScreen}, new screen: {newScreen}");
            return;
        }

        if (_currentScreen.name == newScreen.name)
        {
            Debug.Log($"[WARNING] {newScreen.name} is already active, no switch applied");
            return;
        }

        // stops to avoid set the same screen to active again
        Debug.Log($"[PANEL MANAGER] need to switch {_currentScreen.name} with {newScreen.name}");
        SetScreenState(_currentScreen, false);
        SetScreenState(newScreen,      true);
        _currentScreen = newScreen;
    }


    public void SetScreenState(Screen screen, bool isShown)
    {
        screen.active = isShown;
        if (isShown)
        {
            Debug.Log($"[PANEL MANAGER] ScreenController shown {screen.name}");
            // screen.controller.Show();
        }
        else
        {
            Debug.Log($"[PANEL MANAGER] ScreenController hidden {screen.name}");
            // screen.controller.Hide();
        }
    }

    public Screen GetScreen(ScreenName targetScreen)
    {
        // Debug.Log($" ActiveScreen{_currentScreen.name}  PRESSED: {targetScreen}");
        return screenMenu.Find(it => it.name == targetScreen);
    }
}