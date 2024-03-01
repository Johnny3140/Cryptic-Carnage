using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{

    public float damage = 20f; // Damage caused by pistol
    public float range = 100f; 

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    

    void Shoot()
    {   MuzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
           ZombieHealth zombieHealth = hit.transform.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damage);
            }
        }

    }
}
