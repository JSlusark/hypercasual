using System;
using UnityEngine;

/*
     - base class for all panel controllers
     - methods are called by panel manager when switching panels

     Inheritance in C#: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/inheritance
 */
public abstract class PanelController : MonoBehaviour
{
    [SerializeField] protected GameObject panelPrefab;
    protected GameObject PanelInstance;
    public bool hasSubPanel = false;

    public virtual void Show() 
    {
        if (PanelInstance != null)
        {
            Debug.LogWarning($"[PanelController] {panelPrefab.name} is already shown.");
            return;
        }

        Debug.Log($"[PanelController] Show: {panelPrefab.name}");
        PanelInstance = Instantiate(panelPrefab);
    }

    public virtual void Hide()
    {
        if (PanelInstance == null)
        {
            Debug.LogWarning($"[PanelController] {panelPrefab.name} is not currently shown.");
            return;
        }

        Debug.Log($"[PanelController] Hide: {panelPrefab.name}");
        Destroy(PanelInstance);
        PanelInstance = null;
    }
    
/*
  Function is called from child class so that panel manager can subscribe to the event
  and know what dance panel type (base preview, play, results) should be shown.
  I am not fond of this logic as I feel the DanceSession and DanceSummary are more "overlays" of the Dance Panel
  rather than their own separate panels. 
  Storing this on a separate branch, so that I can keep it in case my reasoning is wrong and need to pursue this
  path instead of the overlay one.
 */
    public event Action<GameObject, bool> OnPanelLayerRequest;
    protected void TriggerPanelLayer(GameObject requestedLayer, bool requestedMenuState)
    {
        OnPanelLayerRequest?.Invoke(requestedLayer, requestedMenuState);
    }
    
    
}