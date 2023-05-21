using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : EnemyBase
{
    public float thrustForce = 5f;
    public float agroRange = 5f;
    public float range = 10f;
    public float rotationSpeed = 200f;
    public Vector2 targetPosition;

    public void Thrust()
    {
        // Apply a forward force to the squid
        rb.AddForce(transform.up * thrustForce, ForceMode2D.Impulse);
        targetPosition = new Vector2(0, 0); 
    }

    public void RotateTowardsTarget()
    {
        if (Vector2.Distance(SubmarineController.Instance.transform.position, transform.position) < (agroRange*SubmarineController.Instance.CurrentVolume()))
        {
            targetPosition = SubmarineController.Instance.transform.position;
        }
        if (targetPosition == Vector2.zero)
        {
            targetPosition = GetRandomTargetPosition();
        }

        // Calculate the direction to the target
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();

        // Calculate the rotation angle
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate the squid towards the target
        rb.rotation = Mathf.MoveTowardsAngle(rb.rotation, rotationAngle, rotationSpeed * Time.deltaTime);
    }

    public void HaltMovement()
    {
        // Set the squid's velocity and angular velocity to zero
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    Vector2 GetRandomTargetPosition()
    {
        // Generate a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Calculate the random target position within the specified range
        Vector2 randomTargetPosition = (Vector2)transform.position + randomDirection * range;

        return randomTargetPosition;
    }
}