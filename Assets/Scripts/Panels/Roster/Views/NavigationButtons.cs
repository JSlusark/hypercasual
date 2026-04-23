using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


/*
 * DanceSession component for character card
 */

    public class NavigationButtons : MonoBehaviour
    {
        
        public enum Request
        {
            ShowPrevious,
            ShowNext,
            SelectCharacter
        }
        
        public event Action<Request> OnNavigationButton;
        [SerializeField] private Request  buttonRequest;

        
        private void OnButtonClick()
        {
            OnNavigationButton?.Invoke(buttonRequest);
        }

    }
