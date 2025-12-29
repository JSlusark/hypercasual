using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ArrowSlider : MonoBehaviour
{
    public GameObject arrow;
    public int arrowCount = 3;
    // list of arrows currently active
    private List<GameObject> arrowList = new List<GameObject>();
    // starting position (leftmost)
    public Vector3 startPosition = new Vector3(-1f, 3f, 0f);


    // fixed spacing between arrows
    public float spacing = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < arrowCount; i++)
        {
            GameObject newArrow = Instantiate(arrow);
            AddArrow(newArrow);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddArrow(GameObject arrow)
    {
        arrowList.Add(arrow);
        RepositionArrows();
    }

    public void RemoveArrow(GameObject arrow)
    {
        arrowList.Remove(arrow);
        RepositionArrows();
    }

    private void RepositionArrows()
    {
        for (int i = 0; i < arrowList.Count; i++)
        {
            arrowList[i].transform.position =
                startPosition + new Vector3(i * spacing, 0f, 0f);
        }
    }
}
