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
    [SerializeField]private Image _background;

    private void Awake()
    {
    }

    public void ShowCharacter(CharacterModel character) // characterModel
    {
    
        bool isUnlocked = character.IsUnlocked;
        dancerName.text = isUnlocked ? character.Name : "Locked Dancer";
        danceStyle.text = isUnlocked ? character.Id.ToString() : "Unknown";
        previewImage.sprite = character.RosterSprite;
        previewImage.color = isUnlocked ? Color.white : Color.black;
        _background.color = isUnlocked ? Color.deepSkyBlue : Color.gray4;
        DatabaseModel _data = GameManager.Instance.Database;
        if (character.Id == _data.Data.activeCharacterId)
        {
            _background.color = Color.green;
        }


    }
}