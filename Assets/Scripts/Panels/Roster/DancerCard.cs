using UnityEngine;
using TMPro;
using UnityEngine.UI;


/*
 * DanceSession component for character card
 */

    public class DancerCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dancerName;
        [SerializeField] private Image previewImage;
        [SerializeField] private TextMeshProUGUI danceStyle;
        
        public void ShowCharacter(CharacterData data)
        {
            dancerName.text = data.dancerName;
            danceStyle.text = data.danceStyle;
            previewImage.sprite = data.rosterSprite;
        }
        
    }
