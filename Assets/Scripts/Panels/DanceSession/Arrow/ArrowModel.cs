using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowModel
{
    public static readonly Dictionary<SwipeID, float> ArrowDirectionMap = new()
                                                                          {
                                                                              { SwipeID.Up, 0f },
                                                                              { SwipeID.Right, -90f },
                                                                              { SwipeID.Down, 180f },
                                                                              { SwipeID.Left, 90f }
                                                                          };

    public enum ArrowType
    {
        Basic,     // average arrow that is removed on swipe
        Breakable, // needs 3 swipes to be removed
        Pressable, // needs x seconds of press
        Opposite,  // needs to be opposite direction to be removed
        Hideable   // is hidden right after spawn 
    }


    public SwipeID ArrowID { get; protected set; }
    public float ArrowDirection { get; protected set; }
    public ArrowType Type { get; protected set; }

    public ArrowModel()
    {
        var randomPair = ArrowDirectionMap.ElementAt(UnityEngine.Random.Range(0, ArrowDirectionMap.Count));
        ArrowID = randomPair.Key;
        ArrowDirection = randomPair.Value;

        // Based on random type we should construct upon the base class with the proper derived class based on type
        Type = (ArrowType)UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(ArrowType)).Length);
        // return randomType switch
        //        {
        //            ArrowType.Basic     => new BasicArrow(directionPair.Key, directionPair.Value),
        //            ArrowType.Breakable => new BreakableArrow(directionPair.Key, directionPair.Value),
        //            ArrowType.Pressable => new PressableArrow(directionPair.Key, directionPair.Value),
        //            ArrowType.Opposite  => new OppositeArrow(directionPair.Key, directionPair.Value),
        //            ArrowType.Hideable  => new HideableArrow(directionPair.Key, directionPair.Value),
        //            _                   => new BasicArrow(directionPair.Key, directionPair.Value)
        //        };
    }

    public bool SwipeSuccess(SwipeID swipeID)
    {
        Debug.Log(swipeID == ArrowID
                      ? $"[ArrowModel] SUCCESS - Swipe:{swipeID} equals ArrowID:{ArrowID}"
                      : $"[ArrowModel] FAIL - Swipe:{swipeID} does not match ArrowID:{ArrowID}");
        return swipeID == ArrowID;
    }
}