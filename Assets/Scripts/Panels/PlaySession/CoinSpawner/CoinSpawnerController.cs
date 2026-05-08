using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CoinSpawnerController : MonoBehaviour
{
    [SerializeField] private DanceSession danceSessionConfig;
    [SerializeField] private RectTransform panel;
    [SerializeField] private CoinView coinPrefab;
    private CoinView _coinView;
    [SerializeField] private AudioSource audioSource;
    

    private void Awake()
    {
        StartCoroutine(SpawnCoinOnTime(danceSessionConfig.coinTimer));
    }

    private void SpawnCoin()
    {
        _coinView = Instantiate(coinPrefab, panel);
        _coinView.OnCoinCollected += HandleCoinCollected;
        int tip = danceSessionConfig.coinValue; // apply multiplier to the range based on consecutve set, reset multiplier if  consecutiveset is 0
        float[] anchors = new float[] { UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f) };
        _coinView.Initialize(tip, anchors[0], anchors[1]);
    }

    private void HandleCoinCollected(int coinValue)
    {
        audioSource.PlayOneShot(danceSessionConfig.audioOnCoinCollected);
        danceSessionConfig.coins+= coinValue;
        _coinView.OnCoinCollected -= HandleCoinCollected;
        Destroy(_coinView.gameObject);
        StartCoroutine(SpawnCoinOnTime(danceSessionConfig.coinTimer));
    }
    
    
    private IEnumerator SpawnCoinOnTime(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SpawnCoin();
    }
}