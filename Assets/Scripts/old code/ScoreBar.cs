using UnityEngine;

public class ScoreBar : MonoBehaviour
{

    [Tooltip("Score needed to complete a level")]
    public float maxScore = 100f;
    private float score;

    public float Width;
    public float Height;

    [SerializeField] private RectTransform videoBar;
    [SerializeField] private SpriteRenderer videoIcon;      // SpriteRenderer component

    public void SetStart()
    {
        score = 0;
        videoBar.sizeDelta = new Vector2(score, Height);
    }


    public void UpdateLength(float point)
    {
        float newWidth;
        score += point;
        // Debug.Log($"💖 {point} | Score: {score}");
        /*
            NOTE: when score reaches target it does not show full bar,
            instead it starts from 0 again.
            I want to show the full bar before it resets because user completed the level
         */
        score = Mathf.Clamp(score, 0f, maxScore); // unsure if doing something else
        newWidth = (score / maxScore) * Width; // needs to be fixed
        videoBar.sizeDelta = new Vector2(newWidth, Height);
    }

    public bool MaxScoreReached()
    {
        return score >= maxScore;
    }
}
