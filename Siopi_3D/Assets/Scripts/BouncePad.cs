using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                if (playerRb.velocity.y < 0)
                {
                    playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);
                }
            }
        }
    }
}