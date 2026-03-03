using UnityEngine;
using System.Collections.Generic;
using System; // used for event Action delegate

public class PanelManager : MonoBehaviour
{
    public enum PanelType
    {
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
    public Panel activePanel; // active panel object
    
    // public Transform uiRoot;       // root - where the instantiated panels spawn under
    private GameObject _currentPanelInstance;
    
    public event Action<Panel> OnPanelChanged;
    
    void Start()
    {
        ChangePanelVisibility(GetPanelByType(activePanel.name), true); 
    }
    
    public void OnPanelSelect(PanelType targetPanel)
    {
        SetNewPanelView(GetPanelByType(targetPanel));
        OnPanelChanged?.Invoke(activePanel); // sends out panel change alert to all listeners (buttons) for OnPanelChanged
    }
    
    public void SetNewPanelView(Panel targetPanel)
    {
        if (activePanel == null || activePanel == targetPanel) return; // unsure if leaving error message for the null so budling together
        ChangePanelVisibility(activePanel, false); 
        ChangePanelVisibility(targetPanel, true); 
        activePanel = targetPanel; 
    }
    
    
    public void ChangePanelVisibility(Panel panel, bool view)
    {
        if (panel == null) 
        {
            return; // Stop running the code before it crashes
        }
        panel.active = view; // need to set the old panel to inactive first
        if(view)
        {
            _currentPanelInstance = Instantiate(panel.panelPrefab);
            // Debug.Log($" Instantiated {_currentPanelInstance.name} ");
        }
        else
        {
            // Debug.Log($" Destroyed {_currentPanelInstance.name} ");
            Destroy(_currentPanelInstance);
        }
    }
    
    public Panel GetPanelByType(PanelType panelName)
    {
        // Debug.Log($" ActivePanel{activePanel.name}  PRESSED: {panelName}");
        return panelMenu.Find(it => it.name == panelName);
    }
    
        
    
    
}
