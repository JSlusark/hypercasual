using System;
using UnityEngine;
using UnityEngine.UI;
    
    /*
     Should only hold the View logic (state and interaction) for the button. 
     It is attached as a script component to each button in the menu.
     */
    
public class PanelEmitterButton : MonoBehaviour
{
    
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;
    
    [SerializeField] private Image image;
    
    public event Action<PanelEmitterButton> OnMenuButtonClick;
    
    
    public void OnClick()
    {
        OnMenuButtonClick?.Invoke(this); // emits signal with button instance that is clicked 
    }


    public void Select()
    {
        // Debug.Log($"[BUTTON] {this.name} to highlight");
        SetHighlightColor();
    }
    
    public void Deselect()
    {
        // Debug.Log($"[BUTTON] {this.name} to normal");
        SetNormalColor();
    }

    private void SetHighlightColor()
    {
        image.sprite = selectedSprite;
        transform.localScale = new Vector3(1f, 1.15f, 1f);
    }
    
    private void SetNormalColor()
    {
        image.sprite = defaultSprite;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }


}