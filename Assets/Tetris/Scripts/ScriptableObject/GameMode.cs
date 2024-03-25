using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameMode : ScriptableObject
{
    public float startingFallingSpeed;
    public float downKeyAcceleration; // how many times faster block will fall, if downKey is pressed
    public float difficultyGrowing;
    public int nextLevelRequirement; //how many lines must be destroyed to next level
    public int bombRadius;
}
