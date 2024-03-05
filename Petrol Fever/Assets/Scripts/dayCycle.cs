using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayCycle : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public Color startColor = Color.blue;
    public Color endColor = Color.black;
    public float duration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (Camera.main != null)
        {
            StartCoroutine(ChangeBackgroundColor());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ChangeBackgroundColor()
    {
        float t = 0;
        while (true)
        {
            while (t < duration)
            {
                Camera.main.backgroundColor = Color.Lerp(startColor, endColor, t / duration);
                t += Time.deltaTime;
                yield return null;
            }

            t = 0;

            // Swap the colors for the next cycle
            Color temp = startColor;
            startColor = endColor;
            endColor = temp;
        }
    }
}