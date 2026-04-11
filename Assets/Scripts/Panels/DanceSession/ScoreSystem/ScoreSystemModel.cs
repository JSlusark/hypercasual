using UnityEngine;

public class ScoreSystemModel
{
    public int Level { get; private set; }
    public float Score { get; private set; }
    private float _scoreCap; // represents length of line to fill to complete a level
    private float _power; // score generated from 1st completed sequence

    public ScoreSystemModel(float characterLevel, int startLevel, float startScore)
    {
        Score = startScore;
        Level = startLevel;
        _scoreCap = 300;
        _power = CalculatePower(characterLevel);
        
        // Debug.Log($"Score: {Score} Level: {Level} _scoreCap: {_scoreCap} _power: {_power}");
    }

    public void Update()
    {
        SetScore();
        SetLevel();
    }

    // Increment is the Dance Power decreased while progressing through dance sequences
    public float GetIncrement(int sessionLevel)
    {
        float reduction = 0.9f; // 10% reduction per session level
        return _power * Mathf.Pow(reduction, sessionLevel);
    }

    // Dance Power is the power a character to replicate a dance sequence
    private float CalculatePower(float characterExp)
    {
        float basePower = 100f;
        float factor = 1.05f; // 05 is equal to 5% increase (raised to the power of characterExp)
        return basePower * Mathf.Pow(factor, characterExp); // 1.05 ^ 2 = 1.1025
    }

    private void SetScore()
    {
        Score += GetIncrement(Level);
    }

    private void SetLevel()
    {
        if (Score >= _scoreCap)
        {
            _scoreCap += _scoreCap;
            Level += 1;
        }
    }
    
    
    // thinking of some other data if it could be useful perhaps not here 
    // public int minDanceMoves { get; private set; }
    // public int maxDanceMoves { get; private set; }
    // public int totalSuccessMoves { get; private set; }
    // public int totalFailedMoves { get; private set; }
    // public int completedMovesPerLevel { get; private set; } 
    
    // private void SetDifficulty() // sets arrow combination and sequence size perhaps this could go in a model for Arrow Manager
    // {
    //     /* Shall depend on from:
    //      * how far in level they are
    //      * how many successful dance moves
    //      */
    // }
    
}