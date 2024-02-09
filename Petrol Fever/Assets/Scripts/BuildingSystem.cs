using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common.Connection;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{   
    [SerializeField]
    private List<BuildingTemplate> buildings;
    public static int currentBuilding {get; private set;} = 666;
    
    [SerializeField]
    private int returnPercentage = 80;
    
    //zadeklarowanie zmiennej globalnej amountOfMoney
    private static int money = 300000;
    public static int amountOfMoney { 
        get { return money; }
        private set
        {
            if (value > money) {
                Debug.Log("Dodano " + (value - money) + " $");
            } else {
              Debug.Log("Odjęto " + (money - value) + " $");  
            }
            money = value;
        }
    }

    private void Awake() {
        BuildingTemplate[] temp = Resources.LoadAll<BuildingTemplate>("");
        if (temp.Length == 0) {
            Debug.LogError("Failed to load buildings from Resources");
        }
        buildings = new List<BuildingTemplate>(temp);
    }

    private void BuildBuilding() {
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
            
        if (yGrid < 25 && buildings[currentBuilding].mustPlaceOnTop) {
            Debug.Log("This Building must be placed on top of " + chunkHit.name + " at " + rayHitPosition);
            return;
        }

        if (buildings[currentBuilding].mustPlaceOnTop) {
            if (chunkgridManager.canPlaceBuilding(xGrid, zGrid, buildings[currentBuilding]) == false) {
                Debug.Log("Building can't be placed on " + this.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
                return;
            }
            Debug.Log("Building can be placed on " + this.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
        }
            
        amountOfMoney -= buildings[currentBuilding].price;
        chunkgridManager.InitializeBuilding(new Vector3(xGrid, yGrid, zGrid), buildings[currentBuilding]);
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
            Debug.Log("There is no building to sell on " + this.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
            return;
        }
        
        
        
        chunkgridManager.deleteBulding(xGrid, zGrid, out int deletedBuildingPrice);
        amountOfMoney += deletedBuildingPrice * returnPercentage / 100;

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (currentBuilding != 666) {
                if (amountOfMoney < buildings[currentBuilding].price) {
                    Debug.Log("Not enough money to build " + buildings[currentBuilding].buildingName);
                    return;
                } 
                BuildBuilding();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SellBuilding();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentBuilding = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            currentBuilding = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.F)) {
            currentBuilding = 666;
        }
    }
}
