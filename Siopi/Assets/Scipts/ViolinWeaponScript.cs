using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinWeaponScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireCooldown = 0.5f;
    public float projectileSpeed = 10f;
    public float offset;

    private Vector3 testVector3;
    private Vector3 offSetVector;
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
                offSetVector = new Vector3(offset, 0, 0);
                testVector3 = Vector3.Scale(transform.forward, offSetVector);

                GameObject projectile = Instantiate(projectilePrefab, transform.position + testVector3, transform.rotation);
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
