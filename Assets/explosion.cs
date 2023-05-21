using UnityEngine;

public class explosion : MonoBehaviour
{
    public float explosionRadius = 5f;  // Radius of the explosion
    public int explosionDamage = 10;  // Amount of damage to deal

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere representing the explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void Explode()
    {
        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // Iterate through the colliders
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to an enemy object
            if (collider.CompareTag("Enemie"))
            {
                // Apply damage to the enemy
                EnemyBase enemy = collider.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }
        }

        // Iterate through the colliders
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to an enemy object
            if (collider.CompareTag("Revealer"))
            {
                collider.SendMessage("FadeOutSpriteMask", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void End()
    {
        Destroy(gameObject);
    }
}