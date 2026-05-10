using System;
using TMPro;
using UnityEngine;

public class ResultView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ReelText;
    [SerializeField] TextMeshProUGUI FollowersText;
    [SerializeField] TextMeshProUGUI TipsText;

    public void Show(String reel, String followers, String tips)
    {
        ReelText.text = reel;
        FollowersText.text = followers;
        TipsText.text = $"${tips}";
    }
}