using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class newChunkUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;
    // Update is called once per frame
    void Update() {
        textComponent.text = GameManager.newChunkCost.ToString("N0")+"$";
    }
}
