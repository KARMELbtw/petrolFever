using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ChunkGeneration : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private readonly System.Random Random = new System.Random();
    [SerializeField] private GameObject dirtCubePrefab;
    [SerializeField] private GameObject oilCubePrefab;
    [SerializeField] private GameObject oilVeinPrefab;
    [SerializeField] private GameObject waterCubePrefab;
    [SerializeField] private GameObject waterVeinPrefab;
    public int chunkWidth = 15;
    public int chunkDepth = 15;
    public int chunkHeight = 25;

    private void GenerateOilVein(Vector3 startingPosition, int whichWall) {
        int veinMaxSize = 8;
        int veinMinSize = 5;

        int veinMaxWidth = 4;
        int veinMaxHeight = 4;

        int shift = 0;

        GameObject oilVeinParent = Instantiate(oilVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        var position = oilVeinParent.transform.position;
        oilVeinParent.name = "Oil Vein " + position.x + " , " + position.y + " , " + position.z;

        Instantiate(oilCubePrefab, startingPosition, Quaternion.identity, oilVeinParent.transform);
        if (whichWall == 0) {
            gridManager.rightSetValue((int)startingPosition.y, (int)startingPosition.x, 1);
        }
        else {
            gridManager.leftSetValue((int)startingPosition.y, (int)startingPosition.z, 1);
        }

        Vector3 currentPosition = startingPosition;

        for (int i = 0; i < Random.Next(veinMinSize, veinMaxSize); i++) {
            int whichDirection = Random.Next(1, 3);

            //prawa sciana
            if (whichWall == 0) {
                switch (whichDirection) {
                    case 1: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej szerokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.x + shift < 0 ||
                               currentPosition.x + shift > chunkWidth - 1 + currentPosition.x || shift == 0 ||
                               currentPosition.x + shift > startingPosition.x + veinMaxWidth / 2 ||
                               currentPosition.x + shift < startingPosition.x - veinMaxWidth / 2) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w osi x o shift
                        currentPosition = new Vector3(currentPosition.x + shift, currentPosition.y, currentPosition.z);
                        break;
                    }
                    case 2: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.y + shift < 0 ||
                               currentPosition.y + shift > chunkHeight - 3 + currentPosition.y || shift == 0 ||
                               currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                               currentPosition.y + shift < startingPosition.y - veinMaxHeight / 2) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w górę lub dół o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y + shift, currentPosition.z);
                        break;
                    }
                }
            }
            //lewa sciana
            else {
                switch (whichDirection) {
                    case 1: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej szerokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.z + shift < 0 ||
                               currentPosition.z + shift > chunkDepth - 1 + currentPosition.z || shift == 0 ||
                               currentPosition.z + shift > startingPosition.z + veinMaxWidth / 2 ||
                               currentPosition.z + shift < startingPosition.z - veinMaxWidth / 2) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w osi z o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + shift);
                        break;
                    }
                    case 2: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.y + shift < 0 ||
                               currentPosition.y + shift > chunkHeight - 3 + currentPosition.y || shift == 0 ||
                               currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                               currentPosition.y + shift < startingPosition.y - veinMaxHeight / 2) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w górę lub dół o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y + shift, currentPosition.z);
                        break;
                    }
                }
            }

            Instantiate(oilCubePrefab, currentPosition, Quaternion.identity, oilVeinParent.transform);
            if (whichWall == 0) {
                gridManager.rightSetValue((int)currentPosition.y, (int)currentPosition.x, 1);
            }
            else {
                gridManager.leftSetValue((int)currentPosition.y, (int)currentPosition.z, 1);
            }
        }
    }

    private void GenerateWaterVein(Vector3 startingPosition, int whichWall) {
        int veinMaxSize = 17;
        int veinMinSize = 11;

        int veinMaxWidth = 8;
        int veinMaxHeight = 2;

        int shift = 0;

        GameObject waterVeinParent =
            (GameObject)Instantiate(waterVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        waterVeinParent.name =
            "Water Vein " + waterVeinParent.transform.position.x + ", " + waterVeinParent.transform.position.z;
        Instantiate(waterCubePrefab, startingPosition, Quaternion.identity, waterVeinParent.transform);
        if (whichWall == 0) {
            gridManager.rightSetValue((int)startingPosition.y, (int)startingPosition.x, 2);
        }
        else {
            gridManager.leftSetValue((int)startingPosition.y, (int)startingPosition.z, 2);
        }

        Vector3 currentPosition = startingPosition;

        for (int i = 0; i < Random.Next(veinMinSize, veinMaxSize); i++) {
            int whichDirection = Random.Next(1, 3);

            //prawa sciana
            if (whichWall == 0) {
                switch (whichDirection) {
                    case 1: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej szerokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.x + shift < 0 ||
                               currentPosition.x + shift > chunkWidth - 1 + currentPosition.x || shift == 0 ||
                               currentPosition.x + shift > startingPosition.x + veinMaxWidth / 2 ||
                               currentPosition.x + shift < startingPosition.x - veinMaxWidth / 2) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w osi x o shift
                        currentPosition = new Vector3(currentPosition.x + shift, currentPosition.y, currentPosition.z);
                        break;
                    }
                    case 2: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.y + shift < 0 ||
                               currentPosition.y + shift > chunkHeight - 3 + currentPosition.y || shift == 0 ||
                               currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                               currentPosition.y + shift < startingPosition.y) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w górę lub dół o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y + shift, currentPosition.z);
                        break;
                    }
                }
            }
            //lewa sciana
            else {
                switch (whichDirection) {
                    case 1: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej szerokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.z + shift < 0 ||
                               currentPosition.z + shift > chunkDepth - 1 + currentPosition.z || shift == 0 ||
                               currentPosition.z + shift > startingPosition.z + veinMaxWidth / 2 ||
                               currentPosition.z + shift < startingPosition.z - veinMaxWidth / 2) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w osi z o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + shift);
                        break;
                    }
                    case 2: {
                        //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                        while (currentPosition.y + shift < 0 ||
                               currentPosition.y + shift > chunkHeight - 3 + currentPosition.y || shift == 0 ||
                               currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                               currentPosition.y + shift < startingPosition.y) {
                            shift = Random.Next(-1, 2);
                        }

                        //przeunięcie pozycji w górę lub dół o shift
                        currentPosition = new Vector3(currentPosition.x, currentPosition.y + shift, currentPosition.z);
                        break;
                    }
                }
            }

            Instantiate(waterCubePrefab, currentPosition, Quaternion.identity, waterVeinParent.transform);
            if (whichWall == 0) {
                gridManager.rightSetValue((int)currentPosition.y, (int)currentPosition.x, 2);
            }
            else {
                gridManager.leftSetValue((int)currentPosition.y, (int)currentPosition.z, 2);
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        Vector3 startingPosition = this.transform.position;
        Vector3 chunkSizeV3 = new Vector3(chunkDepth, chunkHeight, chunkWidth);

        GameObject dirtChunk = (GameObject)Instantiate(dirtCubePrefab,
            startingPosition + chunkSizeV3 / 2 - new Vector3(0, 0.1f), Quaternion.identity, this.transform);
        dirtChunk.name = "Dirt of " + this.name;
        dirtChunk.transform.localScale = chunkSizeV3;

        //generowanie ropy
        int amountOfOilVeins = Random.Next(3, 6);

        int x, y, z;
        for (int i = 0; i < amountOfOilVeins; i++) {
            int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;
            //prawa ściana
            if (whichWall == 0) {
                x = Random.Next((int)startingPosition.x, chunkWidth - 1 + (int)startingPosition.x);
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                GenerateOilVein(new Vector3(
                        x,
                        y,
                        startingPosition.z),
                    whichWall);
            }
            //lewa ściana
            else {
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                z = Random.Next((int)startingPosition.z, chunkDepth - 1 + (int)startingPosition.z);
                GenerateOilVein(new Vector3(
                        startingPosition.x,
                        y,
                        z),
                    whichWall);
            }
        }

        int amountOfWaterVeins = 1;

        for (int i = 0; i < amountOfWaterVeins; i++) {
            int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;

            //prawa ściana
            if (whichWall == 0) {
                x = Random.Next((int)startingPosition.x, chunkWidth - 1 + (int)startingPosition.x);
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                GenerateWaterVein(new Vector3(
                        x,
                        y,
                        startingPosition.z),
                    whichWall);
            }
            //lewa ściana
            else {
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                z = Random.Next((int)startingPosition.z, chunkDepth - 1 + (int)startingPosition.z);
                GenerateWaterVein(new Vector3(
                        startingPosition.x,
                        y,
                        z),
                    whichWall);
            }
        }
    }
}