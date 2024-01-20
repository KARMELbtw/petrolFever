using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private ChunkGeneration chunkGeneration;
    [SerializeField]
    private List<Building> buildings;
    private int currentBuilding = 0;
    
    private int[,] leftGrid;
    private int[,] rightGrid;
    private int[,] topGrid;

    private void Awake() {
        leftGrid = new int[chunkGeneration.chunkHeight + 1, chunkGeneration.chunkWidth + 1];
        rightGrid = new int[chunkGeneration.chunkHeight + 1, chunkGeneration.chunkDepth + 1];
        topGrid = new int[chunkGeneration.chunkDepth + 1, chunkGeneration.chunkWidth + 1];
    }
    
    // funkcja do stawiania budynku po kliknięciu prawym przyciskiem myszy
    private void Update()
{
    if (Input.GetMouseButtonDown(1))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers))
        {
            Debug.Log("Raycast didn't hit anything");
            return;
        }
            // Check if the raycast hit the current GameObject
            if (hitInfo.transform.gameObject != this.gameObject)
            {
                Debug.Log("Raycast hit something else");
                return;
            }

            Vector3 rayHitPosition = hitInfo.point;
            Debug.Log("Position of ray hit: " + rayHitPosition);
            Vector3 chunkPosition = this.transform.position;
            int x = (int)(rayHitPosition.x - chunkPosition.x);
            int y = (int)(rayHitPosition.y - chunkPosition.y);
            int z = (int)(rayHitPosition.z - chunkPosition.z);
            Debug.Log("grid postion of ray: x: " + x + " y: " + y + " z: " + z);
            
            if (x < 0 || x >= topGrid.GetLength(1) || z < 0 ||
                z >= topGrid.GetLength(0))
            {
                Debug.LogError("Index out of bounds in Update: " + x + " " + y + " " + z);
                return;
            }
            
            if (!CanPlaceBuilding(new Vector3(x,y,z)))
            {
                Debug.Log("Building can't be placed on " + this.name + " at " + rayHitPosition + " grid: " + x + " " + z);
                return;
            }

            Debug.Log("Building can be placed on " + this.name + " at " + rayHitPosition + " grid: " + x + " " + z);
            BuildBuilding(rayHitPosition, new Vector3(x + chunkPosition.x, y + chunkPosition.y, z + chunkPosition.z));

    }
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        currentBuilding = 0;
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        currentBuilding = 1;
    }
    
}
    // Funkcja do stawiania budynku w danym miejscu na jednej z siatek
    private void BuildBuilding(Vector3 rayHitPosition, Vector3 alignedPosition)
    {
        alignedPosition.y = this.transform.position.y + chunkGeneration.chunkHeight;
        Instantiate(buildings[currentBuilding].prefab, alignedPosition, Quaternion.identity);
        topSetValue((int)rayHitPosition.x,(int)rayHitPosition.z, buildings[currentBuilding].id);
    }
    
    // Funkcja do sprawdzenia czy można postawić budynek w danym miejscu
    private bool CanPlaceBuilding(Vector3 position)
    {
        int x = (int)position.x, z = (int)position.z;
        return topGrid[x, z] == 0;
    }    
    private void Start()
    {
        Debug.Log("leftGrid: " + leftGrid.GetLength(0) + " " + leftGrid.GetLength(1));
        Debug.Log("rightGrid: " + rightGrid.GetLength(0) + " " + rightGrid.GetLength(1));
        Debug.Log("topGrid: " + topGrid.GetLength(0) + " " + topGrid.GetLength(1));
        this.gameObject.AddComponent<BoxCollider>();
        this.gameObject.GetComponent<BoxCollider>().size = new Vector3(chunkGeneration.chunkWidth, chunkGeneration.chunkHeight, chunkGeneration.chunkDepth);
        float boxColiderOffset = 0.5f;
        this.gameObject.GetComponent<BoxCollider>().center = new Vector3(chunkGeneration.chunkWidth / 2 + boxColiderOffset, chunkGeneration.chunkHeight / 2 + boxColiderOffset + 0.01f, chunkGeneration.chunkDepth / 2 + boxColiderOffset);

    }                        
    public void leftSetValue(int y, int z, int value)
    {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        z = (int)(z - position.z);
        if (y < 0 || y >= leftGrid.GetLength(0) || z < 0 || z >= leftGrid.GetLength(1))
        {
            Debug.LogError("Index out of bounds in leftSetValue: " + y + " " + z);
            return;
        }
        Debug.Log("leftSetValue: " + y + " " + z + " " + value);
        leftGrid[y, z] = value;
    }
    public void rightSetValue(int y, int x, int value)                                                                                                                                                                                          
    {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        x = (int)(x - position.x);
        if (y < 0 || y >= rightGrid.GetLength(0) || x < 0 || x >= rightGrid.GetLength(1))
        {
            Debug.LogError("Index out of bounds in rightSetValue: " + y + " " + x);
            return;
        }
        Debug.Log("rightSetValue: " + y + " " + x + " " + value);
        rightGrid[y, x] = value;
    }
    public void topSetValue(int x, int z, int value)
    {
        Vector3 position = this.transform.position;
        Debug.Log("Setting position for " + this.name + " at "+position.x + " " + position.z);
        x = (int)(x - position.x);
        z = (int)(z - position.z);
        if (x < 0 || x >= topGrid.GetLength(0) || z < 0 || z >= topGrid.GetLength(1))
        {
            Debug.LogError("Index out of bounds in topSetValue: " + x + " " + z);
            return;
        }
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