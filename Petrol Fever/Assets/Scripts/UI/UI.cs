using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //zadeklarowanie amountOfMoneyText w edytorze aby moc potem edytować lement text
    private Text amountOfMoneyDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        amountOfMoneyDisplay = GameObject.Find("amountOfMoneyDisplay").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    { 
        //wypisanie zawartośći zmiennej amountOfMoney na ekran do pola amountOfMoneyDisplay typu text
        amountOfMoneyDisplay.text = GameManager.amountOfMoney.ToString("N0");
    }
}
