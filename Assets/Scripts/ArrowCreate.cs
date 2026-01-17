using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ArrowCreate : MonoBehaviour
{
    // private bool isMatching = false;
    public float Width = 1f;
    public float Height = 1f;


    public enum Direction
    {
        upArrow,
        rightArrow,
        downArrow,
        leftArrow
    }
    public Direction direction;

    public static readonly Dictionary<Direction, float> ArrowType = new()
    {
        { Direction.upArrow, 0f },
        { Direction.rightArrow, -90f },
        { Direction.downArrow, 180f },
        { Direction.leftArrow, 90f }
    };



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = (Direction)Random.Range(0, 4);
        transform.rotation = Quaternion.Euler(0f, 0f, ArrowType[direction]);
        Debug.Log($"Arrow direction:[{direction}] angle:{ArrowType[direction]}");

    }

    // Update is called once per frame
    void Update()
    {

    }



}
