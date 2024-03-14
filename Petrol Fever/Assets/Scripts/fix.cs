using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fix : MonoBehaviour
{
    // Make the method public so it can be called from the button
    public void ChangeScene()
    {
        SceneManager.LoadScene("Chunks");
    }
}