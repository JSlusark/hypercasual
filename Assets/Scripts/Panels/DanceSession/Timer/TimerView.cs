using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void Show(float timer)
    {
        text.text = Mathf.CeilToInt(timer).ToString();
    }
    
}