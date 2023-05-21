using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmarineHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public float startingHealth = 100;
    public float oxygenSeconds = 300;
    public float damageFromHit = 20;
    float currentHealth;
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
    }

    public void DealDamage(){
        currentHealth -= damageFromHit;
    }
}
