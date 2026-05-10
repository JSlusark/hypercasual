using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private RectTransform arrowRect;
    [SerializeField] private RectTransform arrowSprite;
    [SerializeField] private Image arrowImage;      // the arrow
    [SerializeField] private Image backgroundImage; // the container/background of the arrow
    [SerializeField] private Image highlightImage;
    
    [Header("Background Color")]
    [SerializeField] private Color DefaultBackground;
    [Header("Arrow Sprites")]
    [SerializeField] private Sprite FailArrow;
    [SerializeField] private Sprite SuccessArrow;
    [SerializeField] private Sprite DefaultArrow;
    [Header("Colors")]
    [SerializeField] private Color activeHighlight;   // 
    [SerializeField] private Color inactiveHighlight; // 
    [SerializeField] private Color successColor;  // 
    [SerializeField] private Color failedColor; // 
    // [SerializeField] private Color DefaultColor;           // 
    // [SerializeField] private Color ActiveColor;            // 

    private Vector2 _originalPosition;
    private Coroutine _moveCoroutine;

    public void Show(float arrowDirection)
    {
        // Debug.Log("ArrowView Show with direction: " + arrowDirection);
        arrowSprite.localRotation = Quaternion.Euler(0f, 0f, arrowDirection);
        SetDefault(); // sets to opaque
    }

    public void SetHighlight()
    {
        arrowImage.sprite = DefaultArrow;
        highlightImage.color = activeHighlight;
        backgroundImage.color = DefaultBackground;
    }

    public void SetDefault()
    {
        arrowImage.sprite = DefaultArrow;
        highlightImage.color = inactiveHighlight;
        backgroundImage.color = DefaultBackground;
    }

    public void SetSuccess()
    {
        highlightImage.color = successColor;
        backgroundImage.color = successColor;
        arrowImage.sprite = SuccessArrow;
        StartCoroutine(MoveArrow());
    }

    public void SetFail()
    {
        highlightImage.color = failedColor;
        backgroundImage.color = failedColor;
        arrowImage.sprite = FailArrow;
        StartCoroutine(MoveArrow());
    }


    private IEnumerator MoveArrow()
    {
        float distance = 20f;
        Vector2 positionStart = arrowRect.anchoredPosition;
        Vector2 positionPeak = positionStart + Vector2.up * distance;
        float timeElapsed = 0f;
        float timeTotal = 0.2f;

        while (timeElapsed <= timeTotal)
        {
            timeElapsed += Time.deltaTime;
            float timeAmount = timeElapsed / timeTotal;

            if (timeElapsed < timeTotal / 2)
                arrowRect.anchoredPosition = Vector2.Lerp(positionStart, positionPeak, timeAmount);
            else
                arrowRect.anchoredPosition = Vector2.Lerp(positionPeak, positionStart, timeAmount);
            yield return null;
        }
    }
}