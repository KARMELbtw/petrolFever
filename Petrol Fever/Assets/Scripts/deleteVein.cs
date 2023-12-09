using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteVein : MonoBehaviour
{
    private void DeleteVein()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(this);
    }

    private void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    DeleteVein();
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
    }
    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if(gameObject.tag=="something")
        {
        }
    }
}
