using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour
{
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text destroyedLinesText;


    public void UpdateStats(int level, int destroyedLines)
    {
        levelText.text = "Level: " + level;
        destroyedLinesText.text = "Destroyed lines: " + destroyedLines;
    }
}
