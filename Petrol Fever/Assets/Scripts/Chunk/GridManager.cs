using System;
using System.Collections;
using System.Collections.Generic;
using Oil;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public ChunkGeneration chunkGeneration;

    private GameObject[,] leftGrid;
    private GameObject[,] rightGrid;
    private GameObject[,] topGrid;

    public void InitializeGrids() {
        leftGrid = new GameObject[chunkGeneration.chunkHeight, chunkGeneration.chunkWidth + 1];
        rightGrid = new GameObject[chunkGeneration.chunkHeight, chunkGeneration.chunkDepth + 1];
        topGrid = new GameObject[chunkGeneration.chunkDepth + 1, chunkGeneration.chunkWidth + 1];
    }

    private void Awake() {
        InitializeGrids();
    }

    // Funkcja do stawiania budynku w danym miejscu
    public void InitializeTopBuilding(Vector3 position, BuildingTemplate buildingTemplate) {
        // Pozycja świata
        Vector3 worldPosition = position + this.transform.position;
        Debug.Log("Building: " + buildingTemplate.name + " at " + worldPosition);
        // Stworzenie nowego budynku
        GameObject newBuilding = Instantiate(buildingTemplate.prefab, worldPosition, Quaternion.identity);
        
        // Przypisanie rodzica i nazwy
        newBuilding.transform.parent = this.transform.GetChild(0);
        newBuilding.name = buildingTemplate.name + " " + position.x + " " + position.z;
        
        // Przypisania parametrów budynku do skryptu BuildingScript
        BuildingScript newBuildingSript = newBuilding.AddComponent<BuildingScript>();
        newBuildingSript.originGrid = new Vector2(position.x, position.z);
        newBuildingSript.width = buildingTemplate.width;
        newBuildingSript.depth = buildingTemplate.depth;
        newBuildingSript.buildingName = buildingTemplate.buildingName;
        newBuildingSript.price = buildingTemplate.price;

        if (!buildingTemplate.mustPlaceOnTop)
            Debug.LogError("Building " + buildingTemplate.name + " must be placed on side, not on top!");
        for (int x = 0; x < buildingTemplate.depth; x++) {
            for (int z = 0; z < buildingTemplate.width; z++) {
                topSetValue((int)position.x + x, (int)position.z + z, newBuilding);
            }
        }
    }
    public void InitializeDeer(Vector3 position, BuildingTemplate deer) {
        // Pozycja świata
        Vector3 worldPosition = position + this.transform.position;
        Debug.Log("Deer at " + worldPosition);
        // Stworzenie nowego budynku
        GameObject newDeer = Instantiate(deer.prefab, worldPosition, Quaternion.identity);
        
        // Przypisanie rodzica i nazwy
        newDeer.transform.parent = this.transform.GetChild(0);
        newDeer.name = "Deer " + position.x + " " + position.z;
    }
    public void InitializeSideBuilding(Vector3 position, BuildingTemplate buildingTemplate) {
        bool onLeft = position.x == 0 && position.z != 0;
        // Pozycja świata
        Vector3 worldPosition = position + this.transform.position;
        Debug.Log("Building: " + buildingTemplate.name + " at " + worldPosition);
        // Stworzenie nowego budynku
        Vector3 offset = new Vector3(0,0.51f,0);
        if (onLeft) {
            offset += new Vector3(0,0,0.5f);
        } else {
            offset += new Vector3(0.5f,0,0);
        }
        GameObject newBuilding = Instantiate(buildingTemplate.prefab, worldPosition - offset, Quaternion.identity);
        
        // Przypisanie rodzica i nazwy
        newBuilding.transform.parent = this.transform.GetChild(0);
        newBuilding.name = buildingTemplate.name + " " + position.x + " " + position.z + " " + position.y;
        
        // Przypisania parametrów budynku do skryptu BuildingScript
        BuildingScript newBuildingSript = newBuilding.AddComponent<BuildingScript>();
        newBuildingSript.originGrid = (onLeft) ? new Vector2(position.y, position.z) : new Vector2(position.y, position.x);

        newBuildingSript.width = buildingTemplate.width;
        newBuildingSript.depth = buildingTemplate.depth;
        newBuildingSript.buildingName = buildingTemplate.buildingName;
        newBuildingSript.price = buildingTemplate.price;
        
        if (onLeft) {
            for (int y = 0; y < buildingTemplate.depth; y++) { 
                for (int z = 0; z < buildingTemplate.width; z++) { 
                    leftSetValue((int)position.y + y,(int)position.z + z + (int)this.transform.position.z, newBuilding);
                }
            }
        } else {
            for (int y = 0; y < buildingTemplate.depth; y++) { 
                for (int x = 0; x < buildingTemplate.width; x++) { 
                    rightSetValue((int)position.y + y,(int)position.x + x + (int)this.transform.position.x, newBuilding);
                }
            }
        }

        if (onLeft) {
            GameObject blockUnder = leftGetValue((int)position.y-1, (int)(position.z + transform.position.z - 1));
            if (blockUnder != null) {
                var parent = blockUnder.transform.parent;
                if (blockUnder.GetComponent<idScript>().id == 1) {
                    Debug.Log("Ropa wykryta "+position.x+" "+(position.y - 1)+" "+position.z+" "+blockUnder.name+" "+parent.name+" "+parent.GetComponent<oilLogic>().name);
                    parent.GetComponent<oilLogic>().startDrilling();
                }
                parent.GetComponent<showingOreScript>().showOre();
            }
        } else {
            GameObject blockUnder = rightGetValue((int)position.y-1, (int)(position.x + transform.position.x - 1));
            if (blockUnder != null) {
                var parent = blockUnder.transform.parent;
                if (blockUnder.GetComponent<idScript>().id == 1) {
                    Debug.Log("Ropa wykryta "+position.x+" "+(position.y - 1)+" "+position.z+" "+blockUnder.name+" "+parent.name+" "+parent.GetComponent<oilLogic>().name);
                    parent.GetComponent<oilLogic>().startDrilling();
                }
                parent.GetComponent<showingOreScript>().showOre();
            }
        }
    }
    
    
    public void deleteBulding(int xGrid, int zGrid, out int deletedBuildingPrice) {
        GameObject building = topGetBuilging(xGrid, zGrid);
        BuildingScript buildingScript = building.GetComponent<BuildingScript>();
        xGrid = (int)buildingScript.originGrid.x;
        zGrid = (int)buildingScript.originGrid.y;
        deletedBuildingPrice = buildingScript.price;
        Destroy(building);
        for (int x = 0; x < buildingScript.depth; x++) {
            for (int z = 0; z < buildingScript.width; z++) {
                topSetValue(xGrid + x, zGrid + z, null);
            }
        }
    }
    
    // Funkcja do sprawdzenia czy można postawić budynek w danym miejscu
    public bool canPlaceBuildingTop(int xGrid, int yGrid, BuildingTemplate buildingTemplate) {
        for (int x = 0; x < buildingTemplate.depth; x++) {
            for (int y = 0; y < buildingTemplate.width; y++) {
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

    public bool canPlaceBuildingSide(int yGrid, int xGrid, BuildingTemplate buildingTemplate, int side) {
        if (side == 0) {
            if (yGrid >= chunkGeneration.chunkHeight || xGrid >= chunkGeneration.chunkDepth) {
                return false;
            }

            GameObject placeOfPipe = leftGetValue(yGrid, xGrid + (int)this.transform.position.z);
            if (placeOfPipe != null) {
                if (placeOfPipe.GetComponent<idScript>().id == 2 && GameManager.canDrillThroughRocks) {
                    // TODO: kacper dodaj zmienną upgrade za false
                    return true;
                }
                return false;
            }
        }
        else {
            if (yGrid >= chunkGeneration.chunkHeight || xGrid >= chunkGeneration.chunkWidth) {
                return false;
            }

            GameObject placeOfPipe = rightGetValue(yGrid, xGrid + (int)this.transform.position.x);
            if (placeOfPipe != null) {
                if (placeOfPipe.GetComponent<idScript>().id == 2 && GameManager.canDrillThroughRocks) {
                    // TODO: Kacper dodaj zmienną upgrade za false
                    return true;
                }
                return false;
            }
        }

        if (yGrid == leftGrid.GetLength(0)-1) {
            if (side == 0) {
                if (topGetBuilging(0, xGrid-1) != null) {
                    if (topGetBuilging(0, xGrid-1).CompareTag("Drill")) {
                        return true;
                    }
                }
            }
            else {
                if (topGetBuilging(xGrid-1,0) != null) {
                    if (topGetBuilging(xGrid-1, 0).CompareTag("Drill")) {
                        return true;
                    }
                    
                }
            }
            return false;
        } else {
            for (int y = yGrid+1; y < leftGrid.GetLength(0); y++) {
                if (side == 0) {
                    if (leftGetValue(y, xGrid + (int)this.transform.position.z) == null) {
                        return false;
                    }
                    if (leftGetValue(y, xGrid + (int)this.transform.position.z).GetComponent<idScript>().id != 3) {
                        return false;
                    }
                } else {
                    if (rightGetValue(y, xGrid + (int)this.transform.position.x) == null) {
                        return false;
                    }
                    if (rightGetValue(y, xGrid + (int)this.transform.position.x).GetComponent<idScript>().id != 3) {
                        return false;
                    }
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

    public void leftSetValue(int y, int z, GameObject value) {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        z = (int)(z - position.z);
        if (y < 0 || y >= leftGrid.GetLength(0) || z < 0 || z >= leftGrid.GetLength(1)) {
            Debug.LogError("Index out of bounds in leftSetValue: " + y + " " + z);
            return;
        }

        // Debug.Log("leftSetValue: " + y + " " + z + " " + value);
        leftGrid[y, z] = value;
    }

    public void rightSetValue(int y, int x, GameObject value) {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        x = (int)(x - position.x);
        if (y < 0 || y >= rightGrid.GetLength(0) || x < 0 || x >= rightGrid.GetLength(1)) {
            Debug.LogError("Index out of bounds in rightSetValue: " + y + " " + x);
            return;
        }

        // Debug.Log("rightSetValue: " + y + " " + x + " " + value);
        rightGrid[y, x] = value;
    }

    public void topSetValue(int x, int z, GameObject building) {
        if (x < 0 || x >= topGrid.GetLength(0) || z < 0 || z >= topGrid.GetLength(1)) {
            Debug.LogWarning("Index out of bounds in topSetValue: " + x + " " + z);
            return;
        }
        // Debug.Log("topSetValue: " + x + " " + z + " " + building);
        topGrid[x, z] = building;
    }

    public GameObject topGetBuilging(int x, int z) {
        if (x < 0 || x >= topGrid.GetLength(0) || z < 0 || z >= topGrid.GetLength(1)) {
            Debug.LogWarning("Index out of bounds in topGetValue: " + x + " " + z);
            return null;
        }
        return topGrid[x, z];
    }
    public GameObject leftGetValue(int y, int z) {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        z = (int)(z - position.z);
        if (y < 0 || y >= leftGrid.GetLength(0) || z < 0 || z >= leftGrid.GetLength(1)) {
            Debug.LogError("Index out of bounds in leftGetValue: " + y + " " + z);
            return null;
        }

        return leftGrid[y, z];
    }
    public GameObject rightGetValue(int y, int x) {
        Vector3 position = this.transform.position;
        y = (int)(y - position.y);
        x = (int)(x - position.x);
        if (y < 0 || y >= rightGrid.GetLength(0) || x < 0 || x >= rightGrid.GetLength(1)) {
            Debug.LogError("Index out of bounds in rightGetValue: " + y + " " + x);
            return null;
        }

        return rightGrid[y, x];
    }
}