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

    public void Show(CharacterModel character, bool isActiveCharacter) // characterModel
    {
        CharacterConfig c = character.Config;
        CharacterData d = character.Data;

        bool isUnlocked = d.isUnlocked;
        dancerName.text = isUnlocked ? c.dancerName : "Locked Dancer";
        danceStyle.text = isUnlocked ? c.id.ToString() : "Unknown";
        previewImage.sprite = c.rosterSprite;
        previewImage.color = isUnlocked ? Color.white : Color.black;
        _background.color = isUnlocked ? Color.deepSkyBlue : Color.gray4;
        // CatalogueModel _data = GameManager.Instance.Catalogue;
        if (isActiveCharacter) _background.color = Color.green;
    }
}