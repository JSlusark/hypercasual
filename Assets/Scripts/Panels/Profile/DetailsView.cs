using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailsView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text styleText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text followerText;
    [SerializeField] private Image characterSprite;
    [SerializeField] private Image studioSprite;

    // A simple method to update everything at once
    public void Setup(string name, string style, string followerCount, Sprite portrait, Sprite bg)
    {
        styleText.text = $"{style  } Dancer";
        nameText.text = name;
        followerText.text = $"Followers {followerCount}";
        characterSprite.sprite = portrait;
        studioSprite.sprite = bg;
    }
}