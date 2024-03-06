using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sell : MonoBehaviour
{
    AudioSource audioSource;
    public void sellOil() {
        if (GameManager.AmountOfOilNowSetGet <= 0) return;
        GameManager.amountOfMoney += GameManager.AmountOfOilNowSetGet * 10;
        GameManager.AmountOfOilNowSetGet = 0;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
