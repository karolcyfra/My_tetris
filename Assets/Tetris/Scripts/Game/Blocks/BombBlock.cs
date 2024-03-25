using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BombBlock : BlockBehaviour
{
    public BombSquare bombSquare;

    private void Start()
    {
        bombSquare.OnBombCreated += TestEvent;
    }

    public void TestEvent(object bombSquare, BombEventArgs e)
    {
        Debug.Log("Bomb is on the screen");
        Debug.Log(bombSquare.GetType() + " pi: " + e.pi);
    }

    void Update()
    {
        MoveOnPlayerInput();

        Fall();
    }
}
