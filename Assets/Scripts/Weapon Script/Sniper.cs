using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Transform gunBarrel; // The barrel of the gun where bullets will be spawned
    public GameObject bulletPrefab; // Prefab of the bullet

    public float rotationSpeed = 5f; // Adjust this value to control rotation speed
    public int damage = 130; // Damage caused by sniper rifle

    void Update()
    {
        RotateTowardsMouse();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        Vector3 directionToMouse = worldMousePosition - transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
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
