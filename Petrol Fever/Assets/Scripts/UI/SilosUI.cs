using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilosUI : MonoBehaviour
{
    public Canvas silosUI;
    bool isShown = false;
    
    
    void OnMouseDown()
    {
        showsilosUI();
    }
    
    void showsilosUI()
    {
        if (GameManager.uiOpened == false) {
            RectTransform silosUIRect = silosUI.GetComponent<RectTransform>();
            silosUIRect.anchoredPosition = new Vector3(0, 0, 0);
            isShown = true;
            GameManager.uiOpened = true;
        }
    }
    
    public void hidesilosUI()
    {
        RectTransform silosUIRect = silosUI.GetComponent<RectTransform>();
        silosUIRect.anchoredPosition = new Vector3(0, 600f, 0);
        isShown = false;
        GameManager.uiOpened = false;
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
