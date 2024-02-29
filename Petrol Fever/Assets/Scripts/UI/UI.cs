using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //zadeklarowanie amountOfMoneyText w edytorze aby moc potem edytować lement text
    private Text amountOfMoneyDisplay;
    private Text amountOfOilDisplay;
    private Text amountOfOilMaxDisplay;
    
    // Start is called before the first frame updates
    void Start()
    {
        amountOfOilDisplay = GameObject.Find("Litry1").GetComponent<Text>();
        amountOfOilMaxDisplay =  GameObject.Find("Litry2").GetComponent<Text>();
        amountOfMoneyDisplay = GameObject.Find("amountOfMoneyDisplay").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {   
        //wypisanie zawartośći zmiennej amountOfMoney na ekran do pola amountOfMoneyDisplay typu text
        amountOfOilDisplay.text = GameManager.oilAmountNowSetGet.ToString("N0");
        amountOfOilMaxDisplay.text = GameManager.oilMaxAmountMaxSetGet.ToString("N0");
        amountOfMoneyDisplay.text = GameManager.amountOfMoney.ToString("N0");
    }
}
