using System;
using UnityEngine;

public class CoinView : MonoBehaviour
{

    [SerializeField] private RectTransform _rectTransform;
    
    public event Action OnCoinCollected;
    
    
    public void CoinCollected()
    {
        Debug.Log($"COIN {this.GetInstanceID()} COLLECTED");
        OnCoinCollected?.Invoke();
    }

    
    public void SetRandomPosition(float x, float y)
    {
        _rectTransform.anchorMin = new Vector2(x, y);
        _rectTransform.anchorMax = new Vector2(x, y);
        _rectTransform.anchoredPosition = Vector2.zero;
        
        Debug.Log($"COIN {this.GetInstanceID()} x: {x} y:{y}");
    }
    
}
