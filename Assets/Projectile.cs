using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Lifetime = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float numOfBounces = 3f;
    // Update is called once per frame
    [SerializeField] public float projectileSpeed = 20f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Color Red = Color.red;

    [SerializeField] private Color Blue = Color.blue;

    [SerializeField] private Color Green = Color.green;
    private int curBounces = 0;

    private float wallIgnoreTimer;

    private float maxTimer = 0.05f;

    private Vector2 direction;

    public void Shoot(Vector2 direction)
    {
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
        wallIgnoreTimer = Mathf.Infinity;
        Destroy(gameObject, Lifetime);
    }
    void Update()
    {
        wallIgnoreTimer += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RedEnemy") && gameObject.CompareTag("RedBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            return;
        }
        else if (collision.CompareTag("BlueEnemy") && gameObject.CompareTag("BlueBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            return;
        }
        else if (collision.CompareTag("GreenEnemy") && gameObject.CompareTag("GreenBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            return;
        }
        else if (collision.CompareTag("WhiteEnemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            return;
        }
        else if (collision.CompareTag("Player") && gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
            return;
        }

        else if (collision.CompareTag("WhiteTurret") && gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.CompareTag("RedTurret") && gameObject.CompareTag("RedBullet"))
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.CompareTag("BlueTurret") && gameObject.CompareTag("BlueBullet"))
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.CompareTag("GreenTurret") && gameObject.CompareTag("GreenBullet"))
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.CompareTag("RedMirror") && wallIgnoreTimer > maxTimer)
        {
            spriteRenderer.color = Red;
            gameObject.tag = "RedBullet";
        }
        else if (collision.CompareTag("BlueMirror") && wallIgnoreTimer > maxTimer)
        {
            spriteRenderer.color = Blue;
            gameObject.tag = "BlueBullet";
        }
        else if (collision.CompareTag("GreenMirror") && wallIgnoreTimer > maxTimer)
        {
            spriteRenderer.color = Green;
            gameObject.tag = "GreenBullet";
        }
        if (curBounces >= numOfBounces)
        {
            Destroy(gameObject);
        }
        if (wallIgnoreTimer > maxTimer && collision.CompareTag("RedMirror") || collision.CompareTag("BlueMirror") || collision.CompareTag("GreenMirror"))
        {
            Vector2 inDirection = rb.velocity.normalized;
            direction = Vector2.Reflect(inDirection, collision.transform.right);
            rb.velocity = direction * Mathf.Max(projectileSpeed, 0f);
            transform.up = direction;
            curBounces++;
            wallIgnoreTimer = 0f;
        }
        else if (collision.CompareTag("Wall") && wallIgnoreTimer > maxTimer)
        {
            Destroy(gameObject);
            return;
        }

    }
}
