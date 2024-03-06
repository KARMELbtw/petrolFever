using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximumOil : MonoBehaviour
{
    private static int max;
    // Start is called before the first frame update
    void Start()
    {
    max = GameManager.AmountOfOilMaxSetGet;
    GameManager.AmountOfOilMaxSetGet = max + 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
