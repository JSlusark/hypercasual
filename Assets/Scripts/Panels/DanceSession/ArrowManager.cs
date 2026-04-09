using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] ArrowController arrowController;
    private List<ArrowController> _arrowGroup;
    private int _maxArrows; // starts with 3 on difficulty increase reaches max 5 or 6

    private ArrowController _pointedArrow;
    private int _pointedIndex;

    private SwipeController _swipeController; // to subscribe to swipes


    private bool _resultInProgress;


    private void Awake()
    {
        Debug.Log("ArrowManager Awake");

        // Activates controllers for keyboard and swipe
        _swipeController = FindAnyObjectByType<SwipeController>();
        // playerInput = GetComponent<PlayerInput>();
        // danceMoveAction = playerInput.actions["DanceMove"];

        // sets up list
        _arrowGroup = new List<ArrowController>();
        StartNewSequence();
    }


    private void OnEnable()
    {
        _swipeController.OnSwipe += HandleDanceMove;
        // danceMoveAction.performed += OnDanceMove;
    }

    private void OnDisable()
    {
        _swipeController.OnSwipe -= HandleDanceMove;
        // danceMoveAction.performed -= OnDanceMove; 
    }

    private void SetActiveArrow(int activeIndex)
    {
        _pointedArrow = _arrowGroup[activeIndex];
        _pointedArrow.ShowPointer(true);
    }


    private void StartNewSequence()
    {
        
        _maxArrows = UnityEngine.Random.Range(3, 6); // range is decided in gameSessionPanel contrller based on how far the session goes
        for (int i = 0; i < _maxArrows; i++)
        {
            ArrowController newArrow = Instantiate(arrowController, this.transform.parent);
            _arrowGroup.Add(newArrow);
        }

        _pointedIndex = 0;
        SetActiveArrow(_pointedIndex); // sets 
    }


    private IEnumerator WaitResultAnimation(bool showSuccess, System.Action callback)
    {
        _resultInProgress = true;
        foreach (var arrow in _arrowGroup)
        {
            if (showSuccess) arrow.ShowSuccess();
            else arrow.ShowFail();
        }
        // perhaps i can send signal for video bar to stop timer

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
        StartCoroutine(WaitResultAnimation(
                                           true, () =>
                                           {
                                               RemoveSequence();
                                               StartNewSequence();
                                               SetActiveArrow(_pointedIndex);
                                           }));
    }

    private void ResetProgress()
    {
        StartCoroutine(WaitResultAnimation(false, () =>
        {
            foreach (var arrow in _arrowGroup)
            {
                // Debug.Log($"[ArrowManager] Reset Sequence");
                arrow.ShowPointer(false);
            }

            ;
            SetActiveArrow(_pointedIndex = 0);
        }));
    }

    private void AdvanceProgress()
    {
        if (_pointedIndex < (_maxArrows - 1))
        {
            // Debug.Log($"[ArrowManager] Advanced Sequence");
            SetActiveArrow(++_pointedIndex); // or ++_actoveIndex?
        }
        else
        {
            // Debug.Log($"[ArrowManager] Completed  Sequence");
            ReplaceSequence();
        }
    }

    private void HandleDanceMove(SwipeID swipeDirection)
    {
        if (_resultInProgress) return;
        if (_pointedArrow.CheckDanceMove(swipeDirection))
        {
            AdvanceProgress();
        }
        else
        {
            ResetProgress();
        }

        // Debug.Log($"[ArrowManager]active arrow: {_pointedIndex}");
    }


    // added here as test to try also keys (it works)
    // private PlayerInput playerInput;
    // private InputAction danceMoveAction;
    // private void OnDanceMove(InputAction.CallbackContext context)
    // {
    //     string inputName = context.control.displayName; // get the name of the control that triggered the action
    //     // Debug.Log("DanceMove performed: " + inputName);
    //     SwipeID swipeID = (SwipeID)System.Enum.Parse(typeof(SwipeID), inputName);
    //     HandleDanceMove(swipeID);
    //     // destroy dance view and controller?
    // }
}