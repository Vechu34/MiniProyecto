using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingEnemy : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;

    public float speed = 5f;
    public float rotateSpeed = 200f;
    public float detectionRadius = 5f;
    public LayerMask playerLayer; // Set this in the Inspector to detect only the player

    private bool isActivated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Check if the player is within the detection radius
        Collider2D detectedPlayer = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (detectedPlayer != null)
        {
            isActivated = true; // Activate the enemy when the player is detected
            target = detectedPlayer.transform;
        }

        // If the enemy is not activated, don't move
        if (!isActivated || target == null) return;

        // Get direction to the player
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        // Calculate angle towards target
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotateSpeed * Time.fixedDeltaTime);

        // Apply rotation and movement
        rb.rotation = angle;
        rb.velocity = transform.up * speed;
    }

    // Debugging: Draw the detection radius in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

