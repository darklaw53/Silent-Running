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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth -= (startingHealth/oxygenSeconds)*Time.deltaTime;
        healthText.text = "Oxygen:" + ((int)(currentHealth));
        invulTimer -= Time.deltaTime;
        if (currentHealth <= 0f){
            currentHealth = startingHealth;
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void DealDamage(){
        currentHealth -= damageFromHit;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<EnemyBase>() != null && invulTimer <= 0){
            invulTimer = 0.5f;
            DealDamage();
        }
    }
}
