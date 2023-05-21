using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChunks : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public float offset = 30f;
    public int limit = 2;
    // Start is called before the first frame update
    void Start()
    {
        for (int h = -limit; h <= limit; h++){
            for (int v = -limit; v <= limit; v++){
                if (h != 0 || v != 0){
                    GameObject chunk = Instantiate<GameObject>(chunkPrefabs[Random.Range(0,chunkPrefabs.Length)]);
                    float oddOffset = offset/2f;
                    if (v%2 == 0){
                        oddOffset = 0f;
                    }
                    chunk.transform.position = new Vector3(offset*h+oddOffset, offset*v, 0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
