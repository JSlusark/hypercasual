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
    [SerializeField] private GameObject _panelInstance;

    public void Show()
    {
        if (_panelInstance != null)
        {
            Debug.LogWarning($"[PanelController] {panelPrefab.name} is already shown.");
            return;
        }

        Debug.Log($"[PanelController] Show: {panelPrefab.name}");
        _panelInstance = Instantiate(panelPrefab);
    }

    public void Hide()
    {
        if (_panelInstance == null)
        {
            Debug.LogWarning($"[PanelController] {panelPrefab.name} is not currently shown.");
            return;
        }

        Debug.Log($"[PanelController] Hide: {panelPrefab.name}");
        Destroy(_panelInstance);
        _panelInstance = null;
    }
}