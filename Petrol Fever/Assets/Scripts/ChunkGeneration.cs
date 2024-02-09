using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using System;

public class ChunkGeneration : MonoBehaviour
{
    private GridManager gridManager;
    private readonly System.Random Random = new System.Random();
    [SerializeField] private GameObject dirtCubePrefab;
    [SerializeField] private GameObject oilCubePrefab;
    [SerializeField] private GameObject grassBlockPrefab;
    [SerializeField] private GameObject rockBlockPrefab;
    [SerializeField] private GameObject magmaBlockPrefab;
    [SerializeField] private GameObject waterCubePrefab;
    [SerializeField] private GameObject oilVeinPrefab;
    [SerializeField] private GameObject waterVeinPrefab;
    [SerializeField] private GameObject rockVeinPrefab;
    [SerializeField] private GameObject magmaVeinPrefab;
    
    public int chunkWidth = 15;
    public int chunkDepth = 15;
    public int chunkHeight = 25;
    // public List<string> occupiedPositions = new List<string>();

    private void Awake() {
        gridManager = this.GetComponent<GridManager>();
    }
    // private bool isOccupied(Vector3 currentPosition, Vector3 startingPosition) {
    //     bool isOccupied = false;
    //     foreach(string occupiedPosition in occupiedPositions) {
    //         if(occupiedPosition == currentPosition.x + " " + currentPosition.y + " " + currentPosition.z) {
    //             return true;
    //         }
    //     }
    //     return isOccupied;
    // }
    private bool isOccupied(Vector3 position, int whichWall) {
        if (whichWall == 0) {
            return gridManager.rightGetValue((int)position.y, (int)position.x ) != 0;
        }
        else {
            return gridManager.leftGetValue((int)position.y, (int)position.z) != 0;
        }
    }
    private void setGridValue(Vector3 position, int whichWall, int value) {
        if (whichWall == 0) {
            gridManager.rightSetValue((int)position.y, (int)position.x , value);
        }
        else {
            gridManager.leftSetValue((int)position.y, (int)position.z, value);
        }
    }
    private Vector3 updateCurrentPosition(Vector3 currentPosition, Vector3 startingPosition, int whichWall, int whichDirection, int veinMaxHeight, int veinMaxWidth) {
        //prawa sciana
        int counter = 0;
        int shift = 0;
        if (whichWall == 0) {
            switch (whichDirection) {
                case 1: {
                    //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej szerokości złoża ropy ani nie jest równe zero 
                    while (
                        currentPosition.x + shift < this.transform.position.x ||
                        currentPosition.x + shift > this.transform.position.x + chunkWidth ||
                        currentPosition.x + shift > startingPosition.x + veinMaxWidth / 2 ||
                        currentPosition.x + shift < startingPosition.x - veinMaxWidth / 2 ||
                        shift == 0)
                    {
                        shift = Random.Next(-1, 2);
                        if(counter > 100) {
                            break;
                        }
                        counter++;
                    }

                    //przeunięcie pozycji w osi x o shift
                    currentPosition = new Vector3(currentPosition.x + shift, currentPosition.y, currentPosition.z);
                    break;
                }
                case 2: {
                    //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                    while (
                        currentPosition.y + shift < this.transform.position.y ||
                        currentPosition.y + shift > this.transform.position.y + chunkHeight - 3 ||
                        currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                        currentPosition.y + shift < startingPosition.y - veinMaxHeight / 2 ||
                        shift == 0)
                    {
                        shift = Random.Next(-1, 2);
                        if(counter > 100) {
                            break;
                        }
                        counter++;
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
                    while (
                        currentPosition.z + shift < this.transform.position.z ||
                        currentPosition.z + shift > this.transform.position.z + chunkDepth ||
                        currentPosition.z + shift > startingPosition.z + veinMaxWidth / 2 ||
                        currentPosition.z + shift < startingPosition.z - veinMaxWidth / 2 ||
                        shift == 0)
                    {
                        shift = Random.Next(-1, 2);
                        if(counter > 100) {
                            break;
                        }
                        counter++;
                    }

                    //przeunięcie pozycji w osi z o shift
                    currentPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + shift);
                    break;
                }
                case 2: {
                    //sprawdzanie czy przesunięcie nie wychodzi poza chunka nie przekracz maskymalnej wysokości złoża ropy ani nie jest równe zero 
                    while (
                        currentPosition.y + shift < this.transform.position.y ||
                        currentPosition.y + shift > this.transform.position.y + chunkHeight - 3 ||
                        currentPosition.y + shift > startingPosition.y + veinMaxHeight / 2 ||
                        currentPosition.y + shift < startingPosition.y - veinMaxHeight / 2 ||
                        shift == 0)
                    {
                        shift = Random.Next(-1, 2);
                        if(counter > 100) {
                            break;
                        }
                        counter++;
                    }

                    //przeunięcie pozycji w górę lub dół o shift
                    currentPosition = new Vector3(currentPosition.x, currentPosition.y + shift, currentPosition.z);
                    break;
                }
            }
        }
        return currentPosition;
    }
    private void GenerateOilVein(Vector3 startingPosition, int whichWall) {
        int veinMaxSize = 8;
        int veinMinSize = 5;

        int veinMaxWidth = 4;
        int veinMaxHeight = 2;

        GameObject oilVeinParent = Instantiate(oilVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        var position = oilVeinParent.transform.position;
        oilVeinParent.name = "Oil Vein " + position.x + " , " + position.y + " , " + position.z;

        Instantiate(oilCubePrefab, startingPosition, Quaternion.identity, oilVeinParent.transform);
        
        setGridValue(startingPosition, whichWall, 1);

        Vector3 currentPosition = startingPosition;
        
        int veinSize = Random.Next(veinMinSize, veinMaxSize);

        for (int i = 0; i < veinSize - 1; i++) {
            
            int whichDirection = Random.Next(1, 3);

            currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);

            Debug.Log("Oil block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z);
            if(isOccupied(currentPosition, whichWall)){
                Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
                continue;
            }
            Instantiate(oilCubePrefab, currentPosition, Quaternion.identity, oilVeinParent.transform);
            
            setGridValue(currentPosition, whichWall, 1);
        }
    }
    private void GenerateWaterVein(Vector3 startingPosition, int whichWall) {
        int veinMaxSize = 15;
        int veinMinSize = 9;

        int veinMaxWidth = 8;
        int veinMaxHeight = 3;

        GameObject waterVeinParent =
            (GameObject)Instantiate(waterVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        waterVeinParent.name =
            "Water Vein " + waterVeinParent.transform.position.x + ", " + waterVeinParent.transform.position.y + ", " + waterVeinParent.transform.position.z;
        Instantiate(waterCubePrefab, startingPosition, Quaternion.identity, waterVeinParent.transform);

        Vector3 currentPosition = startingPosition;

        int veinSize = Random.Next(veinMinSize, veinMaxSize);
        
        for (int i = 0; i < veinSize - 1; i++) {
            
            int whichDirection = Random.Next(1, 3);

            currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);

            Debug.Log("Water block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z);
            if(isOccupied(currentPosition, whichWall)) {
                Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
                continue;
            }

            Instantiate(waterCubePrefab, currentPosition, Quaternion.identity, waterVeinParent.transform);
            setGridValue(currentPosition, whichWall, 2);
        }
    }
    private void GenerateRockVein(Vector3 startingPosition, int whichWall) {
        int veinMaxSize = 15;
        int veinMinSize = 9;

        int veinMaxWidth = 8;
        int veinMaxHeight = 3;

        GameObject rockVeinParent =
            (GameObject)Instantiate(rockVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        rockVeinParent.name =
            "Rock Vein " + rockVeinParent.transform.position.x + ", " + rockVeinParent.transform.position.y + ", " + rockVeinParent.transform.position.z;
        Instantiate(rockBlockPrefab, startingPosition, Quaternion.identity, rockVeinParent.transform);
       
        Vector3 currentPosition = startingPosition;

        int veinSize = Random.Next(veinMinSize, veinMaxSize);
        setGridValue(startingPosition, whichWall, 2);
        
        for (int i = 0; i < veinSize - 1; i++) {
            
            int whichDirection = Random.Next(1, 3);

            currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);

            Debug.Log("Rock block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z);
            if(isOccupied(currentPosition, whichWall)) {
                Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
                continue;
            } 
            Instantiate(rockBlockPrefab, currentPosition, Quaternion.identity, rockVeinParent.transform);
            setGridValue(currentPosition, whichWall, 2);
            
        }
    }
    private void GenerateMagmaVein(Vector3 startingPosition, int whichWall) {
        int veinMaxSize = 15;
        int veinMinSize = 9;

        int veinMaxWidth = 8;
        int veinMaxHeight = 3;

        GameObject magmaVeinParent =
            (GameObject)Instantiate(magmaVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        magmaVeinParent.name =
            "Magma Vein " + magmaVeinParent.transform.position.x + ", " + magmaVeinParent.transform.position.y + ", " + magmaVeinParent.transform.position.z;
        Instantiate(magmaBlockPrefab, startingPosition, Quaternion.identity, magmaVeinParent.transform);
        setGridValue(startingPosition, whichWall, 3);
        

        Vector3 currentPosition = startingPosition;

        int veinSize = Random.Next(veinMinSize, veinMaxSize);

        for (int i = 0; i < veinSize - 1; i++) {
            
            int whichDirection = Random.Next(1, 3);

            currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);

            Debug.Log("Magma block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z );
            if(isOccupied(currentPosition, whichWall)) {
                Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
                continue;
            } 
            Instantiate(magmaBlockPrefab, currentPosition, Quaternion.identity, magmaVeinParent.transform);

            setGridValue(currentPosition, whichWall, 2);
            
        }
    }

    // Start is called before the first frame update
    void Start() {
        Vector3 startingPosition = this.transform.position;
        Vector3 chunkSizeV3 = new Vector3(chunkDepth, chunkHeight, chunkWidth);

        GameObject dirtChunk = (GameObject)Instantiate(dirtCubePrefab, startingPosition + chunkSizeV3 / 2 - new Vector3(0, 0.1f), Quaternion.identity, this.transform);
        dirtChunk.name = "Dirt of " + this.name;
        dirtChunk.transform.localScale = chunkSizeV3;
        
        GameObject chunkGrass = new GameObject();
        chunkGrass.name = "chunkGrass";
        chunkGrass.transform.SetParent(this.transform);
        for(int z2 = 0; z2 < chunkDepth; z2++) {
            for(int x2 = 0; x2 < chunkWidth; x2++) {
                Instantiate(grassBlockPrefab, new Vector3(x2, chunkHeight-0.4999f, z2), Quaternion.identity, chunkGrass.transform);
            }
        }
        chunkGrass.transform.position = new Vector3(chunkGrass.transform.position.x + 0.5f, 0.4f, chunkGrass.transform.position.z + 0.5f);

        //generowanie wody
        int x, y, z;
        int amountOfWaterVeins = 2;

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

        // Generowanie Magmy
        int amountOfMagmaVeins = 1;

        for (int i = 0; i < amountOfMagmaVeins; i++) {
            int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;

            //prawa ściana
            if (whichWall == 0) {
                x = Random.Next((int)startingPosition.x, chunkWidth - 1 + (int)startingPosition.x);
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                GenerateMagmaVein(new Vector3(
                        x,
                        y,
                        startingPosition.z),
                    whichWall);
            }
            //lewa ściana
            else {
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                z = Random.Next((int)startingPosition.z, chunkDepth - 1 + (int)startingPosition.z);
                GenerateMagmaVein(new Vector3(
                        startingPosition.x,
                        y,
                        z),
                    whichWall);
            }
        }

        //Generowanie Kamieni
        int amountOfRockVeins = 3;

        for (int i = 0; i < amountOfRockVeins; i++) {
            int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;

            //prawa ściana
            if (whichWall == 0) {
                x = Random.Next((int)startingPosition.x, chunkWidth - 1 + (int)startingPosition.x);
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                GenerateRockVein(new Vector3(
                        x,
                        y,
                        startingPosition.z),
                    whichWall);
            }
            //lewa ściana
            else {
                y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
                z = Random.Next((int)startingPosition.z, chunkDepth - 1 + (int)startingPosition.z);
                GenerateRockVein(new Vector3(
                        startingPosition.x,
                        y,
                        z),
                    whichWall);
            }
        }

        //generowanie ropy
        int amountOfOilVeins = Random.Next(3, 6);

        Debug.Log(this.transform.position);

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
        Debug.Log("Successfully generated chunk at " + this.transform.position);
    }
}