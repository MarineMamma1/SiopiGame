using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    public float speed = 1.0f;
    private int moveDirection = 1; // 1 for right, -1 for left
    public bool isAlive = true;
    private float hp = 10f;
    public float offset = 0.51f;
    private Vector3 offsetVector;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            // Move the enemy
            rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);

            // Check for obstacles or ledges
            if (CheckForObstaclesAndLedges())
            {
                //Debug.Log("bingo");
                // Change direction
                Flip(); // Flip the enemy sprite to face the new direction
            }
        }
        else
        {
            Destroy(gameObject); // Ensure the correct GameObject is destroyed
        }
    }

    private void Flip()
    {
        // Multiply the x component of localScale by -1 to flip the direction the sprite is facing
        moveDirection *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool CheckForObstaclesAndLedges()
    {
        offsetVector = new Vector3(offset * moveDirection, 0, 0);
        RaycastHit2D hit = Physics2D.Raycast((transform.position + offsetVector), Vector2.right * moveDirection, 1.0f);
        Debug.DrawRay((transform.position + offsetVector), Vector2.right * moveDirection * 1.0f, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                return false;
            }
            else if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }   
        Vector3 ledgeCheckPosition = transform.position + Vector3.right * moveDirection * (offset + 0.5f);
        RaycastHit2D groundCheck = Physics2D.Raycast(ledgeCheckPosition, Vector2.down, 2f);
      
        Debug.DrawRay(ledgeCheckPosition, Vector2.down * 2f, Color.blue);

        if (groundCheck.collider == null || !groundCheck.collider.CompareTag("Ground"))
        {
            return true; // Treat as a ledge
        }

        return false;
    }

    private void TakeDamage()
    {
        hp -= 5;
        if (hp <= 0)
        {
            isAlive = false;
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Flip(); // Flip the enemy sprite to face the new direction


        

        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}
