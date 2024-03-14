using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{   
    [SerializeField]
    private List<BuildingTemplate> buildings;
    public static int currentBuilding {get; set;} = 666;
    
    [SerializeField]
    private int returnPercentage = 80;

    private bool firstIteration = true;
    private Vector3 mousePos;
    private GameObject preview;
    private RaycastHit shootRay(out bool didHit) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        didHit = true;

        int layerMask = 1 << 3;
        
        // Strał raycastem w miejsce kliknięcia
        // Sprawdzenie czy raycast trafił w coś
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layerMask)) {
            // Debug.Log("Raycast didn't hit anything");
            didHit = false;
            return hitInfo;
        }

        // Sprawdzenie czy raycast trafił w chunk
        if (!hitInfo.transform.gameObject.transform.CompareTag("Chunk")) {
            // Debug.Log("Raycast hit something else: "+ hitInfo.transform.gameObject);
            didHit = false;
            return hitInfo;
        }

        // Pobranie chunka i pozycji w który trafił raycast
        GameObject chunkHit = hitInfo.transform.gameObject;
        Vector3 rayHitPosition = hitInfo.point;
        // Debug.Log("Position of ray hit: " + rayHitPosition);
        // Pobranie pozycji chunka w który trafił raycast
        return hitInfo;
    }

    private bool buildBuilding(BuildingTemplate building) {
        if (GameManager.amountOfMoney < building.price) {
            Debug.Log("Not enough money to build " + building.buildingName);
            return false;
        }

        RaycastHit raycastHit = shootRay(out bool didHit);
        
        if (!didHit) {
            return false;
        }
        GameObject chunkHit = raycastHit.transform.gameObject;
        // Obliczenie pozycji w siatce chunka 
        Vector3 raycastPosition = raycastHit.point;
        Vector3 chunkPosition = chunkHit.transform.position;
        
        int xGrid = (int)Math.Round(raycastPosition.x - chunkPosition.x);
        int yGrid = (int)(raycastPosition.y - chunkPosition.y);
        int zGrid = (int)Math.Round(raycastPosition.z - chunkPosition.z);
        
        Debug.Log("Grid position of ray: x: " + xGrid + " y: " + yGrid + " z: " + zGrid);
        bool isPlaced;
        if (building.buildingName == "Deer") {
            isPlaced = placeDeer(building, chunkHit.GetComponent<GridManager>(), xGrid, yGrid, zGrid);
        } else { 
            isPlaced = placeBuilding(building ,chunkHit.GetComponent<GridManager>(), xGrid, yGrid, zGrid, (raycastPosition.x == 0));
        }

        if (!isPlaced) {
            return false;
        }
        GameManager.amountOfMoney -= building.price;
        return true;
    }
    private void Awake() {
        if (buildings == null) {
            Debug.LogError("Failed to load buildings from Resources");
        }
    }

    private bool placeBuilding(BuildingTemplate building, GridManager chunkgridManager, int xGrid, int yGrid, int zGrid, bool leftSide) {
        if (building.name == "Oil Drill") {
            if (xGrid != 0 && zGrid != 0) {
                Debug.Log("Oil Drill must be placed on edges");
                return false;
            }
        }
        // Sprawdzenie czy budenk jest stawiane na dobrej stronie
        if (yGrid < 25 && building.mustPlaceOnTop) {
            Debug.Log("This Building must be placed on top");
            return false;
        } else if (yGrid >= 25 && !building.mustPlaceOnTop) {
            Debug.Log("This Building must be placed on side ");
            return false;
        }
        
        // Sprawdzenie czy miejsce nie jest zajęte
        if (building.mustPlaceOnTop) {
            if (chunkgridManager.canPlaceBuildingTop(xGrid, zGrid, building) == false) {
                Debug.Log("Building can't be placed on " + chunkgridManager.gameObject.name + " grid: " + xGrid + " " + zGrid);
                return false;
            }
            chunkgridManager.InitializeTopBuilding(new Vector3(xGrid, yGrid, zGrid), building);
        } else {
            if (chunkgridManager.canPlaceBuildingSide(yGrid, (leftSide)?zGrid:xGrid, building, (leftSide)?0:1) == false) {
                Debug.Log("Building can't be placed on " + chunkgridManager.gameObject.name + " grid: " + yGrid + " " + zGrid);
                return false;
            }
            chunkgridManager.InitializeSideBuilding(new Vector3(xGrid, yGrid, zGrid), building);
            
        }

        return true;
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
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    private bool placeDeer(BuildingTemplate deer, GridManager chunkgridManager, int xGrid, int yGrid, int zGrid) {
        // Sprawdzenie czy jeleń jest stawiane na dobrej stronie
        if (yGrid < 25) {
            Debug.Log("This Building must be placed on top");
            return false;
        }
        
        chunkgridManager.InitializeDeer(new Vector3(xGrid, yGrid, zGrid), deer);
        return true;
    }

    GameObject showPreview(BuildingTemplate building) {
        RaycastHit raycastHit = shootRay(out bool didHit);
        if (!didHit) return null;
        // Sprawdzenie czy budenk jest stawiane na dobrej stronie
        
        GameObject chunkHit = raycastHit.transform.gameObject;
        // Obliczenie pozycji w siatce chunka 
        Vector3 raycastPosition = raycastHit.point;
        Vector3 chunkPosition = chunkHit.transform.position;
        
        int xGrid = (int)Math.Round(raycastPosition.x - chunkPosition.x);
        int yGrid = (int)(raycastPosition.y - chunkPosition.y);
        int zGrid = (int)Math.Round(raycastPosition.z - chunkPosition.z);
        
        if (yGrid < 25 && building.mustPlaceOnTop) {
            return null;
        } else if (yGrid >= 25 && !building.mustPlaceOnTop) {
            return null;
        }
        if (building.name == "Oil Drill") {
            if (xGrid != 0 && zGrid != 0) {
                return null;
            }
        }
        GridManager chunkgridManager = chunkHit.GetComponent<GridManager>();
        // Sprawdzenie czy miejsce nie jest zajęte
        if (building.mustPlaceOnTop) {
            if (chunkgridManager.canPlaceBuildingTop(xGrid, zGrid, building) == false) {
                return null;
            }
        } else {
            if (chunkgridManager.canPlaceBuildingSide(yGrid, ((raycastPosition.x == 0))?zGrid:xGrid, building, ((raycastPosition.x == 0))?0:1) == false) {
                return null;
            }
        }
        Vector3 offset = new Vector3(0,0.51f,0);
        if (raycastPosition.x == 0) {
            offset += new Vector3(0,0,0.5f);
        } else {
            offset += new Vector3(0.5f,0,0);
        }
        Vector3 previewPosition = new Vector3(xGrid + chunkPosition.x, yGrid + chunkPosition.y, zGrid + chunkPosition.z) - ((building.buildingName == "Pipe")?offset:Vector3.zero);
        GameObject preview = Instantiate(building.previewPrefab, previewPosition, Quaternion.identity);
        return preview;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentBuilding != 666) {
            
            if (Input.mousePosition != mousePos) {
                if (preview != null) {
                    Destroy(preview);
                }
                preview = showPreview(buildings[currentBuilding]);
            }
            mousePos = Input.mousePosition;
            
            if (Input.GetMouseButtonDown(0)) {
                BuildingTemplate buildingInHand = buildings[currentBuilding];
                bool isBuildingPlaced = buildBuilding(buildingInHand);
                if (isBuildingPlaced && buildings[currentBuilding].buildingName != "Pipe") {
                    if (buildings[currentBuilding].buildingName == "Silos") {
                        TutorialManager.placedFirstSilos = true;
                    }
                    if (preview != null) {
                        Destroy(preview);
                    }
                    currentBuilding = 666;
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
            if (preview != null) {
                Destroy(preview);
            }
        }
    }
}
