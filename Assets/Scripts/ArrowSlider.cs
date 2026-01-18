using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

/*

consecutive arrowPrefabs might not be best,
fruit ninja for example has calm periods between series of swipes
and would be better to implement that later in anothe branch to
see if it works better for gameplay.

smaller levels have smaller swipes (3 arrows)
bigger levels have longer swipes (max 10 arrows then complicates with special arrows)
need to also see in which context to run different commands (like inverted arrows, diappearing arrows, etc)
 */

public class ArrowSlider : MonoBehaviour
{
    // imported components that will interact with this script
    public VideoBarUI videoBar;
    public GameObject arrowPrefab;
    public float arrowPrefab_spacing = 1.2f;

    // putting them here, but these should be in level manager and change per level
    public float scoreIncrease = 20f;
    public float scoreDecrease = 10f;

    private List<GameObject> arrowPrefabSequence = new();
    public int sequenceSize = 5;
    public Vector3 sequencePosition = new Vector3(-1f, 1.26f, -0.4f);
    private ArrowCreate firstPrefabArrow;


    void Start()
    {
        for (int i = 0; i < sequenceSize; i++)
        {
            GameObject newArrowPrefab = Instantiate(arrowPrefab, transform);
            newArrowPrefab.transform.localPosition = GetArrowTargetPosition(i);
            arrowPrefabSequence.Add(newArrowPrefab);
        }
        getFirstInSequence();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void getFirstInSequence()
    {
        if (arrowPrefabSequence.Count > 0) // put as conditional for safety
            firstPrefabArrow = arrowPrefabSequence[0].GetComponentInChildren<ArrowCreate>();
    }

    public void RemoveArrow()
    {
        if (arrowPrefabSequence.Count == 0) // safety check
            return;
        GameObject removedArrow = arrowPrefabSequence[0];
        arrowPrefabSequence.RemoveAt(0);
        Destroy(removedArrow);
    }



    private void ContinueArrowSequence()
    {
        GameObject newArrowPrefab = Instantiate(arrowPrefab, transform);
        arrowPrefabSequence.Add(newArrowPrefab);
        newArrowPrefab.transform.localPosition = GetArrowTargetPosition(arrowPrefabSequence.Count - 1);
        getFirstInSequence(); // highlights the new first arrow
        StartCoroutine(SlideArrows(0.2f)); // coroutine + interpolation for sliding arrows
    }

    private IEnumerator SlideArrows(float duration = 0.2f)
    {
        float elapsed = 0f;

        // Cache start & target positions
        Vector3[] startPositions = new Vector3[arrowPrefabSequence.Count];
        Vector3[] targetPositions = new Vector3[arrowPrefabSequence.Count];

        for (int i = 0; i < arrowPrefabSequence.Count; i++)
        {
            startPositions[i] = arrowPrefabSequence[i].transform.localPosition;
            targetPositions[i] = GetArrowTargetPosition(i);
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            for (int i = 0; i < arrowPrefabSequence.Count; i++)
            {
                arrowPrefabSequence[i].transform.localPosition =
                    Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            yield return null;
        }

        // Snaps to final position (precision)
        for (int i = 0; i < arrowPrefabSequence.Count; i++)
        {
            arrowPrefabSequence[i].transform.localPosition = targetPositions[i];
        }
    }

    private Vector3 GetArrowTargetPosition(int index)
    {
        return sequencePosition + new Vector3(index * arrowPrefab_spacing, 0f, 0f);
    }



    public void OnDanceMove(InputAction.CallbackContext context)
    {
        Debug.Log("Input received: " + context.control.name);
        if (context.performed)
        {
            // if (firstPrefabArrow == null) // debugging null reference
            // {
            //     Debug.LogWarning("firstPrefabArrow is null (missing ArrowCreate on the first prefab?)");
            //     return;
            // }

            if (context.control.name == firstPrefabArrow.direction.ToString())
            {
                videoBar.UpdateScore(+20f); // can add multiplier if combo chain active
                Debug.Log($"❤️ +100  | Score: {videoBar.score}");
                RemoveArrow();
                ContinueArrowSequence();

            }
            else
            {
                videoBar.UpdateScore(-10f);
                Debug.Log($"💔 -50  | Score: {videoBar.score}");
            }
        }
    }
}