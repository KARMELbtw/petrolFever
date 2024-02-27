using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PlayerLoop;

public class newChunkBuy : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;

    [SerializeField]
    private GameObject chunkPrefab;
    // Update is called once per frame
    void Update() {
        textComponent.text = GameManager.newChunkCost.ToString("N0")+"$";
    }

    private void updateMoney() {
        GameManager.amountOfMoney -= GameManager.newChunkCost;
        GameManager.newChunkCost += 5000;
    }
    public void buyLeftChunk() {
        updateMoney();
        Instantiate(chunkPrefab, new Vector3(0,0,15*(GameManager.numberOfLeftChunks+1)), Quaternion.identity);
        GameManager.numberOfLeftChunks++;
    }
    public void buyRightChunk() {
        updateMoney();
        Instantiate(chunkPrefab, new Vector3(15*(GameManager.numberOfRightChunks+1),0,0), Quaternion.identity);
        GameManager.numberOfRightChunks++;
    }
}
