using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    public KeyCode rotate;
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode moveDownFaster;

    public enum NumberOfPlayer
    {
        First,
        Second
    }
    public void SetControls(NumberOfPlayer numberOfPlayer, bool isDefaultControls = true)
    {
        if (isDefaultControls)
        {
            if (numberOfPlayer == NumberOfPlayer.First)
            {
                SetFirstPlayerDefaultControls();
            }
            else if (numberOfPlayer == NumberOfPlayer.Second)
            {
                SetSecondPlayerDefaultControls();
            }
        }
        else
        {

        }
    }
    private void SetFirstPlayerDefaultControls()
    {
        rotate = KeyCode.W;
        moveRight = KeyCode.D;
        moveLeft = KeyCode.A;
        moveDownFaster = KeyCode.S;
    }

    private void SetSecondPlayerDefaultControls()
    {
        rotate = KeyCode.UpArrow;
        moveRight = KeyCode.RightArrow;
        moveLeft = KeyCode.LeftArrow;
        moveDownFaster = KeyCode.DownArrow;
    }
}
