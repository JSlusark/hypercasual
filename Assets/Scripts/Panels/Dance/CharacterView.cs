using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Image characterSprite;
    
    private void Start()
    {
        ShowIdle(characterSprite.sprite); // fallback?
    }

    public void ShowIdle(Sprite sprite)
    {
            characterSprite.sprite = sprite;
    }
}
