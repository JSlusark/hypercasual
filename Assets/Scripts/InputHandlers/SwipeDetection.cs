using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumSwipeDistance; // minimum distance for a swipe to be registered
    [SerializeField] private float maximumSwipeTime; //maximum time allowed for a swipe to be registered

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;

    private Vector2 endPosition;
    private float endTime;

    private void Awake()
    {
        inputManager = InputManager.Instance;
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
    }

    private void SwipeEnd(Vector2 touchPosition, float time)
    {
        endPosition = touchPosition;
        endTime = time;
        DetectSwipe();
    }

    void DetectSwipe()
    {
        float distance = Vector2.Distance(endPosition, startPosition);
        float timeLapse = endTime - startTime;
        Debug.Log($"Distance: {distance}, Time: {timeLapse}");
        if (distance >= minimumSwipeDistance && timeLapse <= maximumSwipeTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 1f);
            Debug.Log($"Swipe detected from {startPosition} to {endPosition}");
        }
    }
}