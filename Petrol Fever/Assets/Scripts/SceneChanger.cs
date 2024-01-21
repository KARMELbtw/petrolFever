using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    //zadeklarowanie pozycji i rotacji kamery
    //miasto
    [SerializeField]
    private Vector3 TownCameraPosition = new Vector3(-34f, 36f, -23.5f);
    [SerializeField]
    private Vector3 TownCameraRotation = new Vector3(45f, 226.5f, 0f);
    //chunki
    [SerializeField]
    private Vector3 ChunkCameraPosition = new Vector3(-20f, 38f, -20f);
    [SerializeField]
    private Vector3 ChunkCameraRotation = new Vector3(31.5f, 45f, 0f);
    
    //zadeklarowanie zmiennej do przechowywania informacji o tym czy kamera patrzy na chunki czy na miasto
    bool isLookingAtChunk = false;
    
    void OnMouseDown()
    {
        // Kod do wykonania po kliknięciu myszą na obiekcie
        Debug.Log("Clicked on " + gameObject.name);
        MoveCamera();
    }
    
    void MoveCamera()
    {
        //zamiana kamer
        if (isLookingAtChunk)
        {
            Camera.main.transform.position = TownCameraPosition;
            //nwm czemu ale trzeba dodać ten syf do rotacji bo inaczej ustawia roatcje na 0 360 0
            Camera.main.transform.rotation = Quaternion.Euler(TownCameraRotation);
            Debug.Log("zmieniono widok na miasto");
            isLookingAtChunk = false;
        }
        else
        {
            Camera.main.transform.position = ChunkCameraPosition;
            //tu działa bez tego syfu nwm czemu
            Camera.main.transform.rotation = Quaternion.Euler(ChunkCameraRotation);
            Debug.Log("zmieniono widok na chunki");
            isLookingAtChunk = true;
        }

    }
}