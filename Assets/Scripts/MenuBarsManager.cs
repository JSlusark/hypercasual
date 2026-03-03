using UnityEngine;

using UnityEngine;

public class MenuBarsManager : MonoBehaviour
{
    [SerializeField] private GameObject lowerBar;

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        PlayScreenManager.OnSessionStart += HandlePlayStarted;
        PlayScreenManager.OnReturnToPreview += ShowBars;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        PlayScreenManager.OnSessionStart -= HandlePlayStarted;
        PlayScreenManager.OnReturnToPreview -= ShowBars;
    }

    private void HandlePlayStarted()
    {
        Debug.Log("lower bar listened");
        lowerBar.SetActive(false);
    }
    
    private void ShowBars()
    {
        lowerBar.SetActive(true);
    }
}