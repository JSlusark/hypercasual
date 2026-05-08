using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] ArrowController arrowController;
    private List<ArrowController> _arrowGroup;
    private int _maxArrows; // starts with 3 on difficulty increase reaches max 5 or 6

    private ArrowController _pointedArrow;
    private int _pointedIndex;

    private SwipeController _swipeController; // to subscribe to swipes
    private PlayerInput playerInput;
    // private InputAction danceMoveAction;

    private bool _resultInProgress;
    
    /*On */
    
    
    public event Action<SwipeID, bool/*isMoveSuccess*/, bool/*isSetComplete*/> OnArrowAction; // signals if set was completed or failed
    // public event Action<SwipeID> OnArrowSuccess; // signals what arrow succeded


    private void Awake()
    {
        // Activates controllers for keyboard and swipe
        _swipeController = FindAnyObjectByType<SwipeController>();
        playerInput = GetComponent<PlayerInput>();
        // danceMoveAction = playerInput.actions["DanceMove"];

        // sets up list
        _arrowGroup = new List<ArrowController>();
        StartNewSequence();
    }


    private void OnEnable()
    {
        _swipeController.OnSwipe += HandleDanceMove;
        playerInput.onActionTriggered += OnDanceMove;
        // danceMoveAction.performed += OnDanceMove;
    }

    private void OnDisable()
    {
        _swipeController.OnSwipe -= HandleDanceMove;
        // danceMoveAction.performed -= OnDanceMove; 
        playerInput.onActionTriggered -= OnDanceMove;
    }
    

    private void PointToArrow(int activeIndex)
    {
        _pointedArrow = _arrowGroup[activeIndex];
        _pointedArrow.ShowPointer(true);
    }


    private void StartNewSequence()
    {
        // range is decided based on level, should be dynamic
        _maxArrows = Random.Range(3, 5);
        for (int i = 0; i < _maxArrows; i++)
        {
            ArrowController newArrow = Instantiate(arrowController, this.transform.parent);
            _arrowGroup.Add(newArrow);
        }

        _pointedIndex = 0;
        PointToArrow(_pointedIndex); // sets 
    }


    private IEnumerator WaitResultAnimation(bool showSuccess, System.Action callback)
    {
        _resultInProgress = true;
        foreach (var arrow in _arrowGroup)
        {
            if (showSuccess) arrow.ShowSuccess();
            else arrow.ShowFail();
        }

        // perhaps I can send signal for video bar to stop timer
        yield return new WaitForSeconds(0.5f);
        callback();
        _resultInProgress = false;
    }

    private void RemoveSequence()
    {
        foreach (var arrow in _arrowGroup)
        {
            // Debug.Log($"[ArrowManager] Removed arrow in Sequence ");
            Destroy(arrow.gameObject);
        }

        _arrowGroup.Clear();
    }


    private void ReplaceSequence()
    {
        // OnArrowAction?.Invoke(true);
        StartCoroutine(WaitResultAnimation(
                                           true, () =>
                                           {
                                               RemoveSequence();
                                               StartNewSequence();
                                               PointToArrow(_pointedIndex);
                                           }));
    }

    private void ResetProgress()
    {
        OnArrowAction?.Invoke(0, false, false);
        StartCoroutine(WaitResultAnimation(false, () =>
        {
            foreach (var arrow in _arrowGroup)
            {
                // Debug.Log($"[ArrowManager] Reset Sequence");
                arrow.ShowPointer(false);
            }

            PointToArrow(_pointedIndex = 0);
        }));
    }

    private void AdvanceProgress(SwipeID swipeDirection)
    {
        if (_pointedIndex < (_maxArrows - 1))
        {
            // Debug.Log($"[ArrowManager] Advanced Sublic event ");
            // OnArrowSuccess?.Invoke(swipeDirection);
        OnArrowAction?.Invoke(swipeDirection, true,false);
            
            PointToArrow(++_pointedIndex); // or ++_activeIndex?
        }
        else
        {
            // Debug.Log($"[ArrowManager] Completed  Sequence");
        OnArrowAction?.Invoke(swipeDirection, true, true);
         
            ReplaceSequence();
        }
    }

    private void HandleDanceMove(SwipeID swipeDirection)
    {
        if (_resultInProgress) return;
        if (_pointedArrow.CheckDanceMove(swipeDirection))
        {
            AdvanceProgress(swipeDirection);
        }
        else
        {
            ResetProgress();
        }

        // Debug.Log($"[ArrowManager]active arrow: {_pointedIndex}");
    }


    private void OnDanceMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "DanceMove" || !context.performed) return;
        string inputName = context.control.displayName; // get the name of the control that triggered the action
        if (System.Enum.TryParse(inputName, out SwipeID swipeID))// converts value to enum and returns bool based on condition
            HandleDanceMove(swipeID);
    }
}