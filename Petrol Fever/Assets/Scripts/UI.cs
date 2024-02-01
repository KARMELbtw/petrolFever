using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //zadeklarowanie amountOfMoneyText w edytorze aby moc potem edytować lement text
    public Text amountOfMoneyDisplay;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        //wypisanie zawartośći zmiennej amountOfMoney na ekran do pola amountOfMoneyDisplay typu text
        amountOfMoneyDisplay.text = BuildingSystem.amountOfMoney+" $";
    }
}
