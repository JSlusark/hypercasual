using UnityEngine;
using System.Collections.Generic;
using System; // used for event Action delegate

public class ScreenManager : MonoBehaviour
{
    public event Action<Panel> OnPanelChanged; // signal that is invoked to alert objects that listen for panel change

    public enum PanelName
    {
        CharacterSelection,
        CharacterProfile,
        Play,
        PlaySession,
        PlaySessionSummary,
        Shop,
        Missions
    }

    public enum PanelLayer // ref for what layer the panel belongs to
    {
        MainMenu,
        CharacterSelectionLayer,
        CharacterProfileLayer,
        PlayLayer,
        ShopLayer,
        MissionsLayer
    }

    [System.Serializable]
    public class Panel
    {
        public PanelName name;
        public PanelLayer assignedlayer;
        public GameObject panelPrefab;
        public ScreenController controller; // panel controller (derives from ScreenController as base class)
        public bool active;                 // used only for reference for now
    }

    public List<Panel> panelMenu; // list populated in the inspector
    private Panel _currentPanel;// active panel, needs to be assigned on awake from panelMenu


    void Awake()
    {
        // SwitchPanelView(_currentPanel);
        
        _currentPanel = GetPanel(PanelName.Play);
        SetPanelState(_currentPanel, true);
        OnPanelChanged?.Invoke(_currentPanel);
    }

    public void OnPanelSelect(PanelName targetPanel)
    {
        Panel newPanel = GetPanel(targetPanel); // has to take the prefab from panelMenu list 
        SwitchPanelView(newPanel);
    }

    public void SwitchPanelView(Panel newPanel)
    {
        
        if (_currentPanel == null) // top level check to avoid re-checkin in used methods - double false ?
        {
            Debug.Log($"[ERROR]: current panel or new panel is set to null, make sure to fill fields in the inspector. \n Current panel: {_currentPanel}, new panel: {newPanel}");
            return;
        }
        if (_currentPanel.name == newPanel.name)
        {
            Debug.Log($"[WARNING] {newPanel.name} is already active, no switch applied");
            return;
        }; // stops to avoid set the same panel to active again
        Debug.Log($"[PANEL MANAGER] need to switch {_currentPanel.name} with {newPanel.name}");
        SetPanelState(_currentPanel, false);
        SetPanelState(newPanel,     true);
        _currentPanel = newPanel;
        OnPanelChanged?.Invoke(_currentPanel);
    }


    public void SetPanelState(Panel panel, bool isShown)
    {
        panel.active = isShown;
        if (isShown)
        {
            Debug.Log($"[PANEL MANAGER] ScreenController has to show {panel.name}");
            panel.controller.Show();
        }
        else
        {
            Debug.Log($"[PANEL MANAGER] ScreenController has to hide {panel.name}");
                panel.controller.Hide();
        }
    }

    public Panel GetPanel(PanelName targetPanel)
    {
        // Debug.Log($" ActivePanel{_currentPanel.name}  PRESSED: {targetPanel}");
        return panelMenu.Find(it => it.name == targetPanel);
    }
}