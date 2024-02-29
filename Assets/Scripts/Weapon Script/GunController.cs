using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public Transform gunBarrel; // The barrel of the gun where bullets will be spawned
    public float rotationSpeed = 5f; // Adjust this value to control rotation speed
    public int damage = 20; // Damage caused by the gun

    void Update()
    {
        RotateGun();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void RotateGun()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 target = ray.GetPoint(rayDistance);
            Vector3 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage);
        }
    }
}
