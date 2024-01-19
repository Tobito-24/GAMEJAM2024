using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public Projectile projectile;
    public Transform bulletOrigin;
    public float BulletForce = 20f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Instantiate a bullet prefab at the player's position and rotation
        // Instantiate() returns a reference to the object it creates
        // We can use this reference to access the object's components and properties
        // For example, we can use it to access the bullet's rigidbody and apply a force to it
        Projectile projectileInstance = Instantiate(projectile, bulletOrigin.position, Quaternion.identity);
        projectileInstance.transform.up = transform.up;
        projectileInstance.Shoot(transform.up);

    }
}
