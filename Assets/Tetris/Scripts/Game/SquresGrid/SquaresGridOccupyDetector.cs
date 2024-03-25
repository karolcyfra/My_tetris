using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaresGridOccupyDetector
{
    public Transform[,] squaresGrid;

    public int width;
    public int height;
    public int xDifference;
    public int yDifference;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsThisPositionOccupied(Vector3 position)
    {
       if(squaresGrid[(int)Math.Floor(position.x) + xDifference, (int)Math.Floor(position.y) + yDifference] != null)
       {
            return true;
       }
       return false;
    }
}
