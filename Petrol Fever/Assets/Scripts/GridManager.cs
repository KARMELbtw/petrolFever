using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public ChunkGeneration chunkGeneration;

    private int[,] leftGrid;
    private int[,] rightGrid;
    private GameObject[,] topGrid;

    public void InitializeGrids() {
        leftGrid = new int[chunkGeneration.chunkHeight + 1, chunkGeneration.chunkWidth + 1];
        rightGrid = new int[chunkGeneration.chunkHeight + 1, chunkGeneration.chunkDepth + 1];
        topGrid = new GameObject[chunkGeneration.chunkDepth + 1, chunkGeneration.chunkWidth + 1];
    }

    private void Awake() {
        InitializeGrids();
    }

    // Funkcja do stawiania budynku w danym miejscu na jednej z siatek
    public void InitializeBuilding(Vector3 position, Building building) {
        Vector3 worldPosition = position + this.transform.position;
        Debug.Log("Building: " + building.name + " at " + worldPosition);
        GameObject newBuilding = Instantiate(building.prefab, worldPosition, Quaternion.identity);
         for (int x = 0; x < building.depth; x++) { 
             for (int z = 0; z < building.width; z++) { 
                 topSetValue((int)position.x + x,(int)position.z + z, newBuilding);
             }
         }
    }

    // Funkcja do sprawdzenia czy można postawić budynek w danym miejscu
    public bool canPlaceBuilding(int xGrid, int yGrid, Building building) {
        for (int x = 0; x < building.depth; x++) {
            for (int y = 0; y < building.width; y++) {
                if (xGrid + x >= chunkGeneration.chunkWidth || yGrid + y >= chunkGeneration.chunkDepth) {
                    return false;
                }
                if (topGetBuilging(xGrid + x, yGrid + y) != null) {
                    return false;
                }
            }
        }

        return true;
    }

    private void Start() {
        Debug.Log("leftGrid: " + leftGrid.GetLength(0) + " " + leftGrid.GetLength(1));
        Debug.Log("rightGrid: " + rightGrid.GetLength(0) + " " + rightGrid.GetLength(1));
        Debug.Log("topGrid: " + topGrid.GetLength(0) + " " + topGrid.GetLength(1));
        this.gameObject.AddComponent<BoxCollider>();
        this.gameObject.GetComponent<BoxCollider>().size = new Vector3(chunkGeneration.chunkWidth,
            chunkGeneration.chunkHeight, chunkGeneration.chunkDepth);
        float boxColiderOffset = 0.5f;
        this.gameObject.GetComponent<BoxCollider>().center = new Vector3(
            chunkGeneration.chunkWidth / 2 + boxColiderOffset,
            chunkGeneration.chunkHeight / 2 + boxColiderOffset,
            chunkGeneration.chunkDepth / 2 + boxColiderOffset);
    }

    public void leftSetValue(int y, int z, int value) {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        z = (int)(z - position.z);
        if (y < 0 || y >= leftGrid.GetLength(0) || z < 0 || z >= leftGrid.GetLength(1)) {
            Debug.LogError("Index out of bounds in leftSetValue: " + y + " " + z);
            return;
        }

        Debug.Log("leftSetValue: " + y + " " + z + " " + value);
        leftGrid[y, z] = value;
    }

    public void rightSetValue(int y, int x, int value) {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        x = (int)(x - position.x);
        if (y < 0 || y >= rightGrid.GetLength(0) || x < 0 || x >= rightGrid.GetLength(1)) {
            Debug.LogError("Index out of bounds in rightSetValue: " + y + " " + x);
            return;
        }

        Debug.Log("rightSetValue: " + y + " " + x + " " + value);
        rightGrid[y, x] = value;
    }

    public void topSetValue(int x, int z, GameObject building) {
        Vector3 position = this.transform.position;
        if (x < 0 || x >= topGrid.GetLength(0) || z < 0 || z >= topGrid.GetLength(1)) {
            Debug.LogWarning("Index out of bounds in topSetValue: " + x + " " + z);
            return;
        }

        Debug.Log("topSetValue: " + x + " " + z + " " + building);
        topGrid[x, z] = building;
    }

    public GameObject topGetBuilging(int x, int z) {
        Vector3 position = this.transform.position;
        if (x < 0 || x >= topGrid.GetLength(0) || z < 0 || z >= topGrid.GetLength(1)) {
            Debug.LogWarning("Index out of bounds in topGetValue: " + x + " " + z);
            return null;
        }
        return topGrid[x, z];
    }
}