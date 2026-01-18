using UnityEngine;
using System.Collections;

public class VideoBarUI : MonoBehaviour
{

    public float score;
    [Tooltip("Score needed to complete a level")]
    public float maxScore;
    public float Width;
    public float Height;

    [SerializeField] private RectTransform videoBar;
    [SerializeField] private SpriteRenderer videoIcon;      // SpriteRenderer component
    [SerializeField] private SpriteRenderer dancerSprite;    // dancer sprite  (might need to move to another script later)

    public void SetStart(float levelScore, float levelTarget)
    {
        /*
            - levelScore and levelTarget come from LevelManager (values change at every level)
            - instead of using these values, I should probably just decrease videoBar growth score at every new level
         */
        score = levelScore;
        maxScore = levelTarget;
    }



    public void UpdateScore(float gain)
    {
        float newWidth;
        score += gain;
        /*
            NOTE: when score reaches target it does not show full bar,
            instead it starts from 0 again.
            I want to show the full bar before it resets because user completed the level
         */
        score = Mathf.Clamp(score, 0f, maxScore); // unsure if doing something else
        newWidth = (score / maxScore) * Width; // needs to be fixed
        videoBar.sizeDelta = new Vector2(newWidth, Height);
        Debug.Log(gain + " Score:" + score);
    }

    public IEnumerator showErrorTrigger(Color colorState, Sprite heartState, Sprite dancerState, float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    public void ResetScore()
    {
        score = 0f;
        videoBar.sizeDelta = new Vector2(0f, Height);
    }
}
