using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public float speed = 10f; // Speed at which the torpedo moves forward
    public float activationDelay = 2f; // Time delay before the torpedo becomes active
    public float explosionRadius = 5f; // Radius of the explosion
    public int explosionDamage = 50; // Damage inflicted by the explosion

    private Rigidbody2D torpedoRigidbody;
    private bool isActive = false;

    private void Awake()
    {
        torpedoRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        torpedoRigidbody.velocity = transform.up * speed;
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to a creature or enemy
            /*Creature creature = collider.GetComponent<Creature>();
            if (creature != null)
            {
                creature.TakeDamage(explosionDamage);
            }*/
        }

        // Destroy the torpedo after the explosion
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            Explode();
        }
    }
}
