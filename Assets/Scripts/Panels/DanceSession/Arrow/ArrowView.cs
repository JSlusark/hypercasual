using UnityEngine;
using UnityEngine.UI;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private RectTransform arrowSprite;
    [SerializeField] private Image arrowImage; // the arrow
    [SerializeField] private Image backgroundImage; // the container/background of the arrow
   
    public void Show(float arrowDirection)
    {
        arrowSprite.localRotation = Quaternion.Euler(0f, 0f, arrowDirection);
    }
    
    public void ShowSuccess()
    {
        arrowImage.color = Color.green;
    }
    
    public void ShowFail()
    {
        arrowImage.color = Color.red;
    }
    
    public void Remove()
    {
        Destroy(gameObject);
    }
    

}
