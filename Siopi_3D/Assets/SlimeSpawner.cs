using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlimeSpawner : MonoBehaviour
{
    public GameObject Slime; // Assign your enemy prefab in the Inspector
    public float spawnInterval = 5f; // Time in seconds between spawns

    private float spawnTimer;

    void Update()
    {
        // Increment timer
        spawnTimer += Time.deltaTime;

        // Spawn enemy when timer exceeds the spawn interval
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // Reset the timer
        }

    }

    private void SpawnEnemy()
    {
        // Spawn an enemy at the spawner's position and rotation
        GameObject spawnedEnemy = Instantiate(Slime, transform.position, transform.rotation);
    }


}