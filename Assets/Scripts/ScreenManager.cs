using UnityEngine;
using System.Collections.Generic;

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
    
    public Transform uiRoot;       // root - where the instantiated panels spawn under
    private GameObject _currentSpawnedObject;
    
    void Start()
    {
        // panelMenu.ForEach(panel => {
        //     if (panel.name != activePanel.name)
        //     {
        //         panel.panelPrefab.SetActive(false);
        //     }
        // });
        TogglePanelView(GetPanel(activePanel.name), true); 
    }
    
    
    
    public Panel GetPanel(PanelType panelName)
    {
        // Debug.Log($" ActivePanel{activePanel.name}  PRESSED: {panelName}");
        return panelMenu.Find(it => it.name == panelName);
    }
    
    public void UpdateActivePanel(Panel selectedPanel)
    {
        Debug.Log($"Clicked: {selectedPanel.name} ");
            if (activePanel == null || activePanel == selectedPanel) return; // unsure if leaving error message for the null so budling together
            TogglePanelView(activePanel, false); 
            TogglePanelView(selectedPanel, true); 
            activePanel = selectedPanel; 
    }
    
    public void TogglePanelView(Panel panel, bool view)
    {
        if (panel == null) 
        {
            Debug.LogError("ERROR: selected panel missing from PanelMenu!");
            return; // Stop running the code before it crashes
        }
        panel.active = view; // need to set the old panel to inactive first
        Debug.Log($" Toggle {view}: {panel.name} ");
        // panel.panelPrefab.SetActive(view);
        if(view)
        {
            _currentSpawnedObject = Instantiate(panel.panelPrefab, uiRoot, false);
            Debug.Log($" Instantiated {_currentSpawnedObject.name} ");
        }
        else
        {
            Debug.Log($" Destroyed {_currentSpawnedObject.name} ");
            Destroy(_currentSpawnedObject);
        }
        
    }
    
    public void OnPanelSelect(PanelType selectedPanel)
    {
        UpdateActivePanel(GetPanel(selectedPanel));
    }
    
}
