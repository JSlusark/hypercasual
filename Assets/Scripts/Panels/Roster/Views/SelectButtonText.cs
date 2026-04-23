using System;
using SaveSystem.Character;
using TMPro;
using UnityEngine;

namespace Panels.Roster.Views
{
    public class SelectButtonText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;

        public void UpdateText(CharacterModel character)
        {
            if (character.IsUnlocked)
            {
                text.text = "Select";
            }
            else
                text.text = "$ " + character.CostToUnlock.ToString();
        }
    }
}