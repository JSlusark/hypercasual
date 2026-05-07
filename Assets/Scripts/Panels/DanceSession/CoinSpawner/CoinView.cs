using System;
using UnityEngine;

public class CoinView : MonoBehaviour
{

    [SerializeField] private RectTransform _rectTransform;

    private int value;
    public event Action<int> OnCoinCollected;
    
    
    public void CoinCollected()
    {
        // Debug.Log($"COIN {this.GetInstanceID()} COLLECTED");
        OnCoinCollected?.Invoke(value);
    }

    public void Initialize(int value, float x, float y)
    {
        SetCoinValue(value);
        SetRandomPosition(x, y);
        
    }

    private void SetCoinValue(int n)
    {
        value = n;
    }
    
    private void SetRandomPosition(float x, float y)
    {
        _rectTransform.anchorMin = new Vector2(x, y);
        _rectTransform.anchorMax = new Vector2(x, y);
        _rectTransform.anchoredPosition = Vector2.zero;
        
        // Debug.Log($"COIN {this.GetInstanceID()} x: {x} y:{y}");
    }
    
}
