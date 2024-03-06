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
            // Pierwsza połowa dnia
            while (GameManager.currentTime.Seconds < GameManager.dayDuration / 2)
            {
                timeDiff = (float)GameManager.currentTime.Seconds / GameManager.dayDuration;
                // Debug.Log("Current Time: " + GameManager.currentTime.Seconds + " TimeDiff: " + timeDiff + " GameManager.dayDuration: " + GameManager.dayDuration);
                mainCamera.backgroundColor = Color.Lerp(startColor, endColor, timeDiff);
                GameManager.currentTime += TimeSpan.FromSeconds(secondOffset);
                // Debug.Log("Pierwsza połowa dnia");
                yield return new WaitForSeconds(1);
            }
            
            // Druga połowa dnia
            while (GameManager.currentTime.Seconds < GameManager.dayDuration)
            {
                timeDiff = (float)GameManager.currentTime.Seconds / GameManager.dayDuration;
                // Debug.Log("Current Time: " + GameManager.currentTime.Seconds + " TimeDiff: " + timeDiff + " GameManager.dayDuration: " + GameManager.dayDuration);
                mainCamera.backgroundColor = Color.Lerp(endColor, startColor, timeDiff);
                GameManager.currentTime += TimeSpan.FromSeconds(secondOffset);
                // Debug.Log("Druga połowa dnia");
                yield return new WaitForSeconds(1);
            }
            
            // Reset the time
            if (GameManager.currentTime.Seconds >= GameManager.dayDuration) {
                GameManager.currentTime = TimeSpan.FromSeconds(0);
            }
            i++;
        }
    }
}