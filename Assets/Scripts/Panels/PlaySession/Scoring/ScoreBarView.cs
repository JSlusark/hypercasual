using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarView : MonoBehaviour
{
    [SerializeField] Image barImage;


    public void Show(float increment)
    {
        StartCoroutine(FillBar(increment));
    }
    
    private IEnumerator FillBar(float increment)
    {
        float start = barImage.fillAmount;
        float timeElapsed = 0f;
        float timeTotal = 0.5f;


        while (timeElapsed < timeTotal)
        {
            timeElapsed += Time.deltaTime;
            if (increment > start)
            {
                barImage.fillAmount = Mathf.Lerp(start, increment, timeElapsed / timeTotal);
                yield return null;
            }
            else
            {
                float halfDuration = timeTotal / 2;

                if (timeElapsed < halfDuration)
                {
                    float t = Mathf.Clamp01(timeElapsed / halfDuration);
                    barImage.fillAmount = Mathf.Lerp(start, 1f, t);
                }
                else
                {
                    float t = Mathf.Clamp01((timeElapsed - halfDuration) / halfDuration);
                    barImage.fillAmount = Mathf.Lerp(0f, increment, t);
                }
                yield return null;
            }
        }

        barImage.fillAmount = increment;
    }
}