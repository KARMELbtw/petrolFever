using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class GameManager : MonoBehaviour
{
    //zadeklarowanie zmiennej globalnej amountOfMoney
    private static int money = 30000;

    private static int oilAmonutNow = 0;
    private static int oilMaxAmountCanHave = 0;
    bool checkOilMaxWithNow()
    {
       return oilAmonutNow < oilMaxAmountCanHave;
    }

    public  static int amountOfMoney {
        get { return money; }
        set {
            if (value > money) {
                Debug.Log("Dodano " + (value - money) + " $");
            }
            else {
                Debug.Log("Odjęto " + (money - value) + " $");
            }

            money = value;
        }
    }
    
    public static int newChunkCost = 1000;
    public static int numberOfLeftChunks = 0;
    public static int numberOfRightChunks = 0;
    public static int numberOfChunks = 0;
    public static bool uiOpened = false;
}
