using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceControl : MonoBehaviour
{
    public AudioClip[] ambianceTracks;
    AudioSource audioSource;
    public float maxVolume = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume += (Random.Range(0,2) == 0 ? -1f : 1f)*Time.deltaTime*0.02f;
        if (audioSource.volume > maxVolume){
            audioSource.volume = maxVolume;
        }
        if (audioSource.volume < 0.05f){
            if (Random.Range(0f, 1f) < Time.deltaTime){
                audioSource.clip = ambianceTracks[Random.Range(0,ambianceTracks.Length)];
                audioSource.Play();
            }
        }
    }
}
