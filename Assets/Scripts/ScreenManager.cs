using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    public enum PanelType
    {
        None,
        CharacterSelection,
        CharacterProfile,
        Play,
        Shop,
        Missions
    }
    
    
    
    [System.Serializable]
    public class Panel {
        public PanelType name;
        public GameObject panelPrefab;
        public bool active; // used only for reference for now
        
    }
    
    public List<Panel> panelMenu; 
    public Panel activePanel;
    
    
    void Start()
    {
        panelMenu.ForEach(panel => {
            if (panel.name != activePanel.name)
            {
                panel.panelPrefab.SetActive(false);
            }
        });
        UpdateActivePanel(activePanel); 
    }
    //
    public void UpdateActivePanel(Panel selectedPanel)
    {
        Debug.Log($"Clicked {selectedPanel.name} ");
            if (activePanel == null) return;
            if (activePanel == selectedPanel) return;
            TogglePanelView(GetPanel(activePanel.name), false); 
            TogglePanelView(GetPanel(selectedPanel.name), true); 
            activePanel = selectedPanel; 
    }
    
    public void TogglePanelView(Panel panel, bool view)
    {
        panel.active = view; // need to set the old panel to inactive first
        panel.panelPrefab.SetActive(view);
        Debug.Log($" {panel.name} set to {view} ");
    }
    
    
    private Panel GetPanel(PanelType panelName)
    {
        // Debug.Log($" ActivePanel{activePanel.name}  PRESSED: {panelName}");
        return panelMenu.Find(it => it.name == panelName);
    }
    
    
    // ________  Will create 1 script with same single function for each button, add this quickly to test now ________
    public void OnCharacterSelectionButtonClicked()
    {
        UpdateActivePanel(GetPanel(PanelType.CharacterSelection));
    }
    
    public void OnCharacterProfileButtonClicked()
    {
        UpdateActivePanel(GetPanel(PanelType.CharacterProfile));
    }
    
    public void OnPlayButtonClicked()
    {
        UpdateActivePanel(GetPanel(PanelType.Play));
    }
    
    public void OnShopButtonClicked()
    {
        UpdateActivePanel(GetPanel(PanelType.Shop));
    }
    
    public void OnMissionsButtonClicked()
    {
        UpdateActivePanel(GetPanel(PanelType.Missions));
    }
    
}
