using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerBlock : BlockBehaviour
{

    private bool CanMoveOnPlayerInput = true;
    void Update()
    {
        if (CanMoveOnPlayerInput == true)
        {
            MoveOnPlayerInput();
        }
        else
        {
            timeToFall -= Time.deltaTime * downKeyAcceleration;
        }

        Fall();
    }

    private new void Fall()
    {
        if (timeToFall <= 0)
        {
            if (IsValidOnDown() == true || (IsFreeSpaceBelow() == true))
            {
                transform.position += Vector3.down;
                timeToFall = fallingSpeed;

                if((IsValidOnDown() == false))
                {
                    CanMoveOnPlayerInput = false;
                }

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

    private bool IsFreeSpaceBelow()
    {
        Vector3 squarePosition = transform.GetChild(0).position + Vector3.down;
        for (int i = 0; i < squarePosition.y; i++)
        {
            Vector3 spaceBelow = new Vector3(squarePosition.x, i, squarePosition.z);
            if (squaresDetector.IsThisPositionOccupied(spaceBelow) == false)
            { 
                return true;
            }
        }
        return false;
    }
}
