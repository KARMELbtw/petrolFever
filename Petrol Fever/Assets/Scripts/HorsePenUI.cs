using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorsePenUI : MonoBehaviour
{
    public Button buyDeerButton;
    
    void OnMouseDown()
    {
        // Kod do wykonania po kliknięciu myszą na obiekcie
        Debug.Log("Clicked on " + gameObject.name);
        showHorsePenUI();
    }
    
    void showHorsePenUI()
    {
        RectTransform buyDeerButtonRect = buyDeerButton.GetComponent<RectTransform>();
        buyDeerButtonRect.anchoredPosition = new Vector2(-200, 100);
    }
    
    void hideHorsePenUI()
    {
        RectTransform buyDeerButtonRect = buyDeerButton.GetComponent<RectTransform>();
        buyDeerButtonRect.anchoredPosition = new Vector2(500, 500);
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
