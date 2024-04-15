using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinWeaponScript : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float projectileSpeed = 10f;
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
        if(projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            // Assuming the projectile has a Rigidbody component to apply velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Set the velocity of the projectile
                rb.velocity = transform.forward * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile prefab needs a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Projectile prefab is not assigned.");
        }

    }
    
}
