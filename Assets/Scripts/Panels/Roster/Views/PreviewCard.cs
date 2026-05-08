using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/*
 * DanceSession component for character card
 */

public class PreviewCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dancerName;
    [SerializeField] private Image previewImage;
    [SerializeField] private TextMeshProUGUI danceStyle;
    [SerializeField]private Image _background;

    private void Awake()
    {
    }

    public void Show(Character pointedCharacter, bool isActiveCharacter) // characterModel
    {
        CharacterConfig config = pointedCharacter.Config;
        CharacterData data = pointedCharacter.Data;
        
        dancerName.text = data.isUnlocked ? config.name : "Locked Dancer";
        danceStyle.text = data.isUnlocked ? config.id.ToString() : "Unknown";
        previewImage.sprite = config.rosterSprite;
        previewImage.color = data.isUnlocked ? Color.white : Color.black;
        _background.color = data.isUnlocked ? Color.deepSkyBlue : Color.gray4;
        
        if (isActiveCharacter) _background.color = Color.green;
    }
}