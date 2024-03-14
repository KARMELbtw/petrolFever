using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drillUpgrades : MonoBehaviour
{
    private int drillingSpeedLevel = 1;
    private int cost;
    private GameObject drillingSpeedLevelObject;
    private GameObject canDrillThroughRocksObject;

    public void upgradeDrillingSpeed() {
        cost = drillingSpeedLevel * 1500;
        if(drillingSpeedLevel < 5 && GameManager.amountOfMoney >= cost) {
            GameManager.drillingSpeed -= 0.5f;
            GameManager.amountOfMoney -= cost;
            drillingSpeedLevel++;
            updateUI();
        }
    }

    public void upgradeDrillingThroughStone() {
        cost = 5000;
        if(GameManager.canDrillThroughRocks == false && GameManager.amountOfMoney >= cost) {
            GameManager.amountOfMoney -= cost;
            GameManager.canDrillThroughRocks = true;
            canDrillThroughRocksObject.GetComponent<Text>().text = "Można kopać przez kamień: TAK";
        }
    }

    private void updateUI() {
        drillingSpeedLevelObject.GetComponent<Text>().text = "Prędkość Wydobycia: " + drillingSpeedLevel + "/5";
    }

    // Start is called before the first frame update
    void Start()
    {
        drillingSpeedLevelObject = GameObject.Find("drilingSpeedLevel");
        canDrillThroughRocksObject = GameObject.Find("canGoThroughStoneLevel");
    }
}
