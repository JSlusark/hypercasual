using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void Show(float score)
    {
        text.text = score.ToString();
    }
    
}