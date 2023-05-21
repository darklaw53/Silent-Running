using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Transform target;
    public float movementSpeed = 2f;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;
    private float nextAttackTime;

    public Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the target is within attack range
        if (target != null && Vector2.Distance(transform.position, target.position) <= (attackRange))
        {
            // Check if the attack cooldown has expired
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else
        {
            // Move towards the target
            if (target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }
        }

        if (target == null){
            //target = SubmarineController.Instance.transform;
        }
    }

    private void Attack()
    {
        // Perform attack logic here
        //Debug.Log("Enemy Base is attacking!");
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy the enemy base
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rb.velocity = -rb.velocity;
    }
}