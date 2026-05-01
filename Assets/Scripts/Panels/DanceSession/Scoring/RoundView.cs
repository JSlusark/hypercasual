using System.Collections;
using TMPro;
using UnityEngine;

public class RoundView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Vector3 _originalScale;
    
    // AUDIO: added them momentarily here, will have to go to an audio manager at the end
    [SerializeField] private AudioSource audioSource; // drag the component here
    [SerializeField] private AudioClip roundUpClip;   // drag your clip here
    

    public void Awake()
    {
        _originalScale = text.transform.localScale;
    }

    public void Show(int level)
    {
        text.text = level.ToString();
    }

    public void UpdateRound(int level)
    {
        StopAllCoroutines();
        text.transform.localScale = _originalScale; // reset before starting
        StartCoroutine(PunchAnimation(level));
    }
    
    private IEnumerator PunchAnimation(int targetLevel)
    {
        // need to better check once i put these number in a config file for round animation
        float elapsed = 0f;
        float duration = 0.15f;
        int start = int.Parse(text.text);
        Vector3 originalScale = text.transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Debug.Log(elapsed); // "5"
            float t = Mathf.Clamp01(elapsed / duration );
            int current = Mathf.FloorToInt(Mathf.Lerp(start, targetLevel, t));
            // text.text = current.ToString();
            if (current.ToString() != text.text) // only on change
            {
                text.text = current.ToString();
                audioSource.PlayOneShot(roundUpClip);
            }
            text.transform.localScale = Vector3.Lerp(originalScale, originalScale * 6f   , duration/2);
            text.color = Color.Lerp(Color.white, Color.yellow, t);
            yield return null;
        }
        text.color = Color.white;
        text.text = targetLevel.ToString(); 
        text.transform.localScale = originalScale;
    }
        

    }
