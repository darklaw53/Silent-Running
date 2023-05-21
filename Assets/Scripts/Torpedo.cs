using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public float torpedoSpeed = 2f;  // Speed of the torpedo
    public float torpedoLifetime = 5f;  // Lifetime of the torpedo in seconds
    public float armingDelay = 2f;  // Delay before the torpedo arms itself

    private Rigidbody2D rb;
    private float timer;
    private bool isArmed;

    public GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
        isArmed = false;
    }

    void Update()
    {
        if (!isArmed)
        {
            timer += Time.deltaTime;

            // Check if the arming delay has passed
            if (timer >= armingDelay)
            {
                isArmed = true;
            }
        }
        else
        {
            // Once armed, update the torpedo's behavior as before
            timer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
            rb.velocity = transform.up * torpedoSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isArmed && (collision.CompareTag("Enemie") || collision.CompareTag("Terrain")))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);  // Destroy the torpedo
        }
    }
}
