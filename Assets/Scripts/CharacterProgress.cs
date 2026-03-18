/*
 This attribute allows the class to be serialized and shown in the Unity Inspector,
    which is useful for saving/loading character progress data.
*/

[System.Serializable]
public class CharacterProgress
{
    public CharacterData character;
    public bool isUnlocked;
    public int highScore;

    // public bool SetNewHighScore(int newScore)
    // {
    //     if (newScore <= highScore) return false;
    //     highScore = newScore;
    //     return true;
    // }
}