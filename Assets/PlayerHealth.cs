using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private int healthMax = 100;

    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            currentHealth -= 10;
            healthBar.fillAmount = currentHealth / healthMax;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WhiteEnemy") || other.gameObject.CompareTag("RedEnemy") || other.gameObject.CompareTag("BlueEnemy") || other.gameObject.CompareTag("GreenEnemy"))
        {
            currentHealth -= 10;
            healthBar.fillAmount = currentHealth / healthMax;
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
