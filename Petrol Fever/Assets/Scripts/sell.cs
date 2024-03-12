using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sell : MonoBehaviour
{
    AudioSource audioSource;
    public double sellMultiplier = 30;
    [SerializeField] private GameObject oilPriceTextGameObject;
    private Text oilPriceText;
    private int i = 0;
    public void sellOil() {
        if (GameManager.AmountOfOilNowSetGet <= 0) return;
        GameManager.amountOfMoney += GameManager.AmountOfOilNowSetGet * (int)sellMultiplier;
        GameManager.AmountOfOilNowSetGet = 0;
        gameObject.GetComponent<AudioSource>().Play();
        sellMultiplier = 30.0;
    }

    void Start() {
        Text oilPriceText = oilPriceTextGameObject.GetComponent<Text>();
        oilPriceText.text = sellMultiplier.ToString();
    }
    
    void Update()
    {
        if (i >= 10)
        {
            sellMultiplier += 0.1;
            oilPriceText.text = sellMultiplier.ToString();
            i = 0;
        }
        i++;
    }
}
