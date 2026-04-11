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

    // Perhaps these could be set up differently based on arrowModel.type?
    [SerializeField] private Color HighlightColor; // 
    [SerializeField] private Color DefaultColor;   // 
    [SerializeField] private Color SuccessColor;
    [SerializeField] private Color FailColor;

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
        StartCoroutine(MoveArrow());
    }

    public void SetFail()
    {
        arrowImage.color = FailColor;
        backgroundImage.color = FailColor;
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

            if (timeElapsed < timeTotal / 2)
                arrowRect.anchoredPosition = Vector2.Lerp(positionStart, positionPeak, timeElapsed / timeTotal);
            else
                arrowRect.anchoredPosition = Vector2.Lerp(positionPeak, positionStart, timeElapsed / timeTotal);
            yield return null;
        }
    }
}