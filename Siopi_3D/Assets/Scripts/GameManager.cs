using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOver, heart0, heart1, heart2, heart3;
    public static int health;
    public Transform player; 
    public float cullDistance = 100f; 
    public float cullCheckInterval = 1f; 

    public enum RecordType
    {
        RecordA,
        RecordB,
        RecordC,
    }

    private Dictionary<RecordType, bool> collectedRecords = new Dictionary<RecordType, bool>();

    void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        // Initialize collected records
        foreach (RecordType recordType in System.Enum.GetValues(typeof(RecordType)))
        {
            collectedRecords[recordType] = false;
        }
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
                Die();
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

    public void PickupRecord(RecordType recordType)
    {
        if (!collectedRecords[recordType])
        {
            collectedRecords[recordType] = true;
            Debug.Log($"Picked up {recordType}");
        }
        else
        {
            Debug.Log($"{recordType} already collected");
        }
    }

    public bool HasCollectedRecord(RecordType recordType)
    {
        return collectedRecords.ContainsKey(recordType) && collectedRecords[recordType];
    }

    private void CullDistantEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(player.position, enemy.transform.position) > cullDistance)
            {
                Destroy(enemy);
            }
        }
    }

    private void Die()
    {
        gameOver.gameObject.SetActive(true);
        Time.timeScale = 0; // Pause the game
        Debug.Log("Player died");
    }

    internal void PickupRecord(Record.RecordType recordType)
    {
        throw new NotImplementedException();
    }
}