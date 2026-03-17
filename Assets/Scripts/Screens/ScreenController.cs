using JetBrains.Annotations;
using UnityEngine;

/*
     - base class for all panel controllers
     - methods are called by panel manager when switching panels
     
     Inheritance in C#: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/inheritance
 */
public abstract class ScreenController : MonoBehaviour
{
    // [SerializeField] public ScreenManager.Screen panel; 
    // [SerializeField] protected GameObject Instance; 
    [SerializeField] protected GameObject ScreenObject; 
    
    
    public void Show()
    {
        Debug.Log($"[SCREEN CONTROLLER show {ScreenObject.name}");
        // Debug.Log(ScreenObject.activeSelf); //??
        // if (Instance == null)
        //     Instance = Instantiate(panel.screenPrefab);
        // Instance.SetActive(true);
        // ScreenObject.SetActive(true);
    }
    
    public void Hide()
    {
        Debug.Log($"[SCREEN CONTROLLER] hide {ScreenObject.name}");
        
        ScreenObject.SetActive(false);
        // if (Instance != null)
        // {
        //     Debug.Log($"[PANEL CONTROLLER] hiding {panel.name}");
        //     Instance.SetActive(false); // deactivate instead of destroy for now
        // }
    }
}