using UnityEngine;

public class TorpedoLauncher : MonoBehaviour
{
    public GameObject torpedoPrefab;  // Prefab for the torpedo
    public Transform torpedoSpawnPoint;  // Transform of the torpedo spawn point

    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchTorpedo();
        }
    }

    void LaunchTorpedo()
    {
        // Instantiate a torpedo at the spawn point
        GameObject torpedo = Instantiate(torpedoPrefab, torpedoSpawnPoint.position, torpedoSpawnPoint.rotation);

        // Add force to the torpedo
        Rigidbody2D torpedoRigidbody = torpedo.GetComponent<Rigidbody2D>();
        torpedoRigidbody.AddForce(torpedoSpawnPoint.up * 10f);  // Adjust the force as needed
    }
}