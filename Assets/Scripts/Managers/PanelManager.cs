using UnityEngine;
using System.Collections.Generic;

/*
    This class is responsible for managing the different panels in the UI.
    It listens to menu button clicks and switches the active panel accordingly.
    Each button is linked to its corresponding screen in a list of PanelView objects in the inspector.
*/


public class PanelManager : MonoBehaviour
{
    /*Subscriptions*/
    [SerializeField] private GameObject menuBars;
    
    [SerializeField] private Panel activePanel; // active panel, stored to allow simple panel switching (added in the inspector, might come from database later)
    
    [System.Serializable]
    public class Panel
    {
        public PanelEmitterButton button; // button that emits panel request to manager 
        public PanelController view; 
    }
    public List<Panel> panelList;                // list of panels, populated in the inspector

    
    private void Start()
    {
        SwitchActivePanel(activePanel);
        SubScribeToActivePanelEvents();
    }
    
    public void OnEnable()
    {
        foreach (var panel in panelList)
            panel.button.OnMenuButtonClick += HandleNewPanelRequest;
        
    }

    public void OnDisable()
    {
        foreach (var panel in panelList)
            panel.button.OnMenuButtonClick -= HandleNewPanelRequest;
    }

    private void HandleNewPanelRequest(PanelEmitterButton clickedButton)
    {
        Panel requestPanel = panelList.Find(panel => panel.button == clickedButton);
        SwitchActivePanel(requestPanel);
        SubScribeToActivePanelEvents();
    }

    private void SwitchActivePanel(Panel requestPanel)
    {
        
        if (activePanel.view != requestPanel.view && activePanel.button != requestPanel.button)
        {
            activePanel.view.Hide();
            activePanel.button.Deselect();
        }
        
        requestPanel.view.Show();
        if(requestPanel.button != null) requestPanel.button.Select();
        activePanel = requestPanel;
    }

    void SubScribeToActivePanelEvents()
    {
        if(activePanel.view.hasSubPanel)
            activePanel.view.OnPanelLayerRequest += HandlePanelLayerRequest;
    }
   
    private void HandlePanelLayerRequest(GameObject requestedSubPanel, bool requestedMenuState)
    {
        Debug.Log(requestedMenuState ? "Main Layer is shown": "SubLayer is shown");
        
        Panel subPanel= new Panel();
        subPanel.button = null;
        subPanel.view = requestedSubPanel.GetComponent<PanelController>();
        menuBars.SetActive(requestedMenuState);
        
        SwitchActivePanel(subPanel);
    }
    
   
}