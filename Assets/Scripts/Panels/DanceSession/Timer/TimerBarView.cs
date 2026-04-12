using UnityEngine;
using UnityEngine.UI;

public class TimerBarView : MonoBehaviour
{
    [SerializeField] Image barImage;
    
    public void UpdateFill(float timer, float maxTimer)
    {
        // Fill amount ranges from min 0 and max 1
        barImage.fillAmount = timer/maxTimer;
    }

}
