using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

/*

consecutive commands might not be best,
fruit ninja for example has calm periods between series of swipes
and would be better to implement that later in anothe branch to
see if it works better for gameplay.

smaller levels have smaller swipes (3 arrows)
bigger levels have longer swipes (max 10 arrows then complicates with special arrows)

 */

public class ArrowSlider : MonoBehaviour
{
    // imported components that will interact with this script
    public LikesBarUI likesBar;
    public GameObject arrow;

    // list item
    private List<GameObject> arrowSequence = new();
    private ArrowCreate firstArrow;
    public int arrowCount = 5;
    // starting position of list
    public Vector3 listPosition = new Vector3(-0.5f, 3f, 0f);
    public float spacing = 1.2f;


    void Start()
    {
        for (int i = 0; i < arrowCount; i++)
        {
            GameObject newArrow = Instantiate(arrow);
            newArrow.transform.position = listPosition + new Vector3(i * spacing, 0f, 0f);
            arrowSequence.Add(newArrow);
        }
        getFirstInSequence();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void getFirstInSequence()
    {
        if (arrowSequence.Count > 0)
        {
            firstArrow = arrowSequence[0].GetComponent<ArrowCreate>();
            firstArrow.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    public void RemoveArrow()
    {
        if (arrowSequence.Count == 0)
            return;
        GameObject removedArrow = arrowSequence[0];
        arrowSequence.RemoveAt(0);
        Destroy(removedArrow);
    }


    private void ContinueArrowSequence()
    {
        getFirstInSequence(); // highlight new first arrow
        GameObject newArrow = Instantiate(arrow);
        arrowSequence.Add(newArrow);
        for (int i = 0; i < arrowSequence.Count; i++)
        {
            arrowSequence[i].transform.position =
                listPosition + new Vector3(i * spacing, 0f, 0f);
        }
    }

    public void OnDanceMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("DanceMove pressed: " + context.control.name);

            if (context.control.name == firstArrow.direction.ToString())
            {
                likesBar.UpdateScore(+20f); // - 1 or multiplier if combo chain active
                Debug.Log("💔 +100  | Score:" + likesBar.score);
                Debug.Log("Matched arrow: " + firstArrow.direction + "point +1");
                RemoveArrow();
                // if max likes is not reached
                ContinueArrowSequence();
                // else level complete (add 1 video, show dancanimation and create a new sequence from scratch)

            }
            else
            {
                likesBar.UpdateScore(-10f); // - 1 or multiplier if combo chain active
                Debug.Log("💔 -50  | Score:" + likesBar.score);
                Debug.Log("Mismatched arrow. Expected: " + firstArrow.direction + "point -1");
            }
        }

    }
}
