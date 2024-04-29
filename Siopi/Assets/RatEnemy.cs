using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    public float speed = 1.0f;
    private int moveDirection = 1;
    public bool isAlive = true;
    private float hp = 10f;
  //  public LayerMask groundLayer; // Layer to detect obstacles and ground

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (isAlive)
        {
            // Move the enemy
            rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);

            // Check for obstacles or ledges
            if (CheckForObstaclesAndLedges())
            {
                moveDirection *= -1; // Change direction
            }
        }
    }

    private bool CheckForObstaclesAndLedges()
    {
        Debug.DrawRay(transform.position, new Vector2(moveDirection, 0) * 1.0f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + 0.5f * moveDirection, transform.position.y), Vector2.down * 1, Color.green);

        // Cast a ray forward to detect obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(moveDirection, 0), 1.0f);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            // If hit something tagged as "Obstacle"
            return true;
        }

        // Cast a ray downwards slightly ahead of the front to detect ledges
        RaycastHit2D groundCheck = Physics2D.Raycast(new Vector2(transform.position.x + 0.51f * moveDirection, transform.position.y), Vector2.down, 1);
        if (groundCheck.collider == null || !groundCheck.collider.CompareTag("Ground"))
        {
            // If there's no collider or the collider is not tagged as "Ground"
            Debug.Log("noCollison");
            return true;
        }

        // Continue moving in the same direction if no obstacles or ledges are detected
        Debug.Log("Collison");
        return false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float yOffSet = 0.5f; // Adjust based on your sprite size and collider setup
            if (collision.contacts[0].point.y > transform.position.y + yOffSet)
            {
                // Player hits the enemy from above
                isAlive = false;
                rb.velocity = Vector2.zero; // Stop moving
                GetComponent<Collider2D>().enabled = false; // Disable the collider
                // Optional: Add animation or effects
            }
            else
            {
                // Player collides with the enemy horizontally
                // Handle player damage here
            }
        }
    }
}
