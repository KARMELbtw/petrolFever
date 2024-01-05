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
}
