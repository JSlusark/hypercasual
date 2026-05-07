using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CoinSpawnerController : MonoBehaviour
{
    [SerializeField] private CoinView coinPrefab;
    [SerializeField] private RectTransform panel;
    public CoinView coinView;
    [SerializeField] private DanceSession danceSessionConfig;
    public AudioSource audioSource;
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnCoinOnTime(danceSessionConfig.coinTimer));
    }

    private void SpawnCoin()
    {
        coinView = Instantiate(coinPrefab, panel);
        coinView.OnCoinCollected += HandleCoinCollected;
        int tip = danceSessionConfig.coinValue; // apply multiplier to the range based on consecutve set, reset multiplier if  consecutiveset is 0
        float[] anchors = new float[] { UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f) };
        coinView.Initialize(tip, anchors[0], anchors[1]);
    }

    private void HandleCoinCollected(int coinValue)
    {
        audioSource.PlayOneShot(danceSessionConfig.audioOnCoinCollected);
        danceSessionConfig.coins+= coinValue;
        coinView.OnCoinCollected -= HandleCoinCollected;
        Destroy(coinView.gameObject);
        StartCoroutine(SpawnCoinOnTime(danceSessionConfig.coinTimer));
    }
    
    
    private IEnumerator SpawnCoinOnTime(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SpawnCoin();
    }
}