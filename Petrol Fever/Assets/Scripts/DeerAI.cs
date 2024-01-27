using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAI : MonoBehaviour
{
    private float randomX;
    private float randomZ;
    private int i = 1000;
    private int nextMove;
    

    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-54f, -43f);
        randomZ = Random.Range(-51f, -42f);
        nextMove = Random.Range(2500, 5000);
        this.transform.position = new Vector3(randomX,16,randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        if(i >= nextMove){
            randomX = Random.Range(-54f, -45f);
            randomZ = Random.Range(-51f, -42f);
            nextMove = Random.Range(2500, 5000);
            this.transform.position = new Vector3(randomX,16,randomZ);
            i = 0;
        }
        i++;
    }
}