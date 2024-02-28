using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerUpgrades : MonoBehaviour
{
    public static int deerCost = 3000;

    private static int rangeUpgradeLevel = 1;
    private static int speedUpgradeLevel = 1;
    private static int costUpgradeLevel = 1;
    private static int chanceUpgradeLevel = 1;
    private static double costMultiplier = 1.5;

    public static void upgradeDeerSpeed() {
        DeerOilAi.minTimeToFindOil -= 1;
        DeerOilAi.maxTimeToFindOil -= 1;
        GameManager.amountOfMoney -= (int)(speedUpgradeLevel * costMultiplier) * 1000;
        speedUpgradeLevel++;
    }

    public static void upgradeDeerCost() {
        deerCost -= 500;
        GameManager.amountOfMoney -= (int)(speedUpgradeLevel * costMultiplier) * 1000;
        costUpgradeLevel++;
    }

    public static void upgradeDeerChance() {
        DeerOilAi.chanceToFindOil += 10;
        GameManager.amountOfMoney -= (int)(speedUpgradeLevel * costMultiplier) * 1250;
        chanceUpgradeLevel++;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
