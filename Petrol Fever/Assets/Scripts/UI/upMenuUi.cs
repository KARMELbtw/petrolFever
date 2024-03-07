using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upMenuUi : MonoBehaviour
{
    //zadeklarowanie amountOfMoneyText w edytorze aby moc potem edytować lement text
    private Text amountOfMoneyDisplay;
    private Text clockDisplay;
    
    // Start is called before the first frame updates
    void Start()
    {
        amountOfMoneyDisplay = GameObject.Find("amountOfMoneyDisplay").GetComponent<Text>();
        clockDisplay = GameObject.Find("Clock").GetComponent<Text>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        //wypisanie zawartośći zmiennej amountOfMoney na ekran do pola amountOfMoneyDisplay typu text
        amountOfMoneyDisplay.text = GameManager.amountOfMoney.ToString("N0");
        
        // Wypisanie czasu
        // Debug.Log("Hours: " + GameManager.currentTime.Hours + " Minutes: " + GameManager.currentTime.Minutes + " Seconds: " + GameManager.currentTime.Seconds);
        clockDisplay.text = GameManager.currentTime.Hours.ToString("D2") + ":" + GameManager.currentTime.Minutes.ToString("D2");
    }
}
