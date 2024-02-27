using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyChunk : MonoBehaviour
{
    [SerializeField]
    private GameObject chunkPrefab;
    
    private void updateMoney() {
        GameManager.amountOfMoney -= GameManager.newChunkCost;
        GameManager.newChunkCost += 5000;
    }
    public void buyLeftChunk() {
        Debug.Log("Kupienie chunka lewego");
        updateMoney();
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(0,0,15*(GameManager.numberOfLeftChunks+1)), Quaternion.identity);
        newChunk.name = "Left Chunk " + (GameManager.numberOfLeftChunks+1);
        GameManager.numberOfLeftChunks++;
    }
    public void buyRightChunk() {
        Debug.Log("Kupienie chunka prawego");
        updateMoney();
        GameObject newChunk =Instantiate(chunkPrefab, new Vector3(15*(GameManager.numberOfRightChunks+1),0,0), Quaternion.identity);
        newChunk.name = "Right Chunk " + (GameManager.numberOfRightChunks+1);
        GameManager.numberOfRightChunks++;
    }
}
