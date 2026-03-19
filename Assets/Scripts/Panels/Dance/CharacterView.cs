using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Image characterSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        ShowIdle(characterSprite.sprite);
    }

    public void ShowIdle(Sprite sprite)
    {
            characterSprite.sprite = sprite;
    }
}
