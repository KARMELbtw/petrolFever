using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void playgame() {
        SceneManager.LoadScene("Chunks");
    }
    public void quitgame() {
        Application.Quit();
    }
}
