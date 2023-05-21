using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float spinSpeed = 100f;

    void Update()
    {
        // Get the current rotation of the object
        Quaternion currentRotation = transform.rotation;

        // Calculate the new rotation based on the spin speed
        Quaternion newRotation = Quaternion.Euler(0f, 0f, spinSpeed * Time.deltaTime) * currentRotation;

        // Apply the new rotation to the object
        transform.rotation = newRotation;
    }
}
