using System;
using SaveSystem.Character;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Roster.Views
{
    public class SelectButtonText : MonoBehaviour
    {
        private DatabaseModel _data;
        [SerializeField] TextMeshProUGUI text;

        private NavigationButtons _button;

        // private Button _button;
        private Image buttonImage;

        private void Awake()
        {
            _button = GetComponentInParent<NavigationButtons>();
            buttonImage = _button.GetComponent<Image>();
            _data = GameManager.Instance.Database;
        }
        
        public void UpdateText(CharacterModel character)
        {
            if (character.IsUnlocked)
            {
                if (character.Id != _data.GetActiveCharacter().Id)
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
                text.text = "$ " + character.CostToUnlock.ToString();
                text.color = Color.white;
                buttonImage.color = Color.gray6;
            }
        }
    }
}