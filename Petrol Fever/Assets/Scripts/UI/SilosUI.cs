using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilosUI : MonoBehaviour
{
    private GameObject silosUI;
    private RectTransform silosUIRect;
    bool isShown = false;
    
    
    void OnMouseDown()
    {
        showsilosUI();
    }
    
    void showsilosUI()
    {
        if (GameManager.uiOpened == false) {
            silosUIRect.anchoredPosition = new Vector3(0, 0, 0);
            isShown = true;
            GameManager.uiOpened = true;
        }
    }
    
    public void hidesilosUI()
    {
        silosUIRect.anchoredPosition = new Vector3(0, 600f, 0);
        isShown = false;
        GameManager.uiOpened = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        silosUI = GameObject.Find("silosUI");
        silosUIRect = silosUI.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isShown == true) {
            hidesilosUI();
        }
    }
}
