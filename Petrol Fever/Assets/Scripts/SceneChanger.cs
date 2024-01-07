using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //funkcja zmieniająca scenę na tą zawartą w edytorze jako "sceneName" po kliknięciu guzika
    public void ChangeScene(string sceneName)
    {
        //komunikat o zmianie sceny na sceneName
        Debug.Log("Changing scene to: " + sceneName);
        //zmiana sceny na sceneName
        SceneManager.LoadScene(sceneName);
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