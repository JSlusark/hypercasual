using System;
using UnityEngine;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*
    This class is responsible for managing the different panels in the UI.
    It listens to menu button clicks and switches the active DanceSession accordingly.
    Each button is linked to its corresponding screen in a list of PanelView objects in the inspector.
*/


public class PanelManager : Manager<PanelManager>
{
    [Header("References for imported controllers")] 
    [SerializeField] private GameObject menuBars;
    [SerializeField] private GameObject panelControllers;
    [Header("Active Panel Info")]
    [SerializeField] private PanelID activePanelID;
    
    // Panel and button lists from imported controllers
    private PanelController[] _panelList;
    private PanelEmitterButton[] _menuButtonList;

    protected override void Awake()
    {
        base.Awake();
        
        _menuButtonList = menuBars.GetComponentsInChildren<PanelEmitterButton>();
        _panelList = panelControllers.GetComponentsInChildren<PanelController>();
    }

    private void Start()
    {
        SetPanelState(activePanelID, true);
    }

    public void OnEnable()
    {
        SubscribeToPanelEmitterButtons(_menuButtonList);
    }

    public void OnDisable()
    {
        UnsubscribeToPanelEmitterButtons(_menuButtonList);
    }
    
    private void SubscribeToPanelEmitterButtons(PanelEmitterButton[] buttons)
    {
        foreach (var button in buttons)
        {
            if (button.isInMenuBar && button.panelID.ToString() != button.name) // hard coded check to make sure menu buttons have the right panelID
                throw new System.InvalidOperationException($"[PanelManager] Menu button \"{button.name}\" does not match with its panelID \"{button.panelID}\".");
            button.OnPanelEmitterClick += HandlePanelSwitch;
        }
    }

    private void UnsubscribeToPanelEmitterButtons(PanelEmitterButton[] buttons)
    {
        foreach (var button in buttons)
            button.OnPanelEmitterClick -= HandlePanelSwitch;
    }

    private void HandlePanelSwitch(PanelID requestedPanelID)
    {
        if (requestedPanelID == activePanelID)
        {
            // Debug.LogWarning($"[PanelManager] Nothing to switch as Panel {requestedPanelID} is already active.");
            return;
        }

        // Debug.Log($"[PanelManager] Received DanceSession request: {requestedPanelID}");
        SetPanelState(activePanelID,    false);
        SetPanelState(requestedPanelID, true);
    }


    private void SetPanelState(PanelID panelID, bool setActive)
    {
        /* Panel and paired menu button*/
        PanelController panel = System.Array.Find(_panelList,               panel => panel.panelID   == panelID);
        PanelEmitterButton menuButton = System.Array.Find(_menuButtonList, button => button.panelID == panelID);
        if (setActive)
        {
            panel.Show();
            menuBars.SetActive(panel.showsMenuBar);
            if(menuButton != null) menuButton.Select();
            if (panel.PanelEmitterButtons != null) SubscribeToPanelEmitterButtons(panel.PanelEmitterButtons);
            activePanelID = panelID;
        }
        else
        {
            if (menuButton != null) menuButton.Deselect();
            if (panel.PanelEmitterButtons != null) UnsubscribeToPanelEmitterButtons(panel.PanelEmitterButtons);
            panel.Hide();
        }
    }
    
    
    
}