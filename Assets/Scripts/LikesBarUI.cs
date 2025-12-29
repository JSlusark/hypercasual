using UnityEngine;
// using UnityEngine.UI;

public class LikesBarUI : MonoBehaviour
{
    public float Likes, MaxLikes, Width, Height;


    [SerializeField]
    private RectTransform likesBar;

    public void setMaxLikes(float maxLikes)
    {
        MaxLikes = maxLikes;
        // Likes = MaxLikes;
        // UpdateLikesBar();
    }

    public void updateLikes(float updatedScore)
    {
        Likes = updatedScore;
        float newWidth = (Likes / MaxLikes) * Width;
        likesBar.sizeDelta = new Vector2(newWidth, Height);
        // UpdateLikesBar();
    }
}
