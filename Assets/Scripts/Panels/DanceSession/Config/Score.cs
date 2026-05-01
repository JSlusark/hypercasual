using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Score")]
public class Score : ScriptableObject
{
    public int rounds;
    public float points;
    public int target;
    

    public void StartValues() // resets when Dance Session Controller starts
    {
        target = 3;
        rounds = 0;
        points = 0;
    }
}