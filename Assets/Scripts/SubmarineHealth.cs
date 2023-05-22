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
    private float gameOverTimer = 8.7f;

    public OxygenManager oxy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        oxy.currentOxygen = startingHealth;
        oxy.currentOxygen = currentHealth;
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
            gameOverTimer = 8.7f;
        }
        if (gameOver){
            if (gameOverTimer > 0f){
                currentHealth = 0f;
                gameOverTimer -= Time.deltaTime;
            } else {
                gameOver = false;
                currentHealth = startingHealth;
                SceneManager.LoadScene("MainMenu");
                Destroy(this.gameObject);
            }
        }

        oxy.currentOxygen = currentHealth;
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
