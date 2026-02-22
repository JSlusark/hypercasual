using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUpdateButtonState : MonoBehaviour
{
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private PanelManager.PanelType targetPanel;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;


    private void Start()
    {
        Debug.Log($"Button started {targetPanel}");
        panelManager.OnPanelChanged += UpdateButtonState; // subscribes to panel change event from panel manager
    }

    private void OnDestroy()
    {
        panelManager.OnPanelChanged -= UpdateButtonState; // unsubscribes from panel change event to prevent memory leaks
    }

    // private void UpdateButtonState(PanelManager.Panel activePanel)
    // {
    //     UpdateButtonState(); // delegate method to match the event signature, calls the original logic to update the button's appearance
    // }
    
    private void UpdateButtonState(PanelManager.Panel activePanel)
    {
        // Debug.Log($"Panel manager activated: {panelManager.activePanel.name }, button's target panel: {targetPanel}");
        if (activePanel.name == targetPanel)
        {
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