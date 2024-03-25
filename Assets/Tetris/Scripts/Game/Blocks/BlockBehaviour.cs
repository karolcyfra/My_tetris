using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public Action<BlockBehaviour> onFallen;
    public Action onOverGameArea;
    public float fallingSpeed;
    public float downKeyAcceleration;
    public Controls controls;

    public Transform leftDownCorner;
    public Transform rightUpCorner;
    public SquaresGridOccupyDetector squaresDetector;

    public float timeToFall;

    public enum Type
    {
        PinkBlock,
        GreenBlock,
        BlueBlock,
        OrangeBlock,
        YellowBlock,
        CyanBlock,
        PurpleBlock,
        Bomb,
        Filler
    }

    public Type type;


    void Start()
    {
        timeToFall = fallingSpeed;

        if(IsInGridOccupied() == true)
        {
            onOverGameArea?.Invoke();
            this.enabled = false;
        }
    }


    void Update()
    {
        MoveOnPlayerInput();

        Fall();
    }

    protected void Fall()
    {
        if (timeToFall <= 0)
        {
            if (IsValidOnDown() == true)
            {
                transform.position += Vector3.down;
                timeToFall = fallingSpeed;
            }
            else
            {
                if (IsOverGameArea() == false)
                {
                    onFallen?.Invoke(this);
                }
                else
                {
                    onOverGameArea?.Invoke();
                }
                this.enabled = false;
            }
        }
        timeToFall -= Time.deltaTime;
    }
    protected void MoveOnPlayerInput()
    {
        if (Input.GetKeyDown(controls.rotate))
        {
            if(IsValidToRotate() == true)
            {
               transform.Rotate(0, 0, -90);
            }
        }
        else if (Input.GetKeyDown(controls.moveRight))
        {
            if(IsValidOnRight() == true)
            {
                transform.position += Vector3.right;
            }
        }
        else if (Input.GetKeyDown(controls.moveLeft))
        {
            if (IsValidOnLeft() == true)
            {
                transform.position += Vector3.left;
            }
        }
        else if (Input.GetKey(controls.moveDownFaster))
        {
            timeToFall -= Time.deltaTime * downKeyAcceleration;
        }

        
    }

    private bool IsValidOnLeft()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            Vector3 movedSquare = transform.GetChild(i).position + Vector3.left;
            if ((movedSquare.x <= leftDownCorner.position.x) || (squaresDetector.IsThisPositionOccupied(movedSquare)) == true)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsValidOnRight()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 movedSquare = transform.GetChild(i).position + Vector3.right;
            if ((movedSquare.x >= rightUpCorner.position.x) || (squaresDetector.IsThisPositionOccupied(movedSquare)) == true)
            {
                return false;
            }
        }
        return true;
    }

    protected bool IsValidOnDown()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 movedSquare = transform.GetChild(i).position + Vector3.down;
            if ((movedSquare.y <= leftDownCorner.position.y) || squaresDetector.IsThisPositionOccupied(movedSquare) == true)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsValidToRotate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform square = transform.GetChild(i);

            Vector3 movedSquare = CalculateSquarePositionAfterRotating(square);
            if ((movedSquare.y > leftDownCorner.position.y) && (movedSquare.x < rightUpCorner.position.x) && (movedSquare.x > leftDownCorner.position.x))
            {
                if (squaresDetector.IsThisPositionOccupied(movedSquare) == true)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private Vector3 CalculateSquarePositionAfterRotating(Transform square)
    {
        Vector3 squareCubeDifference = square.localPosition;
        for (int i = 0; i < (450-transform.eulerAngles.z) / 90; i++)
        {         
            squareCubeDifference = new Vector3(squareCubeDifference.y, -squareCubeDifference.x, squareCubeDifference.z);         
        }
        
        Vector3 position = this.transform.position + squareCubeDifference;
        return position;
    }
    protected bool IsInGridOccupied()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform square = transform.GetChild(i);
            if (square.position.y <= rightUpCorner.position.y)
            {
                if (squaresDetector.IsThisPositionOccupied(square.position) == true)
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected bool IsOverGameArea()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform square = transform.GetChild(i);
            if (square.position.y > rightUpCorner.position.y)
            {         
                return true; 
            }
        }
        return false;
    }
}
