using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    public float stompForce = 10f;
    public float stompHeightThreshold = 0.5f;

    private Collider enemyCollider;

    private void Start() 
    {
        enemyCollider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, stompForce, playerRb.velocity.z);
            }

            Destroy(gameObject);
        }
    }
}
