using JetBrains.Annotations;
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
    
    
}