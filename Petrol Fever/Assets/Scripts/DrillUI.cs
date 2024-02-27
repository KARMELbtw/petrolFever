using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillUI : MonoBehaviour
{
    public Canvas drillUI;
    bool isShown = false;
    
    
    void OnMouseDown()
    {
        showDrillUI();
    }
    
    void showDrillUI()
    {
        RectTransform drillUIRect = drillUI.GetComponent<RectTransform>();
        drillUIRect.anchoredPosition = new Vector3(0, 0, 0);
        isShown = true;
    }
    
    public void hideDrillUI()
    {
        RectTransform drillUIRect = drillUI.GetComponent<RectTransform>();
        drillUIRect.anchoredPosition = new Vector3(0, 600f, 0);
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
