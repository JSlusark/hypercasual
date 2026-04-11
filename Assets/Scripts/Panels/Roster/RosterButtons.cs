using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


/*
 * DanceSession component for character card
 */

    public class RosterButtons : MonoBehaviour
    {
        
        public enum Request
        {
            ShowPrevious,
            ShowNext,
            SelectCharacter
        }
        
        public event Action<Request> OnRequestTrigger;
        public Request  buttonRequest;

        
        public void OnButtonClick()
        {
            OnRequestTrigger?.Invoke(buttonRequest);
        }
    
      

    }
