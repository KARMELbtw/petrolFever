using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PlayerLoop;

public class newChunkBuyUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;

    
    // Update is called once per frame
    void Update() {
        textComponent.text = GameManager.newChunkCost.ToString("N0")+"$";
    }
    
    public void deleteSelf() {
        Destroy(this.transform.parent.gameObject);
    }
}
