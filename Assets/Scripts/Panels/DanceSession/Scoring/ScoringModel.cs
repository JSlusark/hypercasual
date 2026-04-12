using UnityEngine;

public class ScoringModel
{
    private float _power; // max points generated on level 0
    public int Level { get; private set; }
    public float TotalPoints { get; private set; } // total points collected through all levels within a session
    public float LevelPoints; // points collected during each level, they are reset to 0 at every start of level
    public float LevelTarget { get; private set; } // points collected to complete 1 level completion

    public ScoringModel(float characterLvl, int startLevel, float startTotalPoints)
    {
        _power = SetPower(characterLvl);
        Level = startLevel;
        TotalPoints = startTotalPoints;
        LevelTarget = 300;
        LevelPoints = TotalPoints % 300f; // if player starts with a different amount of points we make it proportional
        // Debug.Log($"[Model start] TotalPoints: {TotalPoints} Level: {Level} LevelTarget: {LevelTarget} _power: {_power}");
    }

    public void Update()
    {
        IncreasePoints();
        ResetLevelPoints();
    }


    // Calculates the starting Power of our character based on character level on a fixed 1.05f factor
    private static float SetPower(float characterLvl)
    {
        float basePower = 100f;
        float factor = 1.05f; // 05 is equal to 5% increase (raised to the power of characterLvl)
        return basePower * Mathf.Pow(factor, characterLvl); // 1.05 ^ 2 = 1.1025
    }

    // Calculates the remaining Power on a fixed 10% reduction when new level is reached
    private float RemainingPower(int level)
    {
        float reduction = 0.9f; // 10% reduction per session level
        return _power * Mathf.Pow(reduction, level);
    }

    // Increases score based on remaining Power
    private void IncreasePoints()
    {
        LevelPoints += RemainingPower(Level);
        // Debug.Log($"[Increase] Points {LevelPoints}, Total Points {TotalPoints}, LevelTarget: {LevelTarget} remaining power: {RemainingPower(Level)}, Level {Level}");
    }

    private void ResetLevelPoints()
    {
        if (LevelPoints >= LevelTarget)
        {
            Level += 1;
            TotalPoints += LevelPoints; // add on collected to total points
            LevelPoints -= LevelTarget;
            // Debug.Log($"Reset - Levelpoints:{LevelPoints} TotalPoints:{TotalPoints}  LevelTarget: {LevelTarget}");
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