using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject player;
    public float beginFirerate = 4f;

    public float firerate = 1f;

    public float T_ShootDelay = 0f;

    public int hp = 3;
    public Projectile projectile;
    public Transform bulletOrigin;
    SpawnBullet spawnBullet;
    // Empty GameObject to act as the pivot point
    private GameObject pivotPoint;

    private void Start()
    {

    }

    private void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (T_ShootDelay < beginFirerate)
            T_ShootDelay += Time.deltaTime;
        else if (distance < 10 && T_ShootDelay >= beginFirerate)
        {
            ShootTowardsPlayer(direction);
            T_ShootDelay = 0f;
            beginFirerate = firerate;
        }
    }

    void ShootTowardsPlayer(Vector2 direction)
    {
        Projectile projectileInstance = Instantiate(projectile, bulletOrigin.position, Quaternion.identity);
        projectileInstance.transform.up = transform.up;
        projectileInstance.tag = "EnemyBullet";
        projectile.projectileSpeed = 10f;
        projectileInstance.Shoot(transform.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Bullet") && gameObject.CompareTag("WhiteTurret")) || (collision.gameObject.CompareTag("RedBullet") && gameObject.CompareTag("RedTurret")) || (collision.gameObject.CompareTag("BlueBullet") && gameObject.CompareTag("BlueTurret")) || (collision.gameObject.CompareTag("GreenBullet") && gameObject.CompareTag("GreenTurret")))
        {
            hp--;
            if (hp <= 0)
                Destroy(gameObject);
        }

    }
}
