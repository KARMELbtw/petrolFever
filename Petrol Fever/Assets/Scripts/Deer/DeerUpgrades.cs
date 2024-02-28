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

    public static void upgradeDeerSpeed() {
        if(speedUpgradeLevel < 5) {
            DeerOilAi.minTimeToFindOil -= 1;
            DeerOilAi.maxTimeToFindOil -= 1;
            GameManager.amountOfMoney -= (int)(speedUpgradeLevel * costMultiplier) * 1000;
            speedUpgradeLevel++;
            updateUI();
        }
    }

    public static void upgradeDeerCost() {
        if(costUpgradeLevel < 5) {
            deerCost -= 500;
            GameManager.amountOfMoney -= (int)(speedUpgradeLevel * costMultiplier) * 1000;
            costUpgradeLevel++;
            updateUI();
        }
    }

    public static void upgradeDeerChance() {
        if(chanceUpgradeLevel < 5) {
            DeerOilAi.chanceToFindOil += 10;
            GameManager.amountOfMoney -= (int)(speedUpgradeLevel * costMultiplier) * 1250;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
