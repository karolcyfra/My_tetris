using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingConfig
{
    public float startingFallingSpeed = 1;
    public float downKeyAcceleration = 10; // how many times faster block will fall, if downKey is pressed
    public float difficultyGrowing = 0.1f;
    public int nextLevelRequirement = 5; //how many lines must be destroyed to next level
    public int bombRadius = 2;
 }
