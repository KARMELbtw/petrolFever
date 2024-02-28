using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerOilAi : MonoBehaviour
{
    public int chanceToFindOil = 50;
    
    private int minTimeToFindOil = 10;
    private int maxTimeToFindOil = 20;
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
            Debug.Log(foundOil);
            Debug.Log(timeToFindOil + "czas");
    }

    private void Update() {
        if (foundOil) {
            time += Time.deltaTime;
            var seconds = (int)(time % 60);

            if(seconds != previousSec)
                Debug.Log(seconds);

            previousSec = seconds;

            if(timeToFindOil <= seconds) {
                ChunkGeneration.RevealRandomOilVein(ChunkGeneration.oilVeins);
                Debug.Log("Wykryto ropę");
                foundOil = false;
            }
        }
    }
}
