using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
    
    /*
     Should only hold the View logic (state and interaction) for the button. 
     It is attached as a script component to each button in the menu.
     */
    
public class MenuBarView : MonoBehaviour
{
    
    [SerializeField] private ScreenManager.ScreenName screenName;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;
    private Image _image;
    
    // Signal emitted to MenuBarManager when the button is clicked, carries the ScreenName of the button for the manager to handle
    public event Action<ScreenManager.ScreenName> OnButtonClick;
    
    private void Awake()
    {
        _image = GetComponent<Image>(); // reference to the button's image component to change sprite when active
    }
 
    public void OnClick()
    {
        OnButtonClick?.Invoke(screenName); // emits signal with carried ScreeName on click 
    }
    
    public void SetState(ScreenManager.ScreenName activeScreen)
    {

        if (activeScreen == screenName)
        {
            _image.sprite = selectedSprite;
            transform.localScale = new Vector3(1f, 1.15f, 1f);
        }
        else
        {
            _image.sprite = defaultSprite;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
    
}