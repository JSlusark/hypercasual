using System;
using SaveSystem.Character;
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

    public void ShowCharacter(CharacterModel character) // characterModel
    {
    
        bool isUnlocked = character.IsUnlocked;

        dancerName.text = isUnlocked ? character.Name : "Locked Dancer";
        danceStyle.text = isUnlocked ? character.Id.ToString() : "Unknown";
        previewImage.sprite = character.RosterSprite;
        previewImage.color = isUnlocked ? Color.white : Color.black;
    }
}