using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //zadeklarowanie amountOfMoneyText w edytorze aby moc potem edytować lement text
    private Text amountOfMoneyDisplay;
    private Image currentbuildingImageDisplay;
    [SerializeField]
    private List<Texture2D> buildingImages;
    
    // Start is called before the first frame update
    void Start()
    {
        amountOfMoneyDisplay = GetComponentInChildren<Text>();
        currentbuildingImageDisplay = GetComponentInChildren<Image>();
    }
    // Update is called once per frame
    void Update()
    { 
        //wypisanie zawartośći zmiennej amountOfMoney na ekran do pola amountOfMoneyDisplay typu text
        amountOfMoneyDisplay.text = GameManager.amountOfMoney+" $";
        if (BuildingSystem.currentBuilding != 666) {
            currentbuildingImageDisplay.sprite = Sprite.Create(buildingImages[BuildingSystem.currentBuilding], new Rect(0, 0, buildingImages[BuildingSystem.currentBuilding].width, buildingImages[BuildingSystem.currentBuilding].height), new Vector2(0.5f, 0.5f));
            currentbuildingImageDisplay.color = new Color(1,1,1,1);
        } else {
            currentbuildingImageDisplay.sprite = null;
            currentbuildingImageDisplay.color = new Color(0,0,0,0);
        }
    }
}
