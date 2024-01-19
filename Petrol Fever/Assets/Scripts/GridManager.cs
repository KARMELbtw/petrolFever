using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private ChunkGeneration chunkGeneration;
    
    private int[,] leftGrid;
    private int[,] rightGrid;
    private int[,] topGrid;

    private void Awake()
    {
        leftGrid = new int[chunkGeneration.chunkHeight + 1, chunkGeneration.chunkWidth + 1];
        rightGrid = new int[chunkGeneration.chunkHeight + 1, chunkGeneration.chunkDepth + 1];
        topGrid = new int[chunkGeneration.chunkDepth + 1, chunkGeneration.chunkWidth + 1];
    }

    private void Start()
    {
        Debug.Log("leftGrid: " + leftGrid.GetLength(0) + " " + leftGrid.GetLength(1));
        Debug.Log("rightGrid: " + rightGrid.GetLength(0) + " " + rightGrid.GetLength(1));
        Debug.Log("topGrid: " + topGrid.GetLength(0) + " " + topGrid.GetLength(1));
    }                        
    public void leftSetValue(int y, int z, int value)
    {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        z = (int)(z - position.z);
        Debug.Log("leftSetValue: " + y + " " + z + " " + value);
        leftGrid[y, z] = value;
    }
    public void rightSetValue(int y, int x, int value)                                                                                                                                                                                          
    {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        x = (int)(x - position.x);
        Debug.Log("rightSetValue: " + y + " " + x + " " + value);
        rightGrid[y, x] = value;
    }
    public void topSetValue(int x, int z, int value)
    {
        Vector3 position = this.transform.position;
        x = (int)(x - position.x);
        z = (int)(z - position.z);
        Debug.Log("topSetValue: " + x + " " + z + " " + value);
        topGrid[x, z] = value;
    }

    // Funkcja do sprawdzenia wokół punktu x,y o sąsiednie komórki o max dystansie distance czy jest jakiś punkt o wartości value
    // jeśli value jest 0 to sprawdza czy wszystkie punkty są o wartości 0
    // parameter side określa czy sprawdzamy lewą, prawą czy górną siatkę
    public bool checkArround(int x, int y, int distance, int side) {
        int[,] grid;
        switch (side) {
            case 0:
                grid = rightGrid;
                break;
            case 1:
                grid = leftGrid;
                break;
            case 2:
                grid = topGrid;
                break;
            default:
                grid = leftGrid;
                break;
        }
        int startX = x - distance;
        int startY = y - distance;
        int endX = x + distance;
        int endY = y + distance;
        if (startX < 0) startX = 0;
        if (startY < 0) startY = 0;
        if (endX >= grid.GetLength(0)) endX = grid.GetLength(0) - 1;
        if (endY >= grid.GetLength(1)) endY = grid.GetLength(1) - 1;
        for (int i = startX; i <= endX; i++) {
            for (int j = startY; j <= endY; j++) {
                if (grid[i, j] == 1) {
                    return true;
                }
            }
        }
        return false;
    }
}