using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ArrowCreate : MonoBehaviour
{

    // public float MoveSpeed = 2f;
    // public float diffMax = 1f;
    // public float diffMin = 0f;
    // private bool isCorrect;
    private bool isMatching = false;
    // public ArrowSlider arrowSlider;


    public enum Direction
    {
        upArrow,
        rightArrow,
        downArrow,
        leftArrow
    }

    public static readonly Dictionary<Direction, float> ArrowType = new()
    {
        { Direction.upArrow, 0f },
        { Direction.rightArrow, -90f },
        { Direction.downArrow, 180f },
        { Direction.leftArrow, 90f }
    };

    public Direction direction;
    // private int ID;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = (Direction)Random.Range(0, 4);
        transform.rotation = Quaternion.Euler(0f, 0f, ArrowType[direction]);
        // ID = GetInstanceID();
        // arrowList.AddArrow(gameObject);
        // Debug.Log("COMP First arrow ID: " + arrowList.firstArrowID);
        // Debug.Log("COMP First arrow ID: " + ID);

    }

    // Update is called once per frame
    void Update()
    {

    }



}
