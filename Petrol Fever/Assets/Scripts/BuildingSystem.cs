using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private List<Building> buildings;
    private int currentBuilding = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
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
                Debug.Log("This Building can't be placed on top of " + chunkHit.name + " at " + rayHitPosition);
                return;
            }

            if (chunkgridManager.canPlaceBuilding(xGrid, zGrid, buildings[currentBuilding]) == false) {
                Debug.Log("Building can't be placed on " + this.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
                return;
            }
            
            Debug.Log("Building can be placed on " + this.name + " at " + rayHitPosition + " grid: " + xGrid + " " + zGrid);
            chunkgridManager.BuildBuilding(new Vector3(xGrid, yGrid, zGrid), buildings[currentBuilding]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentBuilding = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            currentBuilding = 1;
        }
    }
}
