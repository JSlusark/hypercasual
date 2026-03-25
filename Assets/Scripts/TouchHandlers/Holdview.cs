using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Holdview : MonoBehaviour
{
    [SerializeField] private float lineWidth = 5f;

    [SerializeField] private RawImage _image;
    [SerializeField] private RectTransform _rectTransform;
    private Coroutine _trackCoroutine;
    private Vector2 _startPosition;

    private void Awake()
    {
        // _rectTransform = GetComponent<RectTransform>();
    }

    public void StartTrail(Vector2 startPosition)
    {
        _image.color = Color.yellow;
        _startPosition = startPosition;
        _rectTransform.position = startPosition;
        _trackCoroutine = StartCoroutine(TrackPosition());
    }

    public void StopTrail()
    {
        _image.color = Color.red;
        if (_trackCoroutine != null)
        {
            StopCoroutine(_trackCoroutine);
            _trackCoroutine = null;
        }
    }

    private IEnumerator TrackPosition()
    {
        while (true)
        {
            Vector2 currentPosition = TouchManager.Instance.GetTouchPosition();
            _rectTransform.position = currentPosition;
            yield return null;
        }
    }

}