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
        if(sellMultiplier - GameManager.AmountOfOilNowSetGet * 2 >= 2)
            sellMultiplier -= GameManager.AmountOfOilNowSetGet * 2;
        TutorialManager.soldFirstOil = true;
    }

    void Start() {
        oilPriceText = oilPriceTextGameObject.GetComponent<Text>();
        oilPriceText.text = sellMultiplier.ToString();
        StartCoroutine(morepricebetterMorerYASSlayHeil());
    }
    
    IEnumerator morepricebetterMorerYASSlayHeil() {
        while(true) {
            sellMultiplier += 0.1;
            oilPriceText.text = Mathf.Round((float)sellMultiplier).ToString();
            yield return new WaitForSeconds(1);   
        }
    }
}
