using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMonsterPass : MonoBehaviour
{
    
    public AudioSource oneShotPlayer;
    public AudioClip monsterPassSfx;
    public float monsterPassSfxVolume = 1f;
    private float delayTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delayTime -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<EnemyBase>() != null && delayTime <= 0f){
            oneShotPlayer.PlayOneShot(monsterPassSfx,monsterPassSfxVolume);
            delayTime = 2f;
        }
    }
}
