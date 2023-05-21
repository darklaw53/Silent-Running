using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandomly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = Vector3.forward*Random.Range(0f,360f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
