using UnityEngine;
using DefaultNamespace;

/*
    This class is responsible for managing the different panels in the UI.
    It listens to menu button clicks and switches the active DanceSession accordingly.
    Each button is linked to its corresponding screen in a list of PanelView objects in the inspector.
*/

public class PanelManager : Manager<PanelManager>
{
    [Header("References for imported controllers")]
    [Header("Active Panel Info")]
    [SerializeField] private RectTransform Canvas;

    [SerializeField] private PanelID activePanelID;
    [SerializeField] private PanelController[] panelPrefabList;

    private PanelController activePanelInstance;
    [SerializeField]private PanelEmitterButton[] _menuButtonList;


    protected override void OnAwake()
    {
    }

    private void Start()
    {
        _menuButtonList = MenuBarManager.Instance.GetMenuButtons();
        SetPanelState(activePanelID, true);
        SubscribeToEvents(_menuButtonList, true);
    }


    private void OnDestroy()
    {
        SubscribeToEvents(_menuButtonList, false);
    }

    private void SubscribeToEvents(PanelEmitterButton[] buttons, bool isSubscribed)
    {
        if (buttons == null)
        {
            // Debug.Log("Panel emitter button list is null");
            return;
        }

        foreach (var button in buttons)
        {
            // Debug.Log($"[PanelManager] Subscribing ${activePanelInstance} button ${button.panelID}]: {isSubscribed}");
            if (isSubscribed) button.OnPanelEmitterClick += HandlePanelSwitch;
            else button.OnPanelEmitterClick -= HandlePanelSwitch;
        }
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


    private void SetPanelState(PanelID panelID, bool isActive)
    {
        /* Panel and paired menu button*/
        PanelController panelPrefab = System.Array.Find(panelPrefabList,   panel => panel.panelID   == panelID);
        PanelEmitterButton menuButton = System.Array.Find(_menuButtonList, button => button.panelID == panelID);
        
        if (isActive)
        {
            activePanelInstance = Instantiate(panelPrefab, Canvas);
            SubscribeToEvents(activePanelInstance.PanelEmitterButtons, true);
            if (menuButton is not null)
                menuButton.Select(); // != checks both for destruction and reference, is not null only for reference so less expensive
            activePanelID = panelID;
        }
        else
        {
            if (menuButton is not null) menuButton.Deselect();
            SubscribeToEvents(activePanelInstance.PanelEmitterButtons, false);
            Destroy(activePanelInstance.gameObject);
            activePanelInstance = null;
        }
    }
}