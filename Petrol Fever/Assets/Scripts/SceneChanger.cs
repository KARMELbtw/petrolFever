using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    //zadeklarowanie kamer
    public Camera ChunksCamera;
    public Camera TownCamera;
    
    //funkcja zmieniająca kamerę na drugą po kliknięciu guzika
    public void ChangeScene()
    {
        Debug.Log("zmieniono scene");
        ChunksCamera.gameObject.SetActive(false);
        TownCamera.gameObject.SetActive(true);
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