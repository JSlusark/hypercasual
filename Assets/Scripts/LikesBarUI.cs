using UnityEngine;
// using UnityEngine.UI;

public class LikesBarUI : MonoBehaviour
{
    public float score, target, Width, Height;



    [SerializeField]
    private RectTransform likesBar;

    public void SetStart(float levelScore, float levelTarget)
    {
        score = levelScore; // likes starting point
        target = levelTarget; // likes end point
        Debug.Log(" Score:" + score + " Target:" + target);
        // UpdateLikesBar();
    }

    public void UpdateScore(float gain)
    {
        score += gain;
        score = Mathf.Clamp(score, 0f, target);
        float newWidth = (score / target) * Width;
        likesBar.sizeDelta = new Vector2(newWidth, Height);
        Debug.Log(gain + " Score:" + score);
        // UpdateLikesBar();
    }
}
