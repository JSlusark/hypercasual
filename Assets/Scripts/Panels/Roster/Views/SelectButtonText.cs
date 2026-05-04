using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Roster.Views
{
    public class SelectButtonText : MonoBehaviour
    {
        // private CatalogueModel _data;
        [SerializeField] TextMeshProUGUI text;

        private NavigationButtons _button;

        // private Button _button;
        private Image buttonImage;

        private void Awake()
        {
            _button = GetComponentInParent<NavigationButtons>();
            buttonImage = _button.GetComponent<Image>();
        }
        
        public void UpdateText(Character character, bool isActive)
        {
            CharacterConfig c = character.Config;
            CharacterData d = character.Data;
            
            if (d.isUnlocked)
            {
                if (c.id != SaveSystem.Instance.SaveData.activeCharacterID)
                {
                    text.text = "Select";
                    buttonImage.color = Color.cadetBlue;
                }
                else
                {
                    text.text = "Selected";
                    buttonImage.color = Color.green;
                }
            }
            else
            {
                text.text = "$ " + c.costToUnlock.ToString();
                text.color = Color.white;
                buttonImage.color = Color.gray6;
            }
        }
    }
}