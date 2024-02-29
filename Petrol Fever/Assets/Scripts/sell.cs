using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sell : MonoBehaviour
{
    AudioSource audioSource;
    public void sellOil() {
        if (GameManager.oilAmonutNow <= 0) return;
        GameManager.amountOfMoney += GameManager.oilAmonutNow * 10;
        GameManager.oilAmonutNow = 0;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
