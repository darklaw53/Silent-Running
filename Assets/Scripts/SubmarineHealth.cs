using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SubmarineHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public float startingHealth = 100;
    public float oxygenSeconds = 300;
    public float damageFromHit = 20;
    float currentHealth;
    float invulTimer;
    public AudioSource oneShotPlayer;
    public AudioClip gameOverSfx;
    public float gameOverSfxVolume = 1f;
    private bool gameOver = false;
    public AudioClip getHitSfx;
    public float getHitSfxVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Oxygen:" + (int)(Mathf.Round(currentHealth));
        currentHealth -= (startingHealth/oxygenSeconds)*Time.deltaTime;
        invulTimer -= Time.deltaTime;
        if (currentHealth <= 0f && !gameOver){
            gameOver = true;
            oneShotPlayer.clip = gameOverSfx;
            oneShotPlayer.volume = gameOverSfxVolume;
            oneShotPlayer.Play();
        }
        if (gameOver){
            if (oneShotPlayer.isPlaying){
                currentHealth = 0f;
            } else {
                gameOver = false;
                currentHealth = startingHealth;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void DealDamage(){
        currentHealth -= damageFromHit;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<EnemyBase>() != null && invulTimer <= 0){
            invulTimer = 0.5f;
            oneShotPlayer.PlayOneShot(getHitSfx,getHitSfxVolume);
            DealDamage();
        }
    }
}
