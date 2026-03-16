using JetBrains.Annotations;
using UnityEngine;

/*
     - base class for all panel controllers
     - methods are called by panel manager when switching panels
     
     Inheritance in C#: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/inheritance
 */
public abstract class ScreenController : MonoBehaviour
{
    [SerializeField] public ScreenManager.Screen panel; 
    [SerializeField] protected GameObject Instance; 
    
    public void Show()
    {
        Debug.Log($"[PANEL CONTROLLER] showing {panel.name}");
        if (Instance == null)
            Instance = Instantiate(panel.screenPrefab);
        Instance.SetActive(true);
    }
    
    public void Hide()
    {
        if (Instance != null)
        {
            Debug.Log($"[PANEL CONTROLLER] hiding {panel.name}");
            Instance.SetActive(false); // deactivate instead of destroy for now
        }
    }
}