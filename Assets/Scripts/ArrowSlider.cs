using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;


public class ArrowSlider : MonoBehaviour
{
    private List<GameObject> arrowList = new();
    public GameObject arrow;
    private ArrowCreate firstArrow;
    private int arrowCount = 5;
    // starting position of list
    private Vector3 listPosition = new Vector3(1f, 3f, 0f);
    private float spacing = 1.5f;

    public LikesBarUI likesBar;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < arrowCount; i++)
        {
            GameObject newArrow = Instantiate(arrow);
            newArrow.transform.position = listPosition + new Vector3(i * spacing, 0f, 0f);
            AddArrow(newArrow);
        }
        firstArrow = arrowList[0].GetComponent<ArrowCreate>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddArrow(GameObject arrow)
    {
        arrowList.Add(arrow);
    }

    public void RemoveArrow()
    {
        if (arrowList.Count == 0)
            return;

        GameObject removed = arrowList[0];
        arrowList.RemoveAt(0);
        Destroy(removed);

        if (arrowList.Count > 0)
            firstArrow = arrowList[0].GetComponent<ArrowCreate>();

        GameObject newArrow = Instantiate(arrow);
        AddArrow(newArrow);
        RepositionArrows();


    }


    private void RepositionArrows()
    {
        for (int i = 0; i < arrowList.Count; i++)
        {
            arrowList[i].transform.position =
                listPosition + new Vector3(i * spacing, 0f, 0f);
        }
    }

    public void OnDanceMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("DanceMove pressed: " + context.control.name);
            // firstArrow = arrowList[0].GetComponent<ArrowCreate>();

            if (context.control.name == firstArrow.direction.ToString())
            {
                likesBar.UpdateScore(+1f); // - 1 or multiplier if combo chain active
                Debug.Log("💔 -1  | Score:" + likesBar.score);
                Debug.Log("Matched arrow: " + firstArrow.direction + "point +1");
                RemoveArrow();
            }
            else
            {
                likesBar.UpdateScore(-1f); // - 1 or multiplier if combo chain active
                Debug.Log("💔 -1  | Score:" + likesBar.score);
                Debug.Log("Mismatched arrow. Expected: " + firstArrow.direction + "point -1");
            }
        }

    }
}
