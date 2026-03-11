using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUpdateButtonState : MonoBehaviour
{
    [SerializeField] private ScreenManager panelManager; // import reference to panel manager to use event signal for panel changes
    [SerializeField] private ScreenManager.PanelName targetPanel;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;


    private void Awake()
    {
        panelManager.OnPanelChanged += UpdateButtonState; // assigns the UpdateButtonState method to listen for onPanelChanged event
    }

    private void OnDestroy()
    {
        panelManager.OnPanelChanged -= UpdateButtonState; // unsubscribes from panel change event to prevent memory leaks
    }
    
    
    private void UpdateButtonState(ScreenManager.Panel activePanel)
    {
        // Debug.Log($"Panel manager activated: {panelManager.activePanel.name }, button's target panel: {targetPanel}");
        if (activePanel.name == targetPanel)
        {
            Debug.Log($"[BUTTON HIGHLIGHT] {targetPanel}");
            GetComponent<Image>().sprite = selectedSprite;
            transform.localScale = new Vector3(1f, 1.15f, 1f);
        }
        else
        {
            GetComponent<Image>().sprite = defaultSprite;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnClick()
    {
        if (panelManager == null) return;
        Debug.Log($"Button clicked {targetPanel}");
        panelManager.OnPanelSelect(targetPanel);
    }
}