using System.Collections;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class SwipeController : MonoBehaviour
{
    public delegate void SwipeEventHandler(SwipeID direction);

    public event SwipeEventHandler OnSwipe; // when swipe is detected, it will invoke this


    [Header("Swipe Settings")] [SerializeField]
    private float minimumSwipeDistance;

    [SerializeField] private float maximumSwipeTime;
    [SerializeField, Range(0f, 1f)] private float directionThreshold;
    // [SerializeField] private SwipeID swipeDirection;

    [Header("References")]
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _endPosition;
    [SerializeField] private float _startTime;
    [SerializeField] private float _endTime;
    // [SerializeField] private GameObject touchIndicator;

    private TouchManager _touchManager;
    private TouchIndicatorView _touchTrail;

    
    
    private void Awake()
    {
        _touchManager = TouchManager.Instance;
        // _touchTrail = touchIndicator.GetComponent<TouchIndicatorView>();
    }


    private void OnEnable()
    {
        _touchManager.OnTouchStart += HandleSwipeStart;
        _touchManager.OnTouchEnd += HandleSwipeEnd;
    }

    private void OnDisable()
    {
        _touchManager.OnTouchStart -= HandleSwipeStart;
        _touchManager.OnTouchEnd -= HandleSwipeEnd;
    }
    
    private void HandleSwipeStart(Vector2 startPosition, float time)
    {
        _startPosition = startPosition;
        _startTime = time;
        // _touchTrail.StartTrail(startPosition);
    }

    private void HandleSwipeEnd(Vector2 endPosition, float time)
    {
        _endPosition = endPosition;
        _endTime = time;
        // _touchTrail.StopTrail();
        DetectSwipe();
    }


    private void DetectSwipe()
    {
        float swipeDistance =
            Vector2.Distance(_endPosition,
                             _startPosition); // this value is in raw pixels, perhaps need Screen.height to convert for mobile
        float elapsedTime = _endTime - _startTime;

        // Debug.Log($"Distance: {distance}, Time: {timeLapse}");
        if (swipeDistance >= minimumSwipeDistance && elapsedTime <= maximumSwipeTime)
        {
            // Debug.Log($"Swipe detected from {startPosition} to {endPosition}");
            Debug.DrawLine(_startPosition, _endPosition, Color.red, 1f);
            NotifySwipeID(_endPosition - _startPosition);
        }
    }

    private void NotifySwipeID(Vector2 rawDirection)
    {
        Vector2 direction = rawDirection.normalized;
        // Dot returns 1 if exact same direction or -1 if opposite direction 
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            OnSwipe?.Invoke(SwipeID.Up);
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            OnSwipe?.Invoke(SwipeID.Down);
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            OnSwipe?.Invoke(SwipeID.Right);
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            OnSwipe?.Invoke(SwipeID.Left);
    }
}