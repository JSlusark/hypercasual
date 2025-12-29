using UnityEngine;
// using UnityEngine.UI;

public class LikesBarUI : MonoBehaviour
{
    public float score, target, Width, Height;



    [SerializeField] private RectTransform likesBar;
    [SerializeField] private SpriteRenderer heartIcon;      // SpriteRenderer component
    [SerializeField] private Sprite heartBeat;              // normal heart sprite
    [SerializeField] private Sprite heartBreak;             // broken heart sprite

    public Color successColor = Color.red;
    public Color failColor = Color.gray;


    public void SetStart(float levelScore, float levelTarget)
    {
        score = levelScore; // likes starting point
        target = levelTarget; // likes end point
        Debug.Log(" Score:" + score + " Target:" + target);
    }

    public void UpdateScore(float gain)
    {
        score += gain;
        score = Mathf.Clamp(score, 0f, target);
        float newWidth = (score / target) * Width;
        likesBar.sizeDelta = new Vector2(newWidth, Height);
        var barImage = likesBar.GetComponent<UnityEngine.UI.Image>();

        if (gain < 0)
        {
            heartIcon.sprite = heartBreak;           // change sprite
            // heartIcon.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<UnityEngine.Sprite>("heart_break");
            // likesBar.GetComponent<UnityEngine.UI.Image>().color = failColor;
            barImage.color = failColor;
        }
        else
        {
            heartIcon.sprite = heartBeat;           // change sprite
            // heartIcon.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<UnityEngine.Sprite>("heart_beat");
            // likesBar.GetComponent<UnityEngine.UI.Image>().color = successColor;
            barImage.color = successColor;
        }
        Debug.Log(gain + " Score:" + score);


    }
}
