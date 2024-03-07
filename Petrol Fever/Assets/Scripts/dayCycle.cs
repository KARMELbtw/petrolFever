using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class dayCycle : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50f;
    [SerializeField]
    private Color startColor = Color.blue;
    [SerializeField]
    private Color endColor = Color.black;
    [SerializeField]
    private int secondOffset = 60;
    [SerializeField]
    private static int dayDuration = 24;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start() {
        if (Camera.main == null) {
            Debug.LogError("Brak głównej kamery w scenie!");
            return;
        }
        mainCamera = Camera.main;
        StartCoroutine(ChangeBackgroundColor());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ChangeBackgroundColor() {
        int i = 0;
        float timeDiff;
        while (i<1000)
        {
            // Rano
            while (GameManager.currentTime.TotalHours >= 4 && GameManager.currentTime.TotalHours <= 7)
            {
                timeDiff = ((float)GameManager.currentTime.TotalHours - 4) / 3;
                Debug.Log("Current Time: " + GameManager.currentTime.TotalHours + " TimeDiff: " + timeDiff + " 3");
                mainCamera.backgroundColor = Color.Lerp(endColor, startColor, timeDiff);
                GameManager.currentTime += TimeSpan.FromSeconds(secondOffset);
                yield return new WaitForSeconds(1);
            }
            
            // Wieczór
            while (GameManager.currentTime.TotalHours >= 18 && GameManager.currentTime.TotalHours <= 21)
            {
                timeDiff = ((float)GameManager.currentTime.TotalHours - 18) / 3;
                Debug.Log("Current Time: " + GameManager.currentTime.TotalHours + " TimeDiff: " + timeDiff + " 3");
                mainCamera.backgroundColor = Color.Lerp(startColor, endColor, timeDiff);
                GameManager.currentTime += TimeSpan.FromSeconds(secondOffset);
                yield return new WaitForSeconds(1);
            }
            
            // Reset the time
            if (GameManager.currentTime.TotalHours >= dayDuration) {
                GameManager.currentTime = TimeSpan.FromHours(0);
            }
            i++;
            GameManager.currentTime += TimeSpan.FromSeconds(secondOffset);
            yield return new WaitForSeconds(1);
        }
    }
}