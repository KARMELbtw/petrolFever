using System;
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
        if (GameManager.amountOfMoney < GameManager.newChunkCost) {
            return;
        }
        Debug.Log("Kupienie chunka lewego");
        updateMoney();
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(0,0,15*(GameManager.numberOfLeftChunks+1)), Quaternion.identity);
        newChunk.name = "Left Chunk " + (GameManager.numberOfLeftChunks+1);
        GameManager.numberOfLeftChunks++;
        GameManager.numberOfChunks++;
    }
    public void buyRightChunk() {
        if (GameManager.amountOfMoney < GameManager.newChunkCost) {
            return;
        }
        Debug.Log("Kupienie chunka prawego");
        updateMoney();
        GameObject newChunk =Instantiate(chunkPrefab, new Vector3(15*(GameManager.numberOfRightChunks+1),0,0), Quaternion.identity);
        newChunk.name = "Right Chunk " + (GameManager.numberOfRightChunks+1);
        GameManager.numberOfRightChunks++;
        GameManager.numberOfChunks++;
    }

    private void Update() {
        if (GameManager.numberOfChunks >= 6) {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("BuyChunkButton");
            foreach (GameObject button in buttons) {
                button.SetActive(false);
            }
        }
    }
}
