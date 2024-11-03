using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlimeSpawner : MonoBehaviour
{
    public GameObject Slime;
    public float spawnInterval = 5f;

    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

    }

    private void SpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(Slime, transform.position, transform.rotation);
    }


}