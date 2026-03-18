using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/*
    This class is responsible for managing the different panels in the UI.
    It listens to menu button clicks and switches the active panel accordingly.
    Each button is linked to its corresponding screen in a list of PanelView objects in the inspector.
*/


public class PanelManager : MonoBehaviour
{
    [System.Serializable]
    public class Panel
    {
        public PanelEmitterButton button; // button that emits panel request to manager 
        public PanelController view; 
    }
    
    public List<Panel> panelList;                // list of panels, populated in the inspector
    [SerializeField] private Panel activePanel; // active panel, stored to allow simple panel switching (added in the inspector, might come from database later)

    
    private void Start()
    {
        SwitchActivePanel(activePanel);
    }
    
    public void OnEnable()
    {
        foreach (var panel in panelList)
            panel.button.OnMenuButtonClick += HandlePanelRequest;
    }

    public void OnDisable()
    {
        foreach (var panel in panelList)
            panel.button.OnMenuButtonClick -= HandlePanelRequest;
    }

    private void HandlePanelRequest(PanelEmitterButton clickedButton)
    {
        Panel requestPanel = panelList.Find(panel => panel.button == clickedButton);
        SwitchActivePanel(requestPanel);
    }
    
    private void SwitchActivePanel(Panel requestPanel)
    {

        if (activePanel.view != requestPanel.view && activePanel.button != requestPanel.button)
        {
            activePanel.view.Hide();
            activePanel.button.Deselect();
        }
        
        requestPanel.view.Show();
        requestPanel.button.Select();
        activePanel = requestPanel;
    }
    
   
}