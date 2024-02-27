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
    private float currentHealth;

    void Start()
    {
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
        
        //set initial health
        currentHealth = 100f;

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
                // Set chase speed when player is detected
                navMeshAgent.speed = chaseSpeed;

                // Move towards the player
                navMeshAgent.SetDestination(player.position);

                //trigger walk animation 
                animator.SetBool("isWalking", true);

                if (distanceToPlayer < attackRange && !isAttacking)
                {
                    // Attack the player
                    isAttacking = true;
                    animator.SetTrigger("Attack"); //Called from animation alert in unity 
                    yield return new WaitForSeconds(attackCooldown);
                    isAttacking = false;

                    // Reset speed after attacking
                    navMeshAgent.speed = originalSpeed;
                }
            }
            else 
            {
                animator.SetBool("isWalking", false);
            }

            yield return null;
        }
    }

    // Called by animation event in unity 
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
