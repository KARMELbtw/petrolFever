using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class musicScript : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource audioSource2;
    private int chance;
    private static bool isMusicPlaying = false;
    private void Awake() {
        if(isMusicPlaying) { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        audioSource = sources[0];
        audioSource2 = sources[1];
        audioSource.Play();
        isMusicPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && !audioSource2.isPlaying)
        {
            chance = Random.Range(0, 10000);
            switch (chance) 
            {
                case 1:
                    audioSource.Play();
                    break;
                case 2:
                    audioSource2.Play();
                    break;
                default:
                    break;
            }
        }

    }
}