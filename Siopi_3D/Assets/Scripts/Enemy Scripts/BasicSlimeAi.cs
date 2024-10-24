
using UnityEngine;
using UnityEngine.AI;

public class BasicSlimeAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public float stompForce = 10f;
    public float stompHeightThreshold = 0.5f;

    private Collider enemyCollider;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    //public float timeBetweenAttacks;
    //bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemyCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

       
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
private void AttackPlayer()
{
    agent.SetDestination(player.position);

    float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    float contactRange = 1.5f;

    if (distanceToPlayer <= contactRange)
    {
        KamikazeAttack();
    }
}

private void KamikazeAttack()
{
    GameManager.health -= 1;
    Destroy(gameObject);    
}

void OnCollisionEnter(Collision collision)
{
    if (collision.collider.CompareTag("Player"))
    {
        // Check if the player is above the enemy (for the stomp)
        if (collision.contacts[0].point.y > transform.position.y + 0.3f)  // Adjust 0.5f as needed based on enemy size
        {
            Debug.Log("Lmao get stomped");

            // Apply an upward force to the player to simulate bounce (optional)
            Rigidbody playerRb = collision.collider.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse); // Adjust force strength as needed
            }

            Destroy(gameObject);
        }
        else
        {
            KamikazeAttack();
        }
    }
}

}
