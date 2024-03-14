using System;
using System.Collections.Generic;
using Oil;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    private GridManager gridManager;
    private static readonly System.Random Random = new System.Random();
    [SerializeField] private GameObject oilVeinPrefab;
    [SerializeField] private GameObject oilCubePrefab;
    [SerializeField] private GameObject grassBlockPrefab;
    [SerializeField] private GameObject rockBlockPrefab;
    [SerializeField] private GameObject rockVeinPrefab;
    // [SerializeField] private GameObject magmaBlockPrefab;
    // [SerializeField] private GameObject waterCubePrefab;
    // [SerializeField] private GameObject waterVeinPrefab;
    // [SerializeField] private GameObject magmaVeinPrefab;
    
    public int chunkWidth = 15;
    public int chunkDepth = 15;
    public int chunkHeight = 25;
    public static List<GameObject> oilVeins = new();
    public static List<GameObject> rockVeins = new();

    private void Awake() {
        gridManager = this.GetComponent<GridManager>();
    }
    private bool isOccupied(Vector3 position, int whichWall) {
        if (whichWall == 0) {
            return gridManager.rightGetValue((int)position.y, (int)position.x ) != null;
        }
        else {
            return gridManager.leftGetValue((int)position.y, (int)position.z) != null;
        }
    }
    private void setGridValue(Vector3 position, int whichWall, GameObject value) {
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
        int veinSize = Random.Next(veinMinSize, veinMaxSize);

        GameObject oilVeinParent = Instantiate(oilVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        var position = oilVeinParent.transform.position;
        oilVeinParent.name = "Oil Vein " + position.x + " , " + position.y + " , " + position.z;

        GameObject oilCube = Instantiate(oilCubePrefab, startingPosition, Quaternion.identity, oilVeinParent.transform);
        
        setGridValue(startingPosition, whichWall, oilCube);

        Vector3 currentPosition = startingPosition;
        
        int oilBluckSum = 0;
        for (int i = 0; i < veinSize - 1; i++) {
            
            int whichDirection = Random.Next(1, 3);

            currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);

            // Debug.Log("Oil block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z);
            if(isOccupied(currentPosition, whichWall)){
                Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
                continue;
            }
            oilBluckSum++;
            oilCube = Instantiate(oilCubePrefab, currentPosition, Quaternion.identity, oilVeinParent.transform);
            
            setGridValue(currentPosition, whichWall, oilCube);
        }

        if (whichWall == 0) {
            oilVeinParent.tag = "RightWall";
        } else {
            oilVeinParent.tag = "LeftWall";
        }
        
        oilVeinParent.GetComponent<oilLogic>().numberOfOilBlocks = oilBluckSum + 1;
        
        oilVeins.Add(oilVeinParent);
    }
    // private void GenerateWaterVein(Vector3 startingPosition, int whichWall) {
    //     int veinMaxSize = 15;
    //     int veinMinSize = 9;
    //
    //     int veinMaxWidth = 8;
    //     int veinMaxHeight = 3;
    //
    //     GameObject waterVeinParent =
    //         (GameObject)Instantiate(waterVeinPrefab, startingPosition, Quaternion.identity, this.transform);
    //     waterVeinParent.name =
    //         "Water Vein " + waterVeinParent.transform.position.x + ", " + waterVeinParent.transform.position.y + ", " + waterVeinParent.transform.position.z;
    //     Instantiate(waterCubePrefab, startingPosition, Quaternion.identity, waterVeinParent.transform);
    //
    //     Vector3 currentPosition = startingPosition;
    //
    //     int veinSize = Random.Next(veinMinSize, veinMaxSize);
    //     
    //     for (int i = 0; i < veinSize - 1; i++) {
    //         
    //         int whichDirection = Random.Next(1, 3);
    //
    //         currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);
    //
    //         Debug.Log("Water block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z);
    //         if(isOccupied(currentPosition, whichWall)) {
    //             Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
    //             continue;
    //         }
    //
    //         Instantiate(waterCubePrefab, currentPosition, Quaternion.identity, waterVeinParent.transform);
    //         setGridValue(currentPosition, whichWall, 2);
    //     }
    // }
    private void GenerateRockVein(Vector3 startingPosition, int whichWall) {
        int veinMaxSize = 15;
        int veinMinSize = 9;

        int veinMaxWidth = 8;
        int veinMaxHeight = 3;

        GameObject rockVeinParent =
            (GameObject)Instantiate(rockVeinPrefab, startingPosition, Quaternion.identity, this.transform);
        rockVeinParent.name =
            "Rock Vein " + rockVeinParent.transform.position.x + ", " + rockVeinParent.transform.position.y + ", " + rockVeinParent.transform.position.z;
        GameObject rockCube = Instantiate(rockBlockPrefab, startingPosition, Quaternion.identity, rockVeinParent.transform);
       
        Vector3 currentPosition = startingPosition;

        int veinSize = Random.Next(veinMinSize, veinMaxSize);
        setGridValue(startingPosition, whichWall, rockCube);
        
        for (int i = 0; i < veinSize - 1; i++) {
            
            int whichDirection = Random.Next(1, 3);

            currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);

            // Debug.Log("Rock block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z);
            if(isOccupied(currentPosition, whichWall)) {
                Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
                continue;
            } 
            rockCube = Instantiate(rockBlockPrefab, currentPosition, Quaternion.identity, rockVeinParent.transform);

            setGridValue(currentPosition, whichWall, rockCube);
        }

        if (whichWall == 0) {
            rockVeinParent.tag = "RightWall";
        } else {
            rockVeinParent.tag = "LeftWall";
        }

        rockVeins.Add(rockVeinParent);
    }
    // private void GenerateMagmaVein(Vector3 startingPosition, int whichWall) {
    //     int veinMaxSize = 15;
    //     int veinMinSize = 9;
    //
    //     int veinMaxWidth = 8;
    //     int veinMaxHeight = 3;
    //
    //     GameObject magmaVeinParent =
    //         (GameObject)Instantiate(magmaVeinPrefab, startingPosition, Quaternion.identity, this.transform);
    //     magmaVeinParent.name =
    //         "Magma Vein " + magmaVeinParent.transform.position.x + ", " + magmaVeinParent.transform.position.y + ", " + magmaVeinParent.transform.position.z;
    //     Instantiate(magmaBlockPrefab, startingPosition, Quaternion.identity, magmaVeinParent.transform);
    //     setGridValue(startingPosition, whichWall, 3);
    //     
    //
    //     Vector3 currentPosition = startingPosition;
    //
    //     int veinSize = Random.Next(veinMinSize, veinMaxSize);
    //
    //     for (int i = 0; i < veinSize - 1; i++) {
    //         
    //         int whichDirection = Random.Next(1, 3);
    //
    //         currentPosition = updateCurrentPosition(currentPosition, startingPosition, whichWall, whichDirection, veinMaxHeight, veinMaxWidth);
    //
    //         Debug.Log("Magma block at: " + currentPosition.x + " " + currentPosition.y + " " + currentPosition.z );
    //         if(isOccupied(currentPosition, whichWall)) {
    //             Debug.LogWarning(currentPosition.x + " " + currentPosition.y + " " + currentPosition.z + " is occupied");
    //             continue;
    //         } 
    //         Instantiate(magmaBlockPrefab, currentPosition, Quaternion.identity, magmaVeinParent.transform);
    //
    //         setGridValue(currentPosition, whichWall, 2);
    //         
    //     }
    // }
    public static void RevealRandomOilVein(List<GameObject> oilVeins) {
        int listSize = oilVeins.Count;
        if (listSize <= 0) {
            return;
        }
        GameObject oilVein = oilVeins[Random.Next(0, listSize)];
        // if (oilVein.tag == "RightWall") {
        //     oilVein.transform.position = new Vector3(oilVein.transform.position.x, oilVein.transform.position.y, (float)(oilVein.transform.position.z - 0.5));
        // } else if (oilVein.tag == "LeftWall") {
        //     oilVein.transform.position = new Vector3((float)(oilVein.transform.position.x - 0.5), oilVein.transform.position.y, oilVein.transform.position.z);
        // } else {
        //     oilVein.tag = null;
        // }
        Debug.Log("Wykryta ropa = "+oilVein);
        oilVein.GetComponent<showingOreScript>().showOre();
        oilVeins.Remove(oilVein);
    }

    // Start is called before the first frame update
    void Start() {
        Vector3 startingPosition = this.transform.position;
        Vector3 chunkSizeV3 = new Vector3(chunkDepth, chunkHeight, chunkWidth);

        // GameObject dirtChunk = (GameObject)Instantiate(dirtCubePrefab, startingPosition + chunkSizeV3 / 2 - new Vector3(0,0.002f,0), Quaternion.identity, this.transform);
        // dirtChunk.name = "Dirt of " + this.name;
        // dirtChunk.transform.localScale = chunkSizeV3;
        
        GameObject chunkGrass = new GameObject();
        chunkGrass.name = "chunkGrass";
        chunkGrass.transform.SetParent(this.transform);
        for(int z2 = (int)this.transform.position.z; z2 < chunkDepth + (int)this.transform.position.z; z2++) {
            for(int x2 = (int)this.transform.position.x; x2 < chunkWidth + (int)this.transform.position.x; x2++) {
                Instantiate(grassBlockPrefab, new Vector3(x2 - 0.01f, chunkHeight - 0.5f, z2 - 0.01f), Quaternion.identity, chunkGrass.transform);
            }
        }
        chunkGrass.transform.position = new Vector3(chunkGrass.transform.position.x + 0.5f, 0, chunkGrass.transform.position.z + 0.5f);

        //generowanie wody
        int x, y, z;
        
        // int amountOfWaterVeins = 2;
        //
        // for (int i = 0; i < amountOfWaterVeins; i++) {
        //     int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;
        //
        //     //prawa ściana
        //     if (whichWall == 0) {
        //         x = Random.Next((int)startingPosition.x, chunkWidth - 1 + (int)startingPosition.x);
        //         y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
        //         GenerateWaterVein(new Vector3(
        //                 x,
        //                 y,
        //                 startingPosition.z),
        //             whichWall);
        //     }
        //     //lewa ściana
        //     else {
        //         y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
        //         z = Random.Next((int)startingPosition.z, chunkDepth - 1 + (int)startingPosition.z);
        //         GenerateWaterVein(new Vector3(
        //                 startingPosition.x,
        //                 y,
        //                 z),
        //             whichWall);
        //     }
        // }

        // Generowanie Magmy
        // int amountOfMagmaVeins = 1;
        //
        // for (int i = 0; i < amountOfMagmaVeins; i++) {
        //     int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;
        //
        //     //prawa ściana
        //     if (whichWall == 0) {
        //         x = Random.Next((int)startingPosition.x, chunkWidth - 1 + (int)startingPosition.x);
        //         y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
        //         GenerateMagmaVein(new Vector3(
        //                 x,
        //                 y,
        //                 startingPosition.z),
        //             whichWall);
        //     }
        //     //lewa ściana
        //     else {
        //         y = Random.Next((int)startingPosition.y, chunkHeight - 3 + (int)startingPosition.y);
        //         z = Random.Next((int)startingPosition.z, chunkDepth - 1 + (int)startingPosition.z);
        //         GenerateMagmaVein(new Vector3(
        //                 startingPosition.x,
        //                 y,
        //                 z),
        //             whichWall);
        //     }
        // }

        //generowanie ropy
        int amountOfOilVeins = 10;

        for(int i = 0; i < amountOfOilVeins; i++) {
            int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;

            //prawa ściana
            if (whichWall == 0) {
                x = Random.Next((int)startingPosition.x, (int)(startingPosition.x + chunkWidth));
                y = Random.Next((int)startingPosition.y, (int)(startingPosition.y + chunkHeight - 3));
                GenerateOilVein(new Vector3(x + 0.51f, y + 0.51f, (float)(startingPosition.z + 0.51)), whichWall);
            }
            //lewa ściana
            else {
                z = Random.Next((int)startingPosition.z, (int)(startingPosition.z + chunkDepth));
                y = Random.Next((int)startingPosition.y, (int)(startingPosition.y + chunkHeight - 3));
                GenerateOilVein(new Vector3((float)(startingPosition.x + 0.51), y + 0.51f, z + 0.51f), whichWall);
            }
        }

        //generowanie kamieni
        int amountOfRockVeins = 6;

        for(int i = 0; i < amountOfRockVeins; i++) {
            int whichWall = Random.Next(0, 2) == 0 ? 0 : 1;

            //prawa ściana
            if (whichWall == 0) {
                x = Random.Next((int)startingPosition.x , (int)(startingPosition.x + chunkWidth));
                y = Random.Next((int)startingPosition.y, (int)(startingPosition.y + chunkHeight - 3));
                GenerateRockVein(new Vector3(x + 0.51f, y + 0.51f, (float)(startingPosition.z + 0.51)), whichWall);
            }
            //lewa ściana
            else {
                z = Random.Next((int)startingPosition.z, (int)(startingPosition.z + chunkDepth));
                y = Random.Next((int)startingPosition.y, (int)(startingPosition.y + chunkHeight- 3));
                GenerateRockVein(new Vector3((float)(startingPosition.x + 0.51), y + 0.51f, z + 0.51f), whichWall);
            }
        }

        Debug.Log("Successfully generated chunk at " + this.transform.position);
    }
}