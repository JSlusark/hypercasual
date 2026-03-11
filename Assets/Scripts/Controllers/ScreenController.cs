using JetBrains.Annotations;
using UnityEngine;

/*
     - base class for all screen controllers
     - methods are called by panel manager when switching panels
     
     Inheritance in C#: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/inheritance
 */
public abstract class ScreenController : MonoBehaviour
{
    [SerializeField] public ScreenManager.Panel panel; 
    protected GameObject Instance; 
    [SerializeField] private GameObject panelPrefab;
    
    public void Show()
    {
        Debug.Log($"[PANEL CONTROLLER] showing {panel.name}");
        if (Instance == null)
            Instance = Instantiate(panel.panelPrefab);
        Instance.SetActive(true);
    }
    
    public void Hide()
    {
        if (Instance != null)
        {
            Debug.Log($"[PANEL CONTROLLER] hiding {panel.name}");
            // You can choose to Destroy or just Deactivate
            Instance.SetActive(false);
            // Destroy(_currentPanelInstance);
        }
    }
}