using UnityEngine;

public class CanvasCameraFixer : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        _canvas.worldCamera = Camera.main;
    }
}