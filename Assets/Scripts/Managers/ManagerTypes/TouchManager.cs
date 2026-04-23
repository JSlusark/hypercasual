using System;
using UnityEngine;
using UnityEngine.InputSystem;


[DefaultExecutionOrder(-1)] // Makes sure it runs before any other script
public class TouchManager : Manager<TouchManager>
{
    // Delegates describe an event's signature, used to avoid writing Action<Vector2, float>
    public delegate void TouchEventHandler(Vector2 position, float time);

    // Type of event that others subscribe to, from same delegate type as they share signature, "On" is used to refer "when On"
    public event TouchEventHandler OnTouchStart; // when touch action is started
    public event TouchEventHandler OnTouchEnd;   // when touch action is canceled


    private TouchInputActions touchControl;

    [Header("Touch Data")] [SerializeField]
    private Vector2 startPosition; // makes sense to me to keep it here as used multiple times in diff methods

    [SerializeField]
    private Vector2 endPosition; // makes sense to me to keep it here as used multiple times in diff methods

    [SerializeField]
    private Vector2 currPosition; // makes sense to me to keep it here as used multiple times in diff methods


    protected override void Awake() // overrides base class' awake as we init touchControl object instance
    {
        base.Awake();
        touchControl = new TouchInputActions();
    }

    void OnEnable()
    {
        var touchAction =
            touchControl.Touch
                        .TouchContact; // stores the TouchContact action, which has signature Action<InputAction.CallbackContext> by default

        touchControl.Enable(); // activates the TouchInputActions system so it can start listening for its input
        touchAction.started += HandleTouch;
        touchAction.canceled += HandleTouch;
    }

    void OnDisable()
    {
        var touchAction =
            touchControl.Touch
                        .TouchContact; // stores the TouchContact action, which has signature Action<InputAction.CallbackContext> by default

        touchControl.Disable(); // deactivates 
        touchAction.started -= HandleTouch;
        touchAction.canceled -= HandleTouch;
    }

    void HandleTouch(InputAction.CallbackContext ctx) // Takes touchPosition on trigger
    {
        // Debug.Log($"Touch started: {touchPosition}");
        if (ctx.started)
        {
            startPosition = touchControl.Touch.TouchPosition.ReadValue<Vector2>();
            OnTouchStart?.Invoke(startPosition, (float)ctx.startTime);
        }
        else if (ctx.canceled)
        {
            endPosition = touchControl.Touch.TouchPosition.ReadValue<Vector2>();
            OnTouchEnd?.Invoke(endPosition, (float)ctx.time);
        }
    }


    public Vector2 GetTouchPosition()
    {
        currPosition = touchControl.Touch.TouchPosition.ReadValue<Vector2>();
        return currPosition;
    }
}