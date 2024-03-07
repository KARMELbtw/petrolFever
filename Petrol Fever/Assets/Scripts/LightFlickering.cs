using System.Collections;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public Light lightSource; // The light source to flicker
    public float minIntensity = 0.5f; // The minimum intensity of the light
    public float maxIntensity = 2f; // The maximum intensity of the light
    public float flickerSpeed = 0.07f; // The speed of the flicker

    // Start is called before the first frame update
    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }

        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            float intensity = Random.Range(minIntensity, maxIntensity);
            while (Mathf.Abs(lightSource.intensity - intensity) > 0.01)
            {
                lightSource.intensity = Mathf.Lerp(lightSource.intensity, intensity, flickerSpeed);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }
}