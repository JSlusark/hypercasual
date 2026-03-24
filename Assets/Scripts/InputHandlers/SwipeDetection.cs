using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumSwipeDistance;
    [SerializeField] private float maximumSwipeTime;
    [SerializeField, Range(0f, 1f)] private float directionThreshold;
    [SerializeField] private GameObject swipePreview;

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;

    private Vector2 endPosition;
    private float endTime;

    private RectTransform swipePreviewRect; // used to keep mapped pixel coords on Canvas 
    private Coroutine swipePreviewCoroutine;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        swipePreviewRect =
            swipePreview.GetComponent<RectTransform>(); // gets RectTransform component from the swipePreviewObject
    }

    private void OnEnable()
    {
        inputManager.onStartTouch += SwipeStart;
        inputManager.onEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.onStartTouch -= SwipeStart;
        inputManager.onEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 touchPosition, float time)
    {
        startPosition = touchPosition;
        startTime = time;
        swipePreviewRect.position = touchPosition;
        swipePreviewCoroutine = StartCoroutine(ShowSwipePreview());
    }

    private void SwipeEnd(Vector2 touchPosition, float time)
    {
        endPosition = touchPosition;
        endTime = time;
        DetectSwipe();
        StopCoroutine(swipePreviewCoroutine);
        Debug.Log($"SwipePreview ended: {swipePreview.transform.position} at time {Time.time}");
    }

    private IEnumerator ShowSwipePreview()
    {
        Debug.Log($"SwipePreview started: {swipePreview.transform.position} at time {Time.time}");
        while (true)
        {
            swipePreviewRect.position = inputManager.PrimaryPosition(); // the preview follows finger movement
            yield return
                null; // coroutines needs to always yield a return to avoid infinite loop that freezes the game (null, waitforseconds, etc) 
        }
    }

    void DetectSwipe()
    {
        float distance =
            Vector2.Distance(endPosition,
                             startPosition); // this value is in raw pixels, perhaps need Screen.height to convert for mobile
        float timeLapse = endTime - startTime;
        // Debug.Log($"Distance: {distance}, Time: {timeLapse}");
        if (distance >= minimumSwipeDistance && timeLapse <= maximumSwipeTime)
        {
            // Debug.Log($"Swipe detected from {startPosition} to {endPosition}");
            Debug.DrawLine(startPosition, endPosition, Color.red, 1f);
            AssignDirection(endPosition - startPosition);
        }
    }

    private void AssignDirection(Vector2 directionRaw)
    {
        Vector2 directionNormalized = directionRaw.normalized;
        if (Vector2.Dot(Vector2.up, directionNormalized) >
            directionThreshold) // Dot returns 1 if exact same direction or -1 if opposite direction 
        {
            Debug.Log($"Swipe Up: {directionNormalized}");
        }
        else if (Vector2.Dot(Vector2.down, directionNormalized) > directionThreshold)
        {
            Debug.Log($"Swipe Down: {directionNormalized}");
        }
        else if (Vector2.Dot(Vector2.right, directionNormalized) > directionThreshold)
        {
            Debug.Log($"Swipe Right: {directionNormalized}");
        }
        else if (Vector2.Dot(Vector2.left, directionNormalized) > directionThreshold)
        {
            Debug.Log($"Swipe Left: {directionNormalized}");
        }
    }
}