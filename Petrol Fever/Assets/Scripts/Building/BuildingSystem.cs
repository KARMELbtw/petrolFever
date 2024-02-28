using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{   
    [SerializeField]
    private List<BuildingTemplate> buildings;
    public static int currentBuilding {get; private set;} = 666;
    
    [SerializeField]
    private int returnPercentage = 80;
    
    

    private void Awake() {
        // BuildingTemplate[] temp = Resources.LoadAll<BuildingTemplate>("");
        if (buildings == null) {
            Debug.LogError("Failed to load buildings from Resources");
        }
        // buildings = new List<BuildingTemplate>(temp);
    }

    private void BuildBuilding(BuildingTemplate building) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 3;
        
        // Strał raycastem w miejsce kliknięcia
        // Sprawdzenie czy raycast trafił w coś
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layerMask)) {
            Debug.Log("Raycast didn't hit anything");
            return;
        }

        // Sprawdzenie czy raycast trafił w chunk
        if (!hitInfo.transform.gameObject.transform.CompareTag("Chunk")) {
            Debug.Log("Raycast hit something else: "+ hitInfo.transform.gameObject);
            return;
        }

        // Pobranie chunka i pozycji w który trafił raycast
        GameObject chunkHit = hitInfo.transform.gameObject;
        Vector3 rayHitPosition = hitInfo.point;
        Debug.Log("Position of ray hit: " + rayHitPosition);
        // Pobranie pozycji chunka w który trafił raycast
        Vector3 chunkPosition = chunkHit.transform.position;
        // Obliczenie pozycji w siatce chunka 
        int xGrid = (int)Math.Round(rayHitPosition.x - chunkPosition.x);
        int yGrid = (int)(rayHitPosition.y - chunkPosition.y);
        int zGrid = (int)Math.Round(rayHitPosition.z - chunkPosition.z);
        Debug.Log("Grid position of ray: x: " + xGrid + " y: " + yGrid + " z: " + zGrid);
            
        // Pobranie skryptu GridManager z chunka
        GridManager chunkgridManager = chunkHit.GetComponent<GridManager>();
        
        // Sprawdzenie czy budenk jest stawiane na dobrej stronie
        if (yGrid < 25 && building.mustPlaceOnTop) {
            Debug.Log("This Building must be placed on top of " + chunkHit.name + " at " + rayHitPosition);
            return;
        } else if (yGrid >= 25 && !building.mustPlaceOnTop) {
            Debug.Log("This Building must be placed on side of " + chunkHit.name + " at " + rayHitPosition);
            return;
        }
        
        // Sprawdzenie czy miejsce nie jest zajęte
        if (building.mustPlaceOnTop) {
            if (chunkgridManager.canPlaceBuildingTop(xGrid, zGrid, building) == false) {
                Debug.Log("Building can't be placed on " + chunkgridManager.gameObject.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
                return;
            }
            Debug.Log("Building can be placed on " + chunkgridManager.gameObject.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
            chunkgridManager.InitializeTopBuilding(new Vector3(xGrid, yGrid, zGrid), building);
        } else {
            if (chunkgridManager.canPlaceBuildingSide(yGrid, (rayHitPosition.x == 0)?zGrid:xGrid, building, (rayHitPosition.x == 0)?0:1) == false) {
                Debug.Log("Building can't be placed on " + chunkgridManager.gameObject.name + " at " + rayHitPosition + " grid: " + yGrid + " " + zGrid);
                return;
            }
            Debug.Log("Building can be placed on " + chunkgridManager.gameObject.name + " at " + rayHitPosition + " grid: " + yGrid + " " + zGrid);
            chunkgridManager.InitializeSideBuilding(new Vector3(xGrid, yGrid, zGrid), building);
        }
        GameManager.amountOfMoney -= building.price;
    }
    
    private void SellBuilding() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 3;
            
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layerMask)) {
            Debug.Log("Raycast didn't hit anything");
            return;
        }

        // Check if the raycast hit the current GameObject
        if (!hitInfo.transform.gameObject.transform.CompareTag("Chunk")) {
            Debug.Log("Raycast hit something else: "+ hitInfo.transform.gameObject);
            return;
        }

        GameObject chunkHit = hitInfo.transform.gameObject;
        Vector3 rayHitPosition = hitInfo.point;
        Debug.Log("Position of ray hit: " + rayHitPosition);
        Vector3 chunkPosition = chunkHit.transform.position;
        int xGrid = (int)(rayHitPosition.x - chunkPosition.x);
        int yGrid = (int)(rayHitPosition.y - chunkPosition.y);
        int zGrid = (int)(rayHitPosition.z - chunkPosition.z);
        Debug.Log("Grid position of ray: x: " + xGrid + " y: " + yGrid + " z: " + zGrid);
            
        GridManager chunkgridManager = chunkHit.GetComponent<GridManager>();

        if (chunkgridManager.topGetBuilging(xGrid,zGrid) == null) {
            Debug.Log("There is no building to sell on " + chunkgridManager.gameObject.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
            return;
        }
        
        chunkgridManager.deleteBulding(xGrid, zGrid, out int deletedBuildingPrice);
        GameManager.amountOfMoney += deletedBuildingPrice * returnPercentage / 100;
        
    }

    void placeDeer(BuildingTemplate deer) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 3;
        
        // Strał raycastem w miejsce kliknięcia
        // Sprawdzenie czy raycast trafił w coś
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layerMask)) {
            Debug.Log("Raycast didn't hit anything");
            return;
        }

        // Sprawdzenie czy raycast trafił w chunk
        if (!hitInfo.transform.gameObject.transform.CompareTag("Chunk")) {
            Debug.Log("Raycast hit something else: "+ hitInfo.transform.gameObject);
            return;
        }

        // Pobranie chunka i pozycji w który trafił raycast
        GameObject chunkHit = hitInfo.transform.gameObject;
        Vector3 rayHitPosition = hitInfo.point;
        Debug.Log("Position of ray hit: " + rayHitPosition);
        // Pobranie pozycji chunka w który trafił raycast
        Vector3 chunkPosition = chunkHit.transform.position;
        // Obliczenie pozycji w siatce chunka 
        int xGrid = (int)Math.Round(rayHitPosition.x - chunkPosition.x);
        int yGrid = (int)(rayHitPosition.y - chunkPosition.y);
        int zGrid = (int)Math.Round(rayHitPosition.z - chunkPosition.z);
        Debug.Log("Grid position of ray: x: " + xGrid + " y: " + yGrid + " z: " + zGrid);
            
        // Pobranie skryptu GridManager z chunka
        GridManager chunkgridManager = chunkHit.GetComponent<GridManager>();
        
        // Sprawdzenie czy jeleń jest stawiane na dobrej stronie
        if (yGrid < 25) {
            Debug.Log("This Building must be placed on top of " + chunkHit.name + " at " + rayHitPosition);
            return;
        }
        
        chunkgridManager.InitializeDeer(new Vector3(xGrid, yGrid, zGrid), deer);
        GameManager.amountOfMoney -= deer.price;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (currentBuilding != 666) {
                BuildingTemplate buildingInHand = buildings[currentBuilding];
                if (GameManager.amountOfMoney < buildingInHand.price) {
                    Debug.Log("Not enough money to build " + buildingInHand.buildingName);
                    return;
                }

                if (buildingInHand.buildingName == "Deer") { 
                    placeDeer(buildingInHand);  
                } else { 
                    BuildBuilding(buildingInHand);
                }
            }
        }

        
        if (Input.GetKeyDown(KeyCode.R)) {
            SellBuilding();
        } else 
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentBuilding = 0;
        } else 
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            currentBuilding = 1;
        } else 
        if (Input.GetKeyDown(KeyCode.Alpha3) ) {
            currentBuilding = 2;
        } else 
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            currentBuilding = 3;
        } else 
        if (Input.GetKeyDown(KeyCode.F)) {
            currentBuilding = 666;
        }
    }
}
