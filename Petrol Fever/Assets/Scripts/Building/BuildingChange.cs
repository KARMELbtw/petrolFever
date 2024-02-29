using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingChange : MonoBehaviour
{
    [SerializeField] private int buidlingId;
    public void changeBuilding() {
        Debug.Log("klikło");
        switch(buidlingId) {
            case 1:
                BuildingSystem.currentBuilding = 0;
                break;
            case 2:
                BuildingSystem.currentBuilding = 1;
                break;
            case 3:
                BuildingSystem.currentBuilding = 2;
                break;
            case 4:
                BuildingSystem.currentBuilding = 3;
                break;
            default:
                Debug.Log("nic");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
