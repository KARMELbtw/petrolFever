using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSize : MonoBehaviour
{   
    public Canvas rootCanvas;
    public List<Canvas> childCanvas = new List<Canvas>();

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rootCanvasRect = rootCanvas.GetComponent<RectTransform>();
        RectTransform childCanvasRect;
        foreach (Canvas childCanvas in childCanvas) {
            childCanvasRect = childCanvas.GetComponent<RectTransform>();
            childCanvasRect.sizeDelta = rootCanvasRect.sizeDelta;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
