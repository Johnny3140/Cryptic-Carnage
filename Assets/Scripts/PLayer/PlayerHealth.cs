using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    private float percentComplete; 
    private Coroutine healthRegenCoroutine;
    private bool recentlyDamaged = false;

    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public float healthRegenRate = 5f;
    public float healthRegenAmount = 1f;
    public Image frontHealthBar;
    public Image backHealthBar;

    private void Start()
    {
        health = maxHealth;
        StartCoroutineIfNotRunning(HealthRegeneration());
                Debug.Log("Player health initialized to: " + health);

    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        recentlyDamaged = false; // Reset recentlyDamaged flag each frame
    }

    private void UpdateHealthUI()
    {
        float hFraction = health / maxHealth;
        percentComplete = 0f; 

        if (frontHealthBar.fillAmount < hFraction)
        {
            lerpTimer += Time.deltaTime;
            percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(frontHealthBar.fillAmount, hFraction, percentComplete);
        }
        else
        {
            lerpTimer = 0f;
        }

        if (percentComplete >= 1.0f)
        {
            backHealthBar.color = Color.white;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        StopCoroutineIfRunning(HealthRegeneration());
        StartCoroutineIfNotRunning(HealthRegeneration());
        recentlyDamaged = true; // Set recentlyDamaged to true when taking damage
    }

    private void RestoreHealth(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        lerpTimer = 0f;
    }

    private IEnumerator HealthRegeneration()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            yield return new WaitForSeconds(1f / healthRegenRate);

            if (!recentlyDamaged)
            {
                RestoreHealth(healthRegenAmount);
            }
        }
    }

    private void StartCoroutineIfNotRunning(IEnumerator routine)
    {
        if (healthRegenCoroutine == null)
        {
            healthRegenCoroutine = StartCoroutine(routine);
        }
    }

    private void StopCoroutineIfRunning(IEnumerator routine)
    {
        if (healthRegenCoroutine != null)
        {
            StopCoroutine(routine);
            healthRegenCoroutine = null;
        }
    }
}
