using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ChunkGeneration : MonoBehaviour
{
    public System.Random random = new System.Random();
    public GameObject dirtCubePrefab;
    public GameObject oilCubePrefab;
    public GameObject oilVeinPrefab;
    public Transform startPosition;
    public int chunkWidth = 15;
    public int chunkDepth = 15;
    public int chunkHeight = 25;

    //funkcja generujaca chunki, (numer chunka, przesuniecie chunka o x, przesuniecie chunka o z)
    public void generateChunk(int chunkNumber, int shiftX, int shiftZ)
    {
        Vector3 currentPosition = startPosition.position;
        Vector3 startingPosition = startPosition.position;
        //Kierunek Y
         for (int y = 0; y < chunkHeight; y++)
         {
             // Kierunek Z
             for (int z = 0; z < chunkDepth; z++)
             {
                 // Kierunek X
                 for (int x = 0; x < chunkWidth; x++)
                 {
                     currentPosition = new Vector3(x + shiftX, y, z + shiftZ);
                     Instantiate(dirtCubePrefab, currentPosition, Quaternion.identity, this.transform);
                     Debug.Log("Wygenerowano klocek at: " + currentPosition);
                 }
             }
         }

        int amountOfOilVeins = random.Next(3, 6);
        int whichWall;

        //generowanie ropy
        for (int i = 0; i < amountOfOilVeins; i++)
        {
            if (random.Next(0, 2) == 0)
            {
                whichWall = 0;
            }
            else
            {
                whichWall = 1;
            }

            //prawa ściana
            if (whichWall == 0)
            {
                generateOilVein(new Vector3(
                        random.Next((int)startingPosition.x, chunkWidth - 1),
                        random.Next((int)startingPosition.y, chunkHeight - 3),
                        0),
                    whichWall
                );
                Debug.Log("Prawa ściana");
            }
            //lewa ściana
            else
            {
                generateOilVein(new Vector3(
                        0,
                        random.Next((int)startingPosition.y, chunkHeight - 3),
                        random.Next((int)startingPosition.z, chunkDepth - 1)),
                    whichWall
                );
                Debug.Log("Lewa ściana");
            }
        }
    }

    public void generateOilVein(Vector3 startingPosition, int whichWall)
        {
            int veinMaxSize = 8;
            int veinMinSize = 5;

            int veinMaxWidth = 4;
            int veinMaxHeight = 4;

            int shift = 0;

            GameObject oilVeinParent =
                (GameObject)Instantiate(oilVeinPrefab, startingPosition, Quaternion.identity, this.transform);

            Instantiate(oilCubePrefab, startingPosition, Quaternion.identity, oilVeinParent.transform);
            Vector3 currentPosition = startingPosition;
            for (int i = 0; i < random.Next(veinMinSize, veinMaxSize); i++)
            {
                int whichDirection = random.Next(1, 3);

                //prawa sciana
                if (whichWall == 0)
                {
                    if (whichDirection == 1)
                    {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej szerokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.x + shift < 0 || currentPosition.x + shift > chunkWidth - 1 ||
                               shift == 0 || currentPosition.x + shift > startingPosition.x + veinMaxWidth / 2 ||
                               currentPosition.x + shift < startingPosition.x - veinMaxWidth / 2)
                        {
                            shift = random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w osi x o shift
                        currentPosition = new Vector3(currentPosition.x + shift, currentPosition.y, currentPosition.z);
                    }

                    else if (whichDirection == 2)
                    {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.y + shift < 0 || currentPosition.y + shift > chunkHeight - 3 ||
                               shift == 0 || currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                               currentPosition.y + shift < startingPosition.y - veinMaxHeight / 2)
                        {
                            shift = random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w górę lub dół o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y + shift, currentPosition.z);
                    }
                }

                //lewa sciana
                else
                {
                    if (whichDirection == 1)
                    {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej szerokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.z + shift < 0 || currentPosition.z + shift > chunkDepth - 1 ||
                               shift == 0 || currentPosition.z + shift > startingPosition.z + veinMaxWidth / 2 ||
                               currentPosition.z + shift < startingPosition.z - veinMaxWidth / 2)
                        {
                            shift = random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w osi z o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + shift);
                    }

                    else if (whichDirection == 2)
                    {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.y + shift < 0 || currentPosition.y + shift > chunkHeight - 3 ||
                               shift == 0 || currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                               currentPosition.y + shift < startingPosition.y - veinMaxHeight / 2)
                        {
                            shift = random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w górę lub dół o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y + shift, currentPosition.z);
                    }
                }

                Instantiate(oilCubePrefab, currentPosition, Quaternion.identity, oilVeinParent.transform);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // chunki sie generuja
            generateChunk(1, 0, 0);
            generateChunk(2, 15, 0);
            generateChunk(3, 30, 0);
            generateChunk(4, 15, -15);
            generateChunk(5, 30, -15);
            generateChunk(6, 30, -30);
        }
        
        // // Update is called once per frame
        // void Update()
        // {
        //     
        // }
}
