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
        
    }
}