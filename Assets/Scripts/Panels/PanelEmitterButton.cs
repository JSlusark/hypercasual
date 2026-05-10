using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PanelEmitterButton : MonoBehaviour
{
    [Header("Button Info")]
    public PanelID panelID;
    
    [Header("Image components")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Image image;

    public event Action<PanelID> OnPanelEmitterClick;

    public void OnClick()
    {
        Debug.Log($"Menu OnClick {panelID}");
        OnPanelEmitterClick?.Invoke(panelID); // emits signal with button instance that is clicked 
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
        // Debug.Log($"[PanelEmitterButton] {this.name} to highlight");
        image.sprite = selectedSprite;
        transform.localScale = new Vector3(1f, 1.15f, 1f);
    }

    private void SetNormalColor()
    {
        // Debug.Log($"[PanelEmitterButton] {this.name} to normal");
        image.sprite = defaultSprite;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}