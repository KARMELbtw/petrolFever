using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using System;

public class EntityPlacing : MonoBehaviour
{
    [SerializeField] private GameObject deer;
    private GameObject spawnedDeer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Ray rayToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayToCursor, out RaycastHit hit) && hit.transform.gameObject.transform.CompareTag("Chunk")) {
                Debug.Log("Raycast hit chunk: " + hit.point);
                Vector3 cursorPosition = hit.point;

                if(cursorPosition != null) {
                    spawnedDeer = Instantiate(deer, cursorPosition, Quaternion.identity, hit.collider.gameObject.transform);
                    spawnedDeer.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    spawnedDeer.AddComponent<DeerLogic>();
                }
            }
        }
    }
}
