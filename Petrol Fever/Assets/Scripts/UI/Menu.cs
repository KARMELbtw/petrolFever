using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Image menuBackpanel;
    private bool isShown = false;
    private RectTransform menu;

    public void quitgame() {
        Application.Quit();
    }
    public void showMenu() {
        if (isShown == false) {        
            menu.anchoredPosition = new Vector3(0, 0, 0);
            isShown = true;
        } else {
            menu.anchoredPosition = new Vector3(0, 4000f, 0);
            isShown = false;
        }
    }

    void Start() {
        menu = menuBackpanel.GetComponent<RectTransform>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            showMenu();
        }
    }
}
