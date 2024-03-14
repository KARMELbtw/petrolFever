using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeerUpgrades : MonoBehaviour
{
    public static int deerCost = 3000;
    public static int speedUpgradeLevel = 1;
    public static int costUpgradeLevel = 1;
    public static int chanceUpgradeLevel = 1;
    private static double costMultiplier = 1.5;
    public static GameObject speedLevel;
    public static GameObject costLevel;
    public static GameObject chancesLevel;

    private static int cost;

    public static void upgradeDeerSpeed() {
        cost = (int)(speedUpgradeLevel * costMultiplier) * 1000;
        if(speedUpgradeLevel < 5 && GameManager.amountOfMoney >= cost) {
            DeerOilAi.minTimeToFindOil -= 1;
            DeerOilAi.maxTimeToFindOil -= 1;
            GameManager.amountOfMoney -= cost;
            speedUpgradeLevel++;
            updateUI();
        }
    }

    public static void upgradeDeerCost() {
        cost = (int)(costUpgradeLevel * costMultiplier) * 1000;
        if(costUpgradeLevel < 5 && GameManager.amountOfMoney >= cost) {
            deerCost -= 500;
            GameManager.amountOfMoney -= cost;
            costUpgradeLevel++;
            updateUI();
        }
    }

    public static void upgradeDeerChance() {
        cost = (int)(speedUpgradeLevel * costMultiplier) * 1250;
        if(chanceUpgradeLevel < 5 && GameManager.amountOfMoney >= cost) {
            DeerOilAi.chanceToFindOil += 10;
            GameManager.amountOfMoney -= cost;
            chanceUpgradeLevel++;
            updateUI();
        }
    }

    public static void updateUI() {
        speedLevel.GetComponent<Text>().text = "Prędkość: " + speedUpgradeLevel + "/5";
        costLevel.GetComponent<Text>().text = "Koszt: " + costUpgradeLevel + "/5";
        chancesLevel.GetComponent<Text>().text = "Szanse na znalezienie ropy: " + chanceUpgradeLevel + "/5";
    }

    void Start()
    {
        speedLevel = GameObject.Find("speedLevel");
        costLevel = GameObject.Find("costLevel");
        chancesLevel = GameObject.Find("chancesLevel");
    }
}
