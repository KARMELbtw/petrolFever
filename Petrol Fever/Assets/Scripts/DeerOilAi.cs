using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerOilAi : MonoBehaviour
{   
    public static int chanceToFindOil = 50;
    public static int minTimeToFindOil = 10;
    public static int maxTimeToFindOil = 20;
    private int timeToFindOil;
    private float time;
    private float previousSec;
    private bool foundOil;
    // Start is called before the first frame update
    void Start()
    {
            foundOil = false;
            if(Random.Range(1, 100) <= chanceToFindOil) {
                foundOil = true;
            }

            timeToFindOil = Random.Range(minTimeToFindOil, maxTimeToFindOil);
    }

    private void Update() {
        time += Time.deltaTime;
        var seconds = (int)(time % 60);
        if(timeToFindOil <= seconds) {
            if (foundOil) {
                ChunkGeneration.RevealRandomOilVein(ChunkGeneration.oilVeins);
                Debug.Log("Wykryto ropę");
                foundOil = false;
            }
            Destroy(gameObject);
        }
    }
}
