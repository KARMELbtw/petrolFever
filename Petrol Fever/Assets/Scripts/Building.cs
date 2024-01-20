using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building", order = 1)]
public class Building : ScriptableObject {
    public string buildingName;
    public int price;
    public GameObject prefab;
    public int id;
}
