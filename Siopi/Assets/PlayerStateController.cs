using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private GameManager gamemanager;
    public GameObject GameManagerScript;
    public int MaxHealth = 100;
    public int CurrentHealth;

   private void Start()
    {
        gamemanager = GameManagerScript.GetComponent<GameManager>();
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
    gamemanager.EndGame();
    }
}
