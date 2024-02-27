using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //zadeklarowanie zmiennej globalnej amountOfMoney
    private static int money = 300000;

    public static int amountOfMoney {
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
}