using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/*
    This class is responsible for managing the different panels in the UI.
    It listens to menu button clicks and switches the active panel accordingly.
    Each button is linked to its corresponding screen in a list of PanelView objects in the inspector.
*/


public class PanelViewManager : MonoBehaviour
{
    [System.Serializable]
    public class PanelView
    {
        public MenuButton button; // buttonView
        public ScreenController screen; // panelCaller
    }
    
    public List<PanelView> panelList;                // list populated in the inspector
    [SerializeField] private PanelView activePanel; // stored active button (will come from database later)

    
    private void Start()
    {
        SwitchActiveView(activePanel);
    }
    
    public void OnEnable()
    {
        foreach (var view in panelList)
            view.button.OnMenuButtonClick += HandleViewRequest;
    }

    public void OnDisable()
    {
        foreach (var view in panelList)
            view.button.OnMenuButtonClick += HandleViewRequest;
    }

    private void HandleViewRequest(MenuButton clickedButton)
    {
        PanelView request = panelList.Find(screen => screen.button == clickedButton);
        SwitchActiveView(request);
    }
    
    private void SwitchActiveView(PanelView request)
    {

        if (activePanel.screen != request.screen && activePanel.button != request.button)
        {
            activePanel.screen.Hide();
            activePanel.button.Deselect();
        }
        
        request.screen.Show();
        request.button.Select();
        activePanel = request;
    }
    
   
}