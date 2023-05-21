using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float spinSpeed = 100f;
    private AudioSource audioSource;
    private bool canPlayPing = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Get the current rotation of the object
        Quaternion currentRotation = transform.rotation;

        // Calculate the new rotation based on the spin speed
        Quaternion newRotation = Quaternion.Euler(0f, 0f, spinSpeed * Time.deltaTime) * currentRotation;

        // Apply the new rotation to the object
        transform.rotation = newRotation;

        if (transform.rotation.eulerAngles.z > 270f){
            if (canPlayPing){
                canPlayPing = false;
                audioSource.Play();
            }
        } else if (transform.rotation.eulerAngles.z > 90f){
            canPlayPing = true;
        }

    }
}
