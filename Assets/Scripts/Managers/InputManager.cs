using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)] // Makes sure it runs before any other script
public class InputManager : Singleton<InputManager>
{
    //What are regions? They are 

    #region MyRegion

    public delegate void StartTouch(Vector2 touchPosition, float time); // float is for tracking duration of the touch (useful for implementing features like long-press detection)
    public event StartTouch onStartTouch;
    
    public delegate void EndTouch(Vector2 touchPosition, float time);
    public event EndTouch onEndTouch;
    
    #endregion


    private SwipeControls swipeControls;

    protected override void Awake() // overrides base class' awake as we init swipeControls object instance
    {
        base.Awake();
        swipeControls = new SwipeControls();
    }

    void OnEnable()
    {
        swipeControls.Enable();
    }

    void OnDisable()
    {
        swipeControls.Disable();
    }

    void Start()
    {
        swipeControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        swipeControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        Vector2 position = swipeControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        // Debug.Log($"Touch started: {position}");
        if(onStartTouch != null)
            onStartTouch( position, (float)ctx.startTime );
    }

    void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        Vector2 position = swipeControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        // Debug.Log($"Touch ended: {position}");
        if(onEndTouch != null) 
            onEndTouch(position, (float)ctx.time);
    }

    public Vector2 PrimaryPosition()
    {
        return swipeControls.Touch.PrimaryPosition.ReadValue<Vector2>(); // i did not use the utils to convert world view to screen as not 3d, to check if still works
    }
}