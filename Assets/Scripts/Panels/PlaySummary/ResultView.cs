using System;
using TMPro;
using UnityEngine;

public class ResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] TextMeshProUGUI ReelText;
    [SerializeField] TextMeshProUGUI FollowersText;
    [SerializeField] TextMeshProUGUI TipsText;

    public void Show(String name, String reels, String followers, String tips)
    {
        titleText.text = $"{name}'s performance";
        ReelText.text = reels;
        FollowersText.text = followers;
        TipsText.text = $"${tips}";
    }
}