using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sell : MonoBehaviour
{
    AudioSource audioSource;
    public double sellMultiplier = 30;
    private int i;
    public void sellOil() {
        if (GameManager.AmountOfOilNowSetGet <= 0) return;
        GameManager.amountOfMoney += GameManager.AmountOfOilNowSetGet * (int)sellMultiplier;
        GameManager.AmountOfOilNowSetGet = 0;
        gameObject.GetComponent<AudioSource>().Play();
        sellMultiplier = 30.0;
    }
    
    void Update()
    {
        if (i >= 10)
        {
            sellMultiplier += 0.1;
            i = 0;
        }
    }
}
