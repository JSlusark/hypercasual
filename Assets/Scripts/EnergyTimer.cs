using UnityEngine;

public class EnergyTimer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float score, target, Width, Height;
    public float transitionTime = 0.3f; // global macro for time based transitions

    [SerializeField] private RectTransform videoBar;

    [SerializeField] private SpriteRenderer videoIcon;      // SpriteRenderer component
    [SerializeField] private Sprite videoScatter;             // broken heart sprite
    [SerializeField] private Sprite videoBreak;             // broken heart sprite
    [SerializeField] private Sprite videoFail;             // broken heart sprite


    [SerializeField] private SpriteRenderer dancerSprite;    // dancer sprite for color change feedback
    [SerializeField] private Sprite dancerFail;    // dancer sprite change feedback
    [SerializeField] private Sprite dancerIdle;    // dancer sprite change feedback
    [SerializeField] private Sprite dancerAnimation;    //idle animation or completion animation

    public Color baseColor = Color.red;
    public Color successColor = Color.green;
    public Color failColor = Color.gray;


    public void SetStart(float levelScore, float levelTarget)
    {
        score = levelScore; // likes starting point
        target = levelTarget; // likes end point
        // Debug.Log(" Score:" + score + " Target:" + target);
    }



    public void UpdateScore(float gain)
    {
        float newWidth;
        score += gain;

        // if (score >= target) // resets score if over target
        //     return;
        // // score = 0f;

        score = Mathf.Clamp(score, 0f, target); // unsure if doing something else
        newWidth = (score / target) * Width; // needs to be fixed
        videoBar.sizeDelta = new Vector2(newWidth, Height);

        // Color colorState;
        // Sprite heartState;
        // Sprite dancerState;

        if (gain < 0)
        {
            // colorState = failColor;
            // heartState = videoScatter;
            // dancerState = dancerFail;

        }
        // else
        // {
        //     colorState = successColor;
        //     heartState = heartBeat;
        //     dancerState = dancerSuccess;
        // }

        // StartCoroutine(showErrorTrigger(colorState, heartState, dancerState, transitionTime));

        Debug.Log(gain + " Score:" + score);
    }

    public void ResetScore()
    {
        score = 0f;
        videoBar.sizeDelta = new Vector2(0f, Height);
    }
}
