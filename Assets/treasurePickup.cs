using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class treasurePickup : MonoBehaviour
{
    public string targetTag = "YourTargetTag";
    public TextMeshProUGUI scoreText;
    public AudioSource oneShotPlayer;
    public AudioClip treasureCollectSfx;
    public float treasureSfxVolume = 1f;
    public AudioClip youWinSfx;
    public float youWinSfxVolume = 1f;
    int score = 0;
    float riseTime = 8.2f;
    public GameObject winText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Destroy(collision.gameObject);
            score += 1;
            scoreText.text = "x " + score;
            oneShotPlayer.PlayOneShot(treasureCollectSfx, treasureSfxVolume);
            if (score >= 10){
                winText.SetActive(true);
                oneShotPlayer.PlayOneShot(youWinSfx, youWinSfxVolume);
            }
        }
        if (score >= 10){
            riseTime -= Time.deltaTime;
            SceneManager.LoadScene("MainMenu");
            Destroy(this.gameObject);
        }
    }
}