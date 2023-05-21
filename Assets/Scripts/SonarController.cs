using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarController : MonoBehaviour
{
    public GameObject sonarWavePrefab; // Prefab for the sonar wave
    public float sonarCooldown = 2f; // Cooldown duration between sonar uses
    public float sonarRange = 10f; // Range of the sonar

    private bool canUseSonar = true; // Flag to track sonar cooldown
    /*
    private void Update()
    {
        // Check for sonar input
        if (Input.GetKeyDown(KeyCode.Space) && canUseSonar)
        {
            UseSonar();
        }
    }

    private void UseSonar()
    {
        // Instantiate sonar wave at the player's position
        Instantiate(sonarWavePrefab, transform.position, Quaternion.identity);

        // Detect enemies within the sonar range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sonarRange);
        foreach (Collider2D collider in colliders)
        {
            // Check if the detected collider belongs to an enemy
            EnemyBase enemy = collider.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.Reveal();
            }
        }

        // Start the sonar cooldown
        StartCoroutine(SonarCooldown());
    }

    private IEnumerator SonarCooldown()
    {
        canUseSonar = false;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(sonarCooldown);

        canUseSonar = true;
    }
    */
}
