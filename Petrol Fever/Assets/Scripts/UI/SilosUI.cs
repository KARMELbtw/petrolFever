using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SilosUI : MonoBehaviour
{
    private GameObject silosUI;
    private RectTransform silosUIRect;
    bool isShown = false;
    private Text amountOfOilDisplay;
    private Text amountOfOilMaxDisplay;
    
    
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
        amountOfOilDisplay = GameObject.Find("Litry1").GetComponent<Text>();
        amountOfOilMaxDisplay =  GameObject.Find("Litry2").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        amountOfOilDisplay.text = GameManager.AmountOfOilNowSetGet.ToString("N0");
        amountOfOilMaxDisplay.text = GameManager.AmountOfOilMaxSetGet.ToString("N0");
        if (Input.GetKeyDown(KeyCode.Escape) && isShown == true) {
            hidesilosUI();
        }
    }
}
