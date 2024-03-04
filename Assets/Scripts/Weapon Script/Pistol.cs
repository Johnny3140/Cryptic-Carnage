using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour
{
    private bool isShooting = false;
    public float damage = 20f; // Damage caused by pistol
    public float range = 100f; 
    private bool isReloading = false;
    public int clipSize = 10;
    public int currentAmmo;

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;

    public TextMeshProUGUI ammoText;
    public AudioSource shootingAudioSource;
    public AudioClip shootingSound;
    public Animator gunAnimator; 
    void Start()
    {
        currentAmmo = clipSize; 
        if (ammoText == null)
        {
            Debug.LogError("Ammo Text not assigned in the Inspector!");
        }
        else
        {
            UpdateAmmoUI();
        }
    }
void Update()
{
    if (isReloading)
        return;

    if (Input.GetButtonDown("Fire1"))
    {
        Shoot();
    }

    // Check for reload input
    if (Input.GetButtonDown("Reload"))
    {
        Debug.Log("Reload button pressed"); 
        StartCoroutine(Reload());
    }
}


   void Shoot()
{   
    if (currentAmmo > 0)
    {
    MuzzleFlash.Play();
    if (shootingAudioSource != null && shootingSound != null)
            {
                // Play shooting sound
                shootingAudioSource.PlayOneShot(shootingSound);
            }

            if (gunAnimator != null)
        {
            // Trigger shooting animation
            gunAnimator.SetBool("IsShooting", true);
            Debug.Log("Shooting animation triggered");
        }

    // Define the layer mask for zombies
    int zombieLayerMask = 1 << LayerMask.NameToLayer("Zombies");

    RaycastHit hit;
    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, zombieLayerMask))
    {
        Debug.Log(hit.transform.name);

        ZombieHealth zombieHealth = hit.transform.GetComponent<ZombieHealth>();
        if (zombieHealth != null)
        {
            zombieHealth.TakeDamage(damage);
        }
                Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.red, 0.5f);
     }
            currentAmmo--;

            UpdateAmmoUI(); // Update UI after shooting
        }
    }
    // Stop shoot animation include delay between shots 
    IEnumerator EndShootingAnimation()
{
    yield return new WaitForSeconds(0.2f);
    isShooting = false;

    if (gunAnimator != null)
    {
        gunAnimator.SetBool("IsShooting", false);
    }
}
 
    public IEnumerator Reload()
{
    Debug.Log("Reloading..."); 
    if (currentAmmo < clipSize)
    {
        isReloading = true;
        // Trigger reload animation
            if (gunAnimator != null)
            {
                gunAnimator.SetBool("IsReloading", true);
                Debug.Log("Reload animation triggered");

            }
        yield return new WaitForSeconds(1.0f); // Simulating reload time
        currentAmmo = clipSize;
        isReloading = false;
        if (gunAnimator != null)
        {
            gunAnimator.SetBool("IsReloading", false);
        }
        UpdateAmmoUI();
    }
}

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + " / " + clipSize;
        }
    }
    
    
}


