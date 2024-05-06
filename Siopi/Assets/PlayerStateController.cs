using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    private void Start()
    {
        // Access the GameManager instance using the Singleton pattern
        CurrentHealth = MaxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damageAmount = 10;
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        // Use GameManager's singleton instance to call EndGame
        GameManager.Instance.EndGame();
    }
}
