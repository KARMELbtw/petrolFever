using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeerPenUI : MonoBehaviour
{
    public Canvas deerUI;
    bool isShown = false;
    
    
    void OnMouseDown()
    {
        showHorsePenUI();
    }
    
    void showHorsePenUI()
    {
        RectTransform deerUIRect = deerUI.GetComponent<RectTransform>();
        deerUIRect.anchoredPosition = new Vector3(0, 0, 0);
        isShown = true;
    }
    
    public void hideHorsePenUI()
    {
        RectTransform deerUIRect = deerUI.GetComponent<RectTransform>();
        deerUIRect.anchoredPosition = new Vector3(0, 600f, 0);
        isShown = false;
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
