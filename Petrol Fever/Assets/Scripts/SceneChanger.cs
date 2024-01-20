using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    
    //zadeklarowanie kamery
    public Camera Camera;
    
    //zadeklarowanie pozycji i roatcji kamery
    //miasto
    public Vector3 TownCameraPosition = new Vector3(-34f, 36f, -23.5f);
    public Vector3 TownCameraRotation = new Vector3(45f, 226.5f, 0f);
    //chunki
    public Vector3 ChunkCameraPosition = new Vector3(-20f, 41f, -20f);
    public Vector3 ChunkCameraRotation = new Vector3(31.5f, 45f, 0f);
    
    //zadeklarowanie zmiennych
    bool flag = false;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnMouseDown()
    {
        // Kod do wykonania po kliknięciu myszą na obiekcie
        Debug.Log("Clicked on " + gameObject.name);
        MoveCamera();
    }
    
    void MoveCamera()
    {
        //zamiana kamer
        if (flag)
        {
            Camera.transform.position = TownCameraPosition;
            //nwm czemu ale trzeba dodać ten syf do rotacji bo inaczej ustawia roatcje na 0 360 0
            Camera.transform.rotation = Quaternion.Euler(TownCameraRotation) * Quaternion.Euler(45, 226.5f, 0f);
            Debug.Log("zmieniono widok na miasto");
            flag = false;
        }
        else
        {
            Camera.transform.position = ChunkCameraPosition;
            //tu działa bez tego syfu nwm czemu
            Camera.transform.rotation = Quaternion.Euler(ChunkCameraRotation);
            Debug.Log("zmieniono widok na chunki");
            flag = true;
        }

    }
}