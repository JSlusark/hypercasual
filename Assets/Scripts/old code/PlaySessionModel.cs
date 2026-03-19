/*
 Model - data + rules of the game, no reference to view or controller.
 Has only data and logic related to the game, no reference to view or controller.
 
 The logic data of the play session is:
 - increase / decrease score
 - count completed rounds
 - set new high score if true
 
 */
public class PlaySessionModel
{
    public int CompletedRounds { get; private set; }
    public int HighScore { get; private set; }

    private float _currentScore;
    private readonly float _pointGain;
    private readonly float _pointLoss;
    private string _highScoreMessage;

    public PlaySessionModel(float gain, float loss, int highScore) // constructor in C#
    {
        _pointGain = gain;
        _pointLoss = loss;
        HighScore = highScore;
        // _currentScore = 0;
        CompletedRounds = 0;
        _highScoreMessage = "New High Score Achieved!";
    }

    public float MoveResult (bool hasScored)
    {
        // _currentScore += hasScored ? _pointGain : _pointLoss;
        float point = hasScored ? _pointGain : _pointLoss;
        return point;
    }

    public void CompleteRound()
    {
        CompletedRounds++;
    }

    public bool SetNewHighScore()
    {
        if (CompletedRounds > HighScore)
        {
            HighScore = CompletedRounds;
            _highScoreMessage = "New High Score Achieved!";
            return true;
        }
        return false;
    }
}