using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    private Animator animator;

    public float Health = 100f;
    private ZombieSpawner zombieSpawner;
    void Start()
    {
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
        animator = GetComponent<Animator>();
        Health = 100f;
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(20); // Adjust the score increment as needed
        }

        // Notify the ZombieSpawner that this zombie is defeated
        if (zombieSpawner != null)
        {
            zombieSpawner.ZombieDefeated();
        }

        // Trigger the "Dead" animation if an Animator is attached
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
        // Delay the destruction to allow the animation to play
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        // Wait for the duration of the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the zombie GameObject after the animation
        Destroy(gameObject);
    }
}
