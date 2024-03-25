using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombSquare : MonoBehaviour
{
    public event EventHandler<BombEventArgs> OnBombCreated;

    void Start()
    {
        BombEventArgs e = new BombEventArgs();
        OnBombCreated?.Invoke(this, e);
    }
}
