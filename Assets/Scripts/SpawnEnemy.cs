using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate<GameObject>(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)]);
        go.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
