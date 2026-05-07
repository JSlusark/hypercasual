using System;
using UnityEngine;

public class CoinSpawnerController : MonoBehaviour
{
    [SerializeField] private CoinView coinPrefab;
    [SerializeField] private RectTransform panel;
    private CoinView coinView;

    private void Awake()
    {
        Debug.Log("CoinSpawnerController initialized");
        SpawnCoin();
    }
    

    private void SpawnCoin()
    {
        coinView = Instantiate(coinPrefab, panel);
        coinView.OnCoinCollected += HandleCoinCollected;
        coinView.SetRandomPosition(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        
    }

    private void HandleCoinCollected()
    {
        coinView.OnCoinCollected -= HandleCoinCollected;
        Destroy(coinView.gameObject);
        SpawnCoin();
    }
}