using UnityEngine;

public class SubmarineController : Singleton<SubmarineController>
{
    public float throttleSpeed = 5f;       // Speed at which the submarine accelerates/decelerates
    public float rotationSpeed = 100f;     // Speed at which the submarine rotates
    public float strafingSpeed = 3f;       // Speed at which the submarine strafes
    public float driftForce = 1f;          // Force applied to simulate drifting when speed is zero
    public float maxSpeed = 10f;           // Maximum speed (both positive and negative)
    public float haltDeceleration = 10f;   // Speed reduction per second when halting movement

    private Rigidbody2D submarineRigidbody;
    private float currentSpeed = 0f;
    private bool isHalted = false;

    private void Start()
    {
        submarineRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        ApplyDriftForce();
        ClampSpeed();
        HaltMovement();
    }

    private void HandleInput()
    {
        // Throttle Up
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 strafeForce = transform.up * strafingSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);
        }

        // Throttle Down
        if (Input.GetKey(KeyCode.S))
        {
            Vector2 strafeForce = -transform.up * strafingSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);
        }

        // Turn Left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // Turn Right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Strafe Left
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 strafeForce = -transform.right * strafingSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);
        }

        // Strafe Right
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 strafeForce = transform.right * strafingSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);
        }
    }

    private void ApplyDriftForce()
    {
        if (Mathf.Approximately(currentSpeed, 0f))
        {
            // Apply drifting force when speed is zero
            submarineRigidbody.AddForce(-submarineRigidbody.velocity * driftForce, ForceMode2D.Force);
        }
    }

    private void ClampSpeed()
    {
        // Clamp the current speed to the maximum speed (both positive and negative)
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
    }

    private void HaltMovement()
    {
        // Halting movement gradually when the halt key is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isHalted = true;
        }

        if (isHalted)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, haltDeceleration * Time.deltaTime);

            if (Mathf.Approximately(currentSpeed, 0f))
            {
                isHalted = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // Apply forward movement based on currentSpeed
        submarineRigidbody.AddRelativeForce(Vector2.up * currentSpeed, ForceMode2D.Force);
    }
}
