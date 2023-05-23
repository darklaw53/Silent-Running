using UnityEngine;

public class SubmarineController : Singleton<SubmarineController>
{
    public float throttleSpeed = 5f;       // Speed at which the submarine accelerates/decelerates
    public float rotationSpeed = 100f;     // Speed at which the submarine rotates
    public float strafingSpeed = 3f;       // Speed at which the submarine strafes
    public float driftForce = 1f;          // Force applied to simulate drifting when speed is zero
    public float maxSpeed = 10f;           // Maximum speed (both positive and negative)
    public float haltDeceleration = 10f;   // Speed reduction per second when halting movement
    public Transform compass;

    private Rigidbody2D submarineRigidbody;
    private float currentSpeed = 0f;
    private bool isHalted = false;
    float boostSpeed = 1f;
    float boostMultiplier = 2f;
    float currentVolume = 0f;
    float volumeMult = 2f;
    public GameObject headlights;
    private AudioSource audioSource;
    public AudioSource oneShotPlayer;
    public AudioClip hitWallSfx;
    public float hitWallSfxVolume = 1f;
    private float speedOneFrameAgo;
    private float speedThisFrame;

    private Quaternion initialRotation;

    private void Start()
    {
        submarineRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleInput();
        ApplyDriftForce();
        ClampSpeed();
        HaltMovement();
        RotateCompass();

        currentVolume -= Time.deltaTime;

        if (currentVolume <= 0f){
            currentVolume = 0f;
            headlights.SetActive(false);
        } else {
            headlights.SetActive(true);
        }
    }

    void RotateCompass()
    {
        if (transform.rotation.z == 0)
        {
            var newRot = new Quaternion(0, 0, 180, 0);
            compass.rotation = newRot;
        }
        else
        {
            var newRot = new Quaternion(0, 0, transform.rotation.z * -1, 0);
            compass.rotation = newRot;
        }
    }

    private void HandleInput()
    {
        // Boost
        if (Input.GetKey(KeyCode.LeftShift))
        {
            boostSpeed = boostMultiplier;
        } else {
            boostSpeed = 1f;
        }

        // Throttle Up
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 strafeForce = transform.up * strafingSpeed * boostSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);

            currentVolume += volumeMult * boostSpeed * Time.deltaTime;
        }

        // Throttle Down
        if (Input.GetKey(KeyCode.S))
        {
            Vector2 strafeForce = -transform.up * strafingSpeed * boostSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);

            currentVolume += volumeMult * boostSpeed * Time.deltaTime;
        }

        // Turn Left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * boostSpeed * Time.deltaTime);
            //compass.Rotate(Vector3.forward, -rotationSpeed * boostSpeed * Time.deltaTime);

            currentVolume += volumeMult * boostSpeed * Time.deltaTime;
        }

        // Turn Right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * boostSpeed * Time.deltaTime);
            //compass.Rotate(Vector3.forward, rotationSpeed * boostSpeed * Time.deltaTime);

            currentVolume += volumeMult * boostSpeed * Time.deltaTime;
        }

        // Strafe Left
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 strafeForce = -transform.right * strafingSpeed * boostSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);

            currentVolume += volumeMult * boostSpeed * Time.deltaTime;
        }

        // Strafe Right
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 strafeForce = transform.right * strafingSpeed * boostSpeed;
            submarineRigidbody.AddForce(strafeForce, ForceMode2D.Force);

            currentVolume += volumeMult * boostSpeed * Time.deltaTime;
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
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed * boostSpeed, maxSpeed * boostSpeed);
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

            currentVolume -= volumeMult * boostSpeed * Time.deltaTime;
            if (currentVolume <= 0f){
                currentVolume = 0f;
            }

            if (Mathf.Approximately(currentSpeed, 0f))
            {
                isHalted = false;
            }
        }
    }

    private void FixedUpdate()
    {
        submarineRigidbody.angularVelocity += (submarineRigidbody.angularVelocity > 0 ? -1f : 1f)*Time.deltaTime*3f;
        if (Mathf.Abs(submarineRigidbody.angularVelocity) < Time.deltaTime*4f){
            submarineRigidbody.angularVelocity = 0f;
        }
        audioSource.volume = GetSpeedScalar();
        // Apply forward movement based on currentSpeed
        submarineRigidbody.AddRelativeForce(Vector2.up * currentSpeed, ForceMode2D.Force);
        speedOneFrameAgo = speedThisFrame;
        speedThisFrame = submarineRigidbody.velocity.magnitude;
    }

    public float CurrentVolume(){
        return currentVolume*0.1f;
    }

    public float GetSpeedScalar(){
        return speedOneFrameAgo*0.075f*boostSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<EnemyBase>() == null){
            oneShotPlayer.PlayOneShot(hitWallSfx,hitWallSfxVolume*GetSpeedScalar()*2f);
        }
        submarineRigidbody.velocity = -submarineRigidbody.velocity*0.5f;
        currentVolume += volumeMult*20f;
    }
}
