using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject oil;
    public Transform startPosition;
    public System.Random random = new System.Random();

    public void generateOilVein(Vector3 startingPosition)
    {
        int veinMaxSize = 14;
        int veinMinSize = 5;
        int opposite = 0;
        Instantiate(oil, startingPosition, Quaternion.identity);
        Vector3 currentPosition = startingPosition;
        for(int i = 0; i < random.Next(veinMinSize, veinMaxSize); i++) {
            int whichDirection = random.Next(1,3);
            opposite = random.Next(-1,2);

            while(opposite == 0) {
                opposite = random.Next(-1,2);
            }

            if(whichDirection == 1) {
                currentPosition = new Vector3(currentPosition.x  + random.Next(-1,2), currentPosition.y, currentPosition.z);
            } else if(whichDirection == 2) {
                currentPosition = new Vector3(currentPosition.x, currentPosition.y + random.Next(-1, 2), currentPosition.z);
            }
            
            Instantiate(oil, currentPosition, Quaternion.identity);
        }
    }
    
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
                    Debug.Log("Wygenerowano klocek at: " + currentPosition);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
