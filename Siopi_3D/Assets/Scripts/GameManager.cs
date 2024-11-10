using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver, heart0, heart1, heart2, heart3;
    public static GameManager Instance;
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

    // Dictionary allows Unity to keep a number of things in 'memory' and useful for multiple small things like keys, coins, etc. REMEMBER
    private Dictionary<RecordType, bool> collectedRecords = new Dictionary<RecordType, bool>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        collectedRecords[RecordType.RecordA] = false;
        collectedRecords[RecordType.RecordB] = false;
        collectedRecords[RecordType.RecordC] = false;
    }

    void Start()
    {
        health = 4; 
        heart0.gameObject.SetActive(true);
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);

        // Cull distant enemies periodically, remember Invoke. (I won't remember)
        InvokeRepeating(nameof(CullDistantEnemies), 0f, cullCheckInterval);
    }

    void Update()
    {
    
        UpdateHearts();

    
        if (health <= 0)
        {
            health = 0;
            Die(); // The jokes write themselves
        }
    }

    private void GainHealth(int amount)
    {
        health += amount;
        health = Mathf.Min(health, 4);
    }
    void UpdateHearts()
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
            break;
        }
    }

    
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Damage taken");
        health -= 1; 
    }

    public void Die()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0; // Remember timescale
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CullDistantEnemies()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is lost, under the map? Gonna cry?");
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceToPlayer = Vector3.Distance(player.position, enemy.transform.position);

            if (distanceToPlayer > cullDistance)
            {
                Destroy(enemy);
            }
        }
    }

    public void AddRecord(RecordType recordType)
    {
        if (collectedRecords.ContainsKey(recordType) && !collectedRecords[recordType])
        {
            collectedRecords[recordType] = true;
            Debug.Log($"{recordType} has been collected!");
        }
        else
        {
            Debug.Log($"{recordType} has already been collected.");
        }
    }

    public bool HasRecord(RecordType recordType)
    {
        return collectedRecords.ContainsKey(recordType) && collectedRecords[recordType];
    }
}