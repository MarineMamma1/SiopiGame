using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver, heart0, heart1, heart2, heart3;
    public static int health;
    public Transform player; // Assign the player's transform in the Inspector
    public float cullDistance = 100f; // Set the culling distance
    public float cullCheckInterval = 1f; // Interval between cull checks

    void Start()
    {
        health = 4;
        heart0.gameObject.SetActive(true);
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);

        // Start the periodic culling check
        InvokeRepeating(nameof(CullDistantEnemies), 0f, cullCheckInterval);
    }

    void Update()
    {
        switch (health)
        {
            case 4:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 3:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 2:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart0.gameObject.SetActive(false);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            default:
                heart0.gameObject.SetActive(false);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }

    private void CullDistantEnemies()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is missing in GameManager.");
            return;
        }

        // Find all objects tagged "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // Calculate distance between player and enemy
            float distanceToPlayer = Vector3.Distance(player.position, enemy.transform.position);

            // Destroy enemy if it’s farther than the cull distance
            if (distanceToPlayer > cullDistance)
            {
                Destroy(enemy);
            }
        }
    }
}