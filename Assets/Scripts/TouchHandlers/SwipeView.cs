using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// this scrip can be used for arrowswipe

public class SwipeView : MonoBehaviour, IPointerDownHandler
{
    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 300f;   // max distance before snapping back
    [SerializeField] private float moveSpeed = 8f;        // how fast it moves to target
    [SerializeField] private float returnSpeed = 5f;      // how fast it returns to origin

    private RectTransform _rectTransform;
    private SwipeController _swipeController;
    private bool _isTouched;
    
    private Vector2 _originPosition;
    private Coroutine _moveCoroutine;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _swipeController = FindAnyObjectByType<SwipeController>();
        _originPosition = _rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        _swipeController.OnSwipe += HandleSwipe;
    }

    private void OnDisable()
    {
        _swipeController.OnSwipe -= HandleSwipe;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isTouched = true;
    }

    private void HandleSwipe(SwipeController.SwipeDirection direction)
    {
        if (!_isTouched) return;
        _isTouched = false;

        Vector2 targetPosition = direction switch
        {
            SwipeController.SwipeDirection.Up    => _originPosition + Vector2.up    * moveDistance,
            SwipeController.SwipeDirection.Down  => _originPosition + Vector2.down  * moveDistance,
            SwipeController.SwipeDirection.Right => _originPosition + Vector2.right * moveDistance,
            SwipeController.SwipeDirection.Left  => _originPosition + Vector2.left  * moveDistance,
            _ => _originPosition
        };

        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MoveAndReturn(targetPosition));
    }

    private IEnumerator MoveAndReturn(Vector2 target)
    {
        // move to target
        while (Vector2.Distance(_rectTransform.anchoredPosition, target) > 0.5f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(
                _rectTransform.anchoredPosition,
                target,
                Time.deltaTime * moveSpeed
            );
            yield return null;
        }
        
        _rectTransform.anchoredPosition = target;

        // return to origin
        while (Vector2.Distance(_rectTransform.anchoredPosition, _originPosition) > 0.5f)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(
                _rectTransform.anchoredPosition,
                _originPosition,
                Time.deltaTime * returnSpeed
            );
            yield return null;
        }

        _rectTransform.anchoredPosition = _originPosition;
    }
}