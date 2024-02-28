using UnityEngine;

public class musicScript : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource audioSource2;
    private int chance;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        audioSource = sources[0];
        audioSource2 = sources[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && !audioSource2.isPlaying)
        {
            chance = Random.Range(0, 100);
            switch (chance) 
            {
                case 21:
                    audioSource.Play();
                    break;
                case 42:
                    audioSource2.Play();
                    break;
                default:
                    break;
            }
        }

    }
}