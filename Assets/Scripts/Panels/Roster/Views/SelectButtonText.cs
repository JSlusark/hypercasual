using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Roster.Views
{
    public class SelectButtonText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] private NavigationButtons selectButton;

        private Image _buttonImage;

        private void Awake()
        {
            _buttonImage = selectButton.GetComponent<Image>();
        }
        
        // public void UpdateText(Character character)
        // {
        //     CharacterConfig config = character.Config;
        //     CharacterData data = character.Data;
        //     
        //     if (data.isUnlocked)
        //     {
        //         if (config.id != SaveSystem.Instance.SaveData.activeCharacterID)
        //         {
        //             text.text = "Select";
        //             _buttonImage.color = Color.cadetBlue;
        //         }
        //         else
        //         {
        //             text.text = "Selected";
        //             _buttonImage.color = Color.green;
        //         }
        //     }
        //     else
        //     {
        //         text.text = "$ " + config.costToUnlock.ToString();
        //         text.color = Color.white;
        //         _buttonImage.color = Color.gray6;
        //     }
        // }
    }
}