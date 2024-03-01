using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet speed
    public int damage = 10; // Bullet damage
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
        if (zombieHealth != null)
        {
            zombieHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
