using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingChange : MonoBehaviour
{
    [SerializeField] private Image buidling;
    public void changeBuilding() {
        Debug.Log("klikło");
        switch(buidling.name) {
            case "1 Building":
                BuildingSystem.currentBuilding = 0;
                break;
            case "2 Building":
                BuildingSystem.currentBuilding = 1;
                break;
            case "3 Building":
                BuildingSystem.currentBuilding = 2;
                break;
            case "4 Building":
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
