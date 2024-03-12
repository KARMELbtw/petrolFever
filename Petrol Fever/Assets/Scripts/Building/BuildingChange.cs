using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingChange : MonoBehaviour
{
    private Button[] buttons;
    public void changeBuilding(int id) {
        Debug.Log("klikło");
        
        switch(id) {
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
    void Start() {
        buttons = new Button[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++) {
            buttons[i].interactable = i != BuildingSystem.currentBuilding;
        }
    }
}
