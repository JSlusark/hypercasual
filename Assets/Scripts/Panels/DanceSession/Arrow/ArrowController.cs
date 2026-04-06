using UnityEngine;
using UnityEngine.InputSystem;


public class ArrowController : MonoBehaviour
{
    [SerializeField] private ArrowView arrowView;

    private SwipeController _swipeController; // to subscribe to swipes
    private ArrowModel _arrowModel;



    private void Awake()
    {
        _swipeController = FindAnyObjectByType<SwipeController>();
        playerInput = GetComponent<PlayerInput>();
        danceMoveAction = playerInput.actions["DanceMove"];


        _arrowModel = new ArrowModel();
        arrowView.Show(_arrowModel.ArrowDirection);
    }

    private void OnEnable()
    {
        _swipeController.OnSwipe += HandleDanceMove;
        danceMoveAction.performed += OnDanceMove;
    }

    private void OnDisable()
    {
        _swipeController.OnSwipe -= HandleDanceMove;
        danceMoveAction.performed -= OnDanceMove; 
    }

    private void HandleDanceMove(SwipeID swipeDirection)
    {
        if (_arrowModel.SwipeSuccess(swipeDirection))
            arrowView.ShowSuccess();
        else
            arrowView.ShowFail();
    }

    // added here as test to try also keys (it works)
    private PlayerInput playerInput;
    private InputAction danceMoveAction;
    private void OnDanceMove(InputAction.CallbackContext context)
    {
        string inputName = context.control.displayName; // get the name of the control that triggered the action
        // Debug.Log("DanceMove performed: " + inputName);
        SwipeID swipeID = (SwipeID)System.Enum.Parse(typeof(SwipeID), inputName);
        HandleDanceMove(swipeID);
    }
}
