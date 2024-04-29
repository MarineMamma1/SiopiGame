using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinWeaponScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireCooldown = 0.5f;
    public float projectileSpeed = 10f;

    private float lastFireTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get input?
    }

    private void FireProjectile()
    {
        // Check if the cooldown has elapsed since the last shot
        if (Time.time > lastFireTime + fireCooldown)
        {
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = transform.right * projectileSpeed; // Use right for 2D space direction
                    lastFireTime = Time.time; // Update the last fire time after a successful shot
                }
                else
                {
                    Debug.LogError("Projectile prefab needs a Rigidbody2D component.");
                }
            }
            else
            {
                Debug.LogError("Projectile prefab is not assigned.");
            }
        }
    }

    public void OnFire()
    {
        FireProjectile();
    }
    
}
