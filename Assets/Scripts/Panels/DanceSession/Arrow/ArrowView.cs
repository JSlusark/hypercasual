using UnityEngine;
using UnityEngine.UI;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private RectTransform arrowSprite;
    [SerializeField] private Image arrowImage;      // the arrow
    [SerializeField] private Image backgroundImage; // the container/background of the arrow
    
    // Perhaps these could be set up differently based on arrowModel.type?
    [SerializeField] private Color HighlightColor; // 
    [SerializeField] private Color DefaultColor; // 
    [SerializeField] private Color SuccessColor;
    [SerializeField] private Color FailColor;


    public void Show(float arrowDirection)
    {
        // Debug.Log("ArrowView Show with direction: " + arrowDirection);
        arrowSprite.localRotation = Quaternion.Euler(0f, 0f, arrowDirection);
        SetDefault(); // sets to opaque
    }

    public void SetHighlight()
    {
        arrowImage.color = HighlightColor;
        backgroundImage.color = HighlightColor;
    }

    public void SetDefault()
    {
        arrowImage.color = DefaultColor;
        backgroundImage.color = DefaultColor;
    }

    public void SetSuccess()
    {
        arrowImage.color = SuccessColor;
        backgroundImage.color = SuccessColor;
    }

    public void SetFail()
    {
        arrowImage.color = FailColor;
        backgroundImage.color = FailColor;
    }
}