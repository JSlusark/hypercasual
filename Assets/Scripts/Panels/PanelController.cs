using DefaultNamespace;
using UnityEngine;

/*
     - base class for all panel controllers
     - methods are called by panel manager when switching panels

     Inheritance in C#: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/inheritance
 */
public abstract class PanelController : MonoBehaviour
{
    protected GameObject PanelInstance;
    
    [Header("Panel Info")]
    public PanelID panelID;
    public bool showsMenuBar;
    [SerializeField] protected GameObject panelPrefab;
    
    [Header("PanelEmitterButtons - needed by PanelManager")]
    public PanelEmitterButton[] PanelEmitterButtons { get; private set; } 
    


    public virtual void Show() 
    {
        if (PanelInstance != null)
        {
            Debug.LogWarning($"[PanelController] {panelPrefab.name} is already instantiated - did you forget to call Hide()?");
            return;
        }

        // Debug.Log($"[PanelController] Show: {panelPrefab.name}");
        PanelInstance = Instantiate(panelPrefab);
        PanelEmitterButtons = PanelInstance.GetComponentsInChildren<PanelEmitterButton>();
        // Debug.Log($"[PanelController] Found {PanelEmitterButtons.Length} PanelEmitterButtons in {panelPrefab.name}");
    }

    public virtual void Hide()
    {
        if (PanelInstance == null)
        {
            // Debug.LogWarning($"[PanelController] {panelPrefab.name} is not currently shown.");
            return;
        }
        
        // Debug.Log($"[PanelController] Hide: {panelPrefab.name}");
        Destroy(PanelInstance);
        PanelInstance = null;
    }
    
}