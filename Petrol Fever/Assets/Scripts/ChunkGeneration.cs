using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentPosition = startPosition.position;
        // Kierunek Y
        for (int i = 0; i < 10; i++)
        {
            // Kierunek Z
            for (int j = 0; j < 10; j++)
            {
                // Kierunek X
                for (int k = 0; k < 10; k++)
                {
                    currentPosition = new Vector3(k, i, j);
                    Instantiate(cubePrefab, currentPosition, Quaternion.identity, this.transform);
                    Debug.Log("Wygenerowano klocek at: "+currentPosition);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
