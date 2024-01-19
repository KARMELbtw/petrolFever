using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private ChunkGeneration chunkGeneration;
    
    private int[,] topGrid;
    private int[,] leftGrid;
    private int[,] rightGrid;

    private void Awake()
    {
        topGrid = new int[chunkGeneration.chunkDepth, chunkGeneration.chunkWidth];
        leftGrid = new int[chunkGeneration.chunkHeight, chunkGeneration.chunkWidth];
        rightGrid = new int[chunkGeneration.chunkHeight, chunkGeneration.chunkDepth];
    }

    private void Start()
    {
        Debug.Log("topGrid: " + topGrid.GetLength(0) + " " + topGrid.GetLength(1));
        Debug.Log("leftGrid: " + leftGrid.GetLength(0) + " " + leftGrid.GetLength(1));
        Debug.Log("rightGrid: " + rightGrid.GetLength(0) + " " + rightGrid.GetLength(1));
    }

    public void leftSetValue(int x, int y, int value)
    {
        Debug.Log("leftSetValue: " + x + " " + y + " " + value);
        leftGrid[x, y] = value;
    }
    public void rightSetValue(int x, int y, int value)
    {
        Debug.Log("rightSetValue: " + x + " " + y + " " + value);
        rightGrid[x, y] = value;
    }
    public void topSetValue(int x, int y, int value)
    {
        Debug.Log("topSetValue: " + x + " " + y + " " + value);
        topGrid[x, y] = value;
    }
}
