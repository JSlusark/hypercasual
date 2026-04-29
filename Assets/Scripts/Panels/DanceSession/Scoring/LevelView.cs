using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void Show(int level)
    {
        text.text = level.ToString();
    }
    

}
