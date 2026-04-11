using UnityEngine;

public class DanceSessionModel
{
    public int Level { get; private set; }
    public float TimeAvailable { get; private set; }

    public float Score;
    private float DancePower;  // the max increment from game start, depends on charcater exp
    private float _difficulty; // range from 0 to 10

    // thinking of some data 
    // public int minDanceMoves { get; private set; }
    // public int maxDanceMoves { get; private set; }
    // public int totalSuccessMoves { get; private set; }
    // public int totalFailedMoves { get; private set; }
    // public int completedMovesPerLevel { get; private set; } 

    public DanceSessionModel(int startLevel, float timeAvailable, float score, float characterExp)
    {
        Level = startLevel; // so that for example I can start from higher level if special is triggered
        TimeAvailable = timeAvailable;
        Score = score;
        DancePower = GetDancePower(characterExp);
    }
    
    // Score models?
    float levelCap = 300; // once cap is reached would be cool to have  dance animation
    public void UpdateLevel()
    {
        if(Score >= levelCap)
        {
            levelCap += 300;
            Level += 1;
        }
    }
    
    
    public void SetScore()
    { 
        Score += GetIncrement(Level);
    }
    
    // CharacterPower 
    private float GetDancePower(float characterExp)
    {
        float baseDancePower = 100f;
        float factor = 1.05f; // 05 is equal to 5% increase (raised to the power of characterExp)
        return baseDancePower * Mathf.Pow(factor, characterExp); // 1.05 ^ 2 = 1.1025
    }

    // DancePoint is the Character Power decreasing through levels
    public float GetIncrement(int sessionLevel)
    {
        float reduction = 0.9f; // 10% reduction per session level
        return DancePower * Mathf.Pow(reduction, sessionLevel);
    }


    private void SetDifficulty()
    {
        /* Shall depend on from:
         * how far in level they are
         * how many successful dance moves
         */
    }
}