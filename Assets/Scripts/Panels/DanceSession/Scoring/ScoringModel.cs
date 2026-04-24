using UnityEngine;
using UnityEngine.TextCore.Text;

public class ScoringModel
{

    private Score c;
    private float _power; // max points generated on round 0 based on character level
    public float Points =>  c.points;
    public int Rounds =>  c.rounds;
    public int Target => c.target;


    public ScoringModel(Score config, CharacterModel character)
    {
        c =  config;
        c.StartValues();
        SetPower(character.ExperienceLevel);
        // Rounds = startRounds;
        // Target = 300;
        // Points = 0; // if player starts with a different amount of points we make it proportional
    }

    public void UpdateScore()
    {
        IncreasePoints();
        ResetPoints();
    }


    // Calculates the starting Power of our character based on character level on a fixed 1.05f factor
    private void SetPower(float characterLvl)
    {
        float basePower = 100f;
        float factor = 1.05f; // 05 is equal to 5% increase (raised to the power of characterLvl)
        _power = basePower * Mathf.Pow(factor, characterLvl); // 1.05 ^ 2 = 1.1025
    }

    // Calculates the remaining Power on a fixed 10% reduction when new level is reached
    private float RemainingPower()
    {
        float reduction = 0.9f; // 10% reduction per session round
        return _power * Mathf.Pow(reduction, c.rounds);
    }

    // Increases score based on remaining Power
    private void IncreasePoints()
    {
        // probably need to fix formula in case when the player reaches very high levels
        c.points += RemainingPower();
        Debug.Log($"[Increase] Power: {RemainingPower()} Points:{Points}, Rounds {Rounds}");
    }

    private void ResetPoints()
    {
        if (Points >= Target) 
        {
            while (Points >= Target)
            {
                c.rounds++;
                c.points -= Target;
            } 
            Debug.Log($"Reset - Rounds:{Rounds} Remainder:{Points}  Total Points: {(Rounds * Target) + Points}  Power: {RemainingPower()}");
        }

        // if (Points >= Target) // 
        // {
        //     Rounds++;
        //     Points = 0;
        // }
    }


    public float GetTotalPoints()
    {
        return (Rounds * Target) + Points; // adds up remaining points of unfinished round
    }


    // thinking of some other data if it could be useful perhaps not here 
    // public int minDanceMoves { get; private set; }
    // public int maxDanceMoves { get; private set; }
    // public int totalSuccessMoves { get; private set; }
    // public int totalFailedMoves { get; private set; }
    // public int completedMovesPerRounds { get; private set; } 

    // private void SetDifficulty() // sets arrow combination and sequence size perhaps this could go in a model for Arrow Manager
    // {
    //     /* Shall depend on from:
    //      * how far in level they are
    //      * how many successful dance moves
    //      */
    // }
}