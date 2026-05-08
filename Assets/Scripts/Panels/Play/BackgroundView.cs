using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] private Image bgSprite;

    public void Show(Sprite bg)
    {
        bgSprite.sprite = bg;
    }
}