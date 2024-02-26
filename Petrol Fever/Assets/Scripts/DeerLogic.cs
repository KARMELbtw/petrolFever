using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerLogic : MonoBehaviour
{   
    private Transform endPoint;
    private float duration = 2f;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        //float t = Mathf.ClampOi(elapsedTime / duration)
    }
}
