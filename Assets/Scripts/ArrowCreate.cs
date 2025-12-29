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
    public ArrowSlider arrowList;


    private enum Direction
    {
        upArrow,
        rightArrow,
        downArrow,
        leftArrow
    }

    private static readonly Dictionary<Direction, float> ArrowType = new()
    {
        { Direction.upArrow, 0f },
        { Direction.rightArrow, -90f },
        { Direction.downArrow, 180f },
        { Direction.leftArrow, 90f }
    };

    private Direction direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = (Direction)Random.Range(0, 4);
        transform.rotation = Quaternion.Euler(0f, 0f, ArrowType[direction]);
        arrowList.AddArrow(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDanceMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("DanceMove pressed: " + context.control.name);
            if (context.control.name == direction.ToString())
            {
                Debug.Log("Matched arrow: " + direction.ToString());
                isMatching = true;
                // Direction itself is your "ID":
                // - as enum: Direction
                // - as string: Direction.ToString() -> "Up"/"Down"/...
                // - as int: (int)Direction -> 0..3
                Destroy(gameObject);
            }
            else
                Debug.Log("Mismatched arrow. Expected: " + direction.ToString() + ", but got: " + context.control.name);
        }
    }


}
