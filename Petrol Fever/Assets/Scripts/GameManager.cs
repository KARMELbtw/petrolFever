using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //zadeklarowanie zmiennej globalnej amountOfMoney
    private static int money = 30000;

    private static int amountOfOilNow = 0;
    private static int amountOfOilMax = 0;

    public static int AmountOfOilMaxSetGet {
        get { return amountOfOilMax; }
        set { amountOfOilMax = value; }
    }

    public static int AmountOfOilNowSetGet {
        get { return amountOfOilNow; }
        set { amountOfOilNow = value; }
    }

    public static bool checkOilMaxWithNow() {
        return amountOfOilNow < amountOfOilMax;
    }

    public static void addOil(int amount) {
        amountOfOilNow += amount;
    }

    public static int amountOfMoney {
        get { return money; }
        set
        {
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
    public static TimeSpan currentTime = TimeSpan.FromHours(12);
}