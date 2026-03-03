using UnityEngine;
using System;

public class PlayScreenManager : MonoBehaviour
{
    
    public static event Action OnSessionStart;
    public static event Action OnReturnToPreview;

    [SerializeField] private GameObject previewScreenRoot;
    [SerializeField] private GameObject sessionScreenRoot;
    [SerializeField] private GameObject resultScreenRoot;

    
    public void ShowSessionScreen()
    {
        previewScreenRoot.SetActive(false);
        sessionScreenRoot.SetActive(true);

        OnSessionStart?.Invoke();
    }
    
    public void ShowPreviewScreen()
    {
        previewScreenRoot.SetActive(true);
        sessionScreenRoot.SetActive(false);
        resultScreenRoot.SetActive(false);
        OnReturnToPreview?.Invoke();
    }
}
