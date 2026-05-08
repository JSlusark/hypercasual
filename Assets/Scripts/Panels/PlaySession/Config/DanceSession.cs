using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/DanceSession")]
public class DanceSession : ScriptableObject
{
    [Header("Runtime")] 
    public int rounds;
    public float points;
    public int coins;

    [Header("Config")]
    public int roundTarget;

    public float sessionTimer = 20;
    public int coinValue = 2; // in coinSpawn controller can be changed perhaps based on dance performance
    public float coinTimer = 3f;

    [Header("Audio")]
    public AudioClip audioOnFailedMove;
    public AudioClip audioOnSuccessMove;
    public AudioClip audioOnCoinCollected;


    public void Initialize() // resets when Dance Session Panel Awakens
    {
        roundTarget = 3;
        rounds = 0;
        points = 0;
        coins = 0;
    }
}