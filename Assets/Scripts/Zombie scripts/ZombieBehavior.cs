using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehavior : MonoBehaviour
{
    public float detectionRadius = 100f;
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 2f;
    public float chaseSpeed = 3.5f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private bool isAttacking;
    private Animator animator;
    private float originalSpeed; // Store the original speed
    private ZombieHealth zombieHealth;
    private ZombieSpawner zombieSpawner;

    void Start()
    {
        zombieSpawner = FindObjectOfType<ZombieSpawner>();

        // Add a NavMeshAgent if not present
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        // Store the original speed
        originalSpeed = navMeshAgent.speed;

        zombieHealth = GetComponent<ZombieHealth>();
        // Start the zombie's behavior
        StartCoroutine(StartZombieBehavior());
    }

    IEnumerator StartZombieBehavior()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRadius)
            {
                // Set chase speed when the player is detected
                navMeshAgent.speed = chaseSpeed;

                // Move towards the player
                navMeshAgent.SetDestination(player.position);

                // Trigger walk animation 
                animator.SetBool("Walk", true);

                if (distanceToPlayer < attackRange && !isAttacking)
                {
                    // Attack the player
                    isAttacking = true;
                    animator.SetTrigger("Attack"); // Called from animation alert in Unity 
                    yield return new WaitForSeconds(attackCooldown);
                    isAttacking = false;

                    // Reset speed after attacking
                    navMeshAgent.speed = originalSpeed;

                    // Notify ZombieHealth script when the zombie takes damage
                    zombieHealth.TakeDamage(attackDamage);
                }
            }
            else
            {
                animator.SetBool("Walk", false);
            }

            yield return null;
        }
    }

    // Called by animation event in Unity 
    void DealDamage()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
        {
            // Deal damage to the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Dealt damage: " + attackDamage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        DealDamage();
    }
}
