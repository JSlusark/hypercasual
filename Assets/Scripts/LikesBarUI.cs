using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
// using UnityEngine.UI;

public class LikesBarUI : MonoBehaviour
{
    public float score, target, Width, Height;
    public float transitionTime = 0.3f; // global macro for time based transitions

    [SerializeField] private RectTransform likesBar;

    [SerializeField] private SpriteRenderer heartIcon;      // SpriteRenderer component
    [SerializeField] private Sprite heartFull;              // normal heart sprite
    [SerializeField] private Sprite heartBeat;              // normal heart sprite
    [SerializeField] private Sprite heartBreak;             // broken heart sprite

    [SerializeField] private SpriteRenderer dancerSprite;    // dancer sprite for color change feedback
    [SerializeField] private Sprite dancerSuccess;    // dancer sprite change feedback
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
        likesBar.sizeDelta = new Vector2(newWidth, Height);

        Color colorState;
        Sprite heartState;
        Sprite dancerState;

        if (gain < 0)
        {
            colorState = failColor;
            heartState = heartBreak;
            dancerState = dancerFail;
        }
        else
        {
            colorState = successColor;
            heartState = heartBeat;
            dancerState = dancerSuccess;
        }

        StartCoroutine(showErrorTrigger(colorState, heartState, dancerState, transitionTime));

        Debug.Log(gain + " Score:" + score);
    }

    public IEnumerator showErrorTrigger(Color colorState, Sprite heartState, Sprite dancerState, float duration)
    {
        // Could substitute the static state change with actual animation
        var barImage = likesBar.GetComponent<UnityEngine.UI.Image>();
        barImage.color = colorState;
        heartIcon.sprite = heartState;           // change sprite
        heartIcon.color = colorState;
        dancerSprite.sprite = dancerState;

        yield return new WaitForSeconds(duration);

        // Resets to base state for heartIcon and barImage
        heartIcon.sprite = heartFull;
        heartIcon.color = baseColor;
        barImage.color = baseColor;
        dancerSprite.sprite = dancerIdle;
    }

    public void ResetScore()
    {
        score = 0f;
        likesBar.sizeDelta = new Vector2(0f, Height);
    }
}
