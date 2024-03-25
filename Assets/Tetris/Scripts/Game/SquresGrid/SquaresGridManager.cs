using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaresGridManager : MonoBehaviour
{
    public Action onLineDestroyed;
    public Transform[,] squaresGrid;
    public SquaresGridOccupyDetector squaresDetector = new SquaresGridOccupyDetector();
    public StartingConfig startingConfig;

    public int width;
    public int height;
    public int xDifference;
    public int yDifference;

    private List<Vector3> bombToDestroyPositions = new List<Vector3>();
    [SerializeField]
    private Transform leftDownCorner;
    [SerializeField]
    private Transform rightUpCorner;

    void Awake()
    {
        
    }

    public void CreateSquaresGrid()
    {
        width = (int)rightUpCorner.position.x - (int)leftDownCorner.position.x;
        height = (int)rightUpCorner.position.y - (int)leftDownCorner.position.y;
        xDifference = -(int)leftDownCorner.position.x;
        yDifference = -(int)leftDownCorner.position.y;
        squaresGrid = new Transform[width, height];

        squaresDetector.squaresGrid = squaresGrid;
        squaresDetector.width = width;
        squaresDetector.height = height;
        squaresDetector.xDifference = xDifference;
        squaresDetector.yDifference = yDifference;
    }

    public void DestroyFilledLines()
    {
        int destroyedLines = 0;
        int lastDestroyedLine = 0;
        for (int i = height-1; i >=0 ; i--)
        {
            if (IsLineFilled(i) == true)
            {
                destroyedLines++;
                DestroyLine(i);
                lastDestroyedLine = i;
            }
        }
        ExplodeDestroyedBombs();
        if (destroyedLines > 0)
        {
            FallGrid(lastDestroyedLine, destroyedLines);
        }
    }

    private bool IsLineFilled(int numberOfLine)
    {
        for (int i = 0; i < width; i++)
        {
            if (squaresGrid[i, numberOfLine] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DestroyLine(int numberOfLine)
    {
        for (int i = 0; i < width; i++)
        {
            if(squaresGrid[i, numberOfLine].gameObject.GetComponent<BombSquare>() != null)
            {
                bombToDestroyPositions.Add(squaresGrid[i, numberOfLine].position);
            }
            Destroy(squaresGrid[i, numberOfLine].gameObject);
        }
        
        onLineDestroyed?.Invoke();
    }

    private void ExplodeDestroyedBombs()
    {
        for(int i=0; i<bombToDestroyPositions.Count;i++)
        {
            Explosion(bombToDestroyPositions[i], startingConfig.bombRadius);
        }
        bombToDestroyPositions.Clear();
    }
    public void FallGrid(int numberOfLine, int howMuchTime)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = numberOfLine; j < height - howMuchTime; j++)
            {
                if (squaresGrid[i, j + howMuchTime] != null)
                {
                    squaresGrid[i, j] = squaresGrid[i, j + howMuchTime];
                    squaresGrid[i, j].transform.position += new Vector3(0, -howMuchTime, 0);
                }
                else
                {
                    squaresGrid[i, j] = null;
                }
            }
        }
    }

    public void AddToGrid(BlockBehaviour block)
    {
        while (block.transform.childCount > 0)
        {
            Transform square = block.transform.GetChild(0);
            squaresGrid[(int)Math.Floor(square.transform.position.x) + xDifference, (int)Math.Floor(square.transform.position.y) + yDifference] = square;
            square.SetParent(this.transform);
        }
        Destroy(block.gameObject);
    }

    public void Explosion(Vector3 bombPosition, int radius)
    {
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                int xInGrid = (int)Math.Floor(bombPosition.x) + xDifference;
                int yInGrid = (int)Math.Floor(bombPosition.y) + yDifference;
                if (xInGrid + i > 0 && xInGrid + i < width)
                {
                    if (bombPosition.y + yDifference + j > 0 && yInGrid + j < height)
                    {
                        if(squaresGrid[xInGrid + i, (int)yInGrid + j] != null)
                        {
                            Destroy(squaresGrid[xInGrid + i, (int)yInGrid + j].gameObject);
                            squaresGrid[xInGrid + i, (int)yInGrid + j] = null;
                        }
                    }
                }
            }
        }
    }
}
