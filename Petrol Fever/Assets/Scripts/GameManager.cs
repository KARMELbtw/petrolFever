using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class GameManager : MonoBehaviour
{
    //zadeklarowanie zmiennej globalnej amountOfMoney
    private static int money = 30000;

    private static int oilAmonutNow = 100;
    private static int oilMaxAmountMax = 1110;
    public  static int oilMaxAmountMaxSetGet {
     get { return oilMaxAmountMax; }
        set {
            oilMaxAmountMax = value;
        }
    }
public  static int oilAmountNowSetGet {
     get { return oilAmonutNow; }
        set {
            oilAmonutNow = value;
        }
}
    bool checkOilMaxWithNow()
    {
        return oilAmonutNow < oilMaxAmountCanHave;
       return oilAmonutNow < oilMaxAmountMax;
    }
    public static void addOil(int amount)
    {
        oilAmonutNow += amount;
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
