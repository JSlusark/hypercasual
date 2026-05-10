using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/*
 * DanceSession component for character card
 */

public class PreviewCard : MonoBehaviour
{
    [SerializeField] private GameObject info;
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [SerializeField] private GameObject lockedInfo;
    [SerializeField] private TextMeshProUGUI unlockCostText;
    
    [SerializeField] private Image previewImage;
    [SerializeField]private Image _background;
    

    public void Show(Character pointedCharacter, bool isActiveCharacter) // characterModel
    {
        
        
        CharacterConfig config = pointedCharacter.Config;
        CharacterData data = pointedCharacter.Data;

        
        if (data.isUnlocked)
        {
            info.SetActive(true);
            lockedInfo.SetActive(false);
            nameText.text = config.name;
            idText.text = config.id.ToString() + " dancer";
        }
        else
        {
            info.SetActive(false);
            lockedInfo.SetActive(true);
            unlockCostText.text = config.costToUnlock.ToString();
        }

        previewImage.sprite = config.rosterSprite;
        previewImage.color = data.isUnlocked ? Color.white : Color.black;
        _background.color = data.isUnlocked ? Color.deepSkyBlue : Color.gray4;
        
        if (isActiveCharacter) _background.color = Color.green;
    }
    
    
}