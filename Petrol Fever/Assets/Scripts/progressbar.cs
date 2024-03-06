using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]


public class progressbar : MonoBehaviour
{
    public int max;
    public int current;
    public Image mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    max = GameManager.AmountOfOilMaxSetGet;
    current = GameManager.AmountOfOilNowSetGet;
        getCurrentFill();
    }
    void getCurrentFill()
    {
        float fillAmount = (float)current /(float)max;
        mask.fillAmount = fillAmount;

    }
}
