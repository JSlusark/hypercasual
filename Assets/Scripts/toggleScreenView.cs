using UnityEngine;
using UnityEngine.UI;

public class ScreenToggleButton : MonoBehaviour
{
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private PanelManager.PanelType targetPanel;
    
    private void Start()
    {
    }


    public void OnClick()
    {
        if (panelManager != null)
        {
            Debug.Log($"Button: {targetPanel} clicked!");
            panelManager.OnPanelSelect(targetPanel);
        }
        else
            Debug.LogError("You forgot to drag the PanelManager into this button's script!", this);
    }
}