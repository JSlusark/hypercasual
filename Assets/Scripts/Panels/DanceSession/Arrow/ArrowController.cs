using UnityEngine;


public class ArrowController : MonoBehaviour
{
    [SerializeField] private ArrowView arrowView;

    private ArrowModel _arrowModel;


    private void Awake()
    {
        _arrowModel = new ArrowModel();
        arrowView.Show(_arrowModel.ArrowDirection);
    }

    public bool CheckDanceMove(SwipeID swipeDirection)
    {
        if (_arrowModel.SwipeSuccess(swipeDirection))
        {
            ShowSuccess();
            return true;
        }
        else
        {
            ShowFail();
            return false;
        }
    }

    public void ShowPointer(bool isSelected)
    {
        if (isSelected)
            arrowView.SetHighlight();
        else
            arrowView.SetDefault();
    }

    // ArrowController

    public void ShowSuccess()
    {
        arrowView.SetSuccess();
    }

    public void ShowFail()
    {
        arrowView.SetFail();
    }
}