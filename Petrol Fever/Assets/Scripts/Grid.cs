using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _height;
    private int _width;
    private int[,] _gridArray;
    private float _cellSize;

    public Grid(int height, int width, float cellSize)
    {
        _height = height;
        _width = width;
        _cellSize = cellSize;

        _gridArray = new int[width, height];
        
        Debug.Log("Grid created: "+width+" "+height);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y>= 0 && value>=0)
        {
            _gridArray[x, y] = value;
        }
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0)
        {
            return _gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }
}
