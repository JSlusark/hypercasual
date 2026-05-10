using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ArrowGridController : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private RectTransform _rect;

    private int minCol = 5;
    private int _currentCol;

    private void OnRectTransformDimensionsChange()
    { 
        UpdateCellSize(_currentCol);
    }

    public void UpdateCellSize(int arrowCount)
    {
        if(arrowCount > minCol) _currentCol = arrowCount;
        else _currentCol = minCol;
        
        _grid.constraintCount = _currentCol;
        float totalPadding = _grid.padding.left + _grid.padding.right;
        float totalSpacing = _grid.spacing.x * (_currentCol - 1);
        float cellWidth = (_rect.rect.width           - totalPadding - totalSpacing) / _currentCol;

        _grid.cellSize = new Vector2(cellWidth, cellWidth);
    }
}